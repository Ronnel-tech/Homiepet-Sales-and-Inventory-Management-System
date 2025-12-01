using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace red_framework
{
    /// <summary>
    /// SQL Server implementation with parameterized queries and helper utilities (synchronous).
    /// </summary>
    public class SqlDatabase : IDatabase, IDisposable
    {
        private readonly SqlConnection conn;

        /// <summary>Default command timeout in seconds for all commands.</summary>
        public int CommandTimeoutSeconds { get; set; } = 30;

        /// <summary>Optional logger; assign to write SQL text (e.g., Debug.WriteLine).</summary>
        public Action<string> Logger { get; set; }

        public SqlDatabase(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
        }

        public void Close() => conn.Close();

        public void Dispose()
        {
            try
            {
                if (conn?.State != ConnectionState.Closed) conn?.Close();
                conn?.Dispose();
            }
            catch { /* swallow on dispose */ }
        }

        #region Core plumbing

        private void EnsureConnectionOpen()
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        private static SqlDbType InferSqlDbType(Type t)
        {
            t = Nullable.GetUnderlyingType(t) ?? t;

            if (t == typeof(string)) return SqlDbType.NVarChar;
            if (t == typeof(int)) return SqlDbType.Int;
            if (t == typeof(long)) return SqlDbType.BigInt;
            if (t == typeof(short)) return SqlDbType.SmallInt;
            if (t == typeof(byte)) return SqlDbType.TinyInt;
            if (t == typeof(bool)) return SqlDbType.Bit;
            if (t == typeof(DateTime)) return SqlDbType.DateTime2;
            if (t == typeof(decimal)) return SqlDbType.Decimal;
            if (t == typeof(double)) return SqlDbType.Float;
            if (t == typeof(float)) return SqlDbType.Real;
            if (t == typeof(Guid)) return SqlDbType.UniqueIdentifier;
            if (t == typeof(byte[])) return SqlDbType.VarBinary;

            return SqlDbType.Variant;
        }

        private static void AddParameters(SqlCommand cmd, object parameters)
        {
            if (parameters == null) return;

            // Allow IDictionary<string, object> OR anonymous/POCO
            if (parameters is IDictionary<string, object> dict)
            {
                foreach (var kv in dict)
                {
                    var p = cmd.Parameters.Add("@" + kv.Key, kv.Value == null || kv.Value is DBNull
                        ? SqlDbType.Variant
                        : InferSqlDbType(kv.Value.GetType()));
                    p.Value = kv.Value ?? DBNull.Value;
                }
                return;
            }

            foreach (var prop in parameters.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var name = "@" + prop.Name;
                var value = prop.GetValue(parameters) ?? DBNull.Value;
                var sqlType = value == DBNull.Value ? InferSqlDbType(prop.PropertyType) : InferSqlDbType(value.GetType());

                var p = cmd.Parameters.Add(name, sqlType);
                // Handle common sizes/precision if desired
                if (sqlType == SqlDbType.NVarChar && value is string s && s.Length <= 4000) p.Size = Math.Max(1, s.Length);
                if (sqlType == SqlDbType.Decimal)
                {
                    p.Precision = 18;
                    p.Scale = 6;
                }
                p.Value = value;
            }
        }

        private SqlCommand CreateCommand(string query, object parameters = null, SqlTransaction tran = null)
        {
            EnsureConnectionOpen();
            var cmd = new SqlCommand(query, conn, tran) { CommandTimeout = CommandTimeoutSeconds };
            AddParameters(cmd, parameters);
            Logger?.Invoke(cmd.CommandText);
            return cmd;
        }

        private static string Q(string identifier) => $"[{identifier}]"; // bracket-quote

        #endregion

        #region Basic CRUD / Queries (sync)

        public int CUD(string query, object parameters = null)
        {
            EnsureConnectionOpen();
            using (var cmd = CreateCommand(query, parameters))
                return cmd.ExecuteNonQuery();
        }

        public object Scalar(string query, object parameters = null)
        {
            EnsureConnectionOpen();
            using (var cmd = CreateCommand(query, parameters))
                return cmd.ExecuteScalar();
        }

        public DataTable TableData(string query, object parameters = null)
        {
            EnsureConnectionOpen();
            using (var cmd = CreateCommand(query, parameters))
            using (var da = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // KEEP: your original reader method, but now private to guide users to DataTable/POCO.
        // Public if you still need manual reader consumption.
        public SqlDataReader select(string query)
        {
            EnsureConnectionOpen();
            var cmd = CreateCommand(query);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public bool Save(string table, object data)
        {
            EnsureConnectionOpen();
            var props = data.GetType().GetProperties();
            var columns = string.Join(",", props.Select(p => Q(p.Name)));
            var values = string.Join(",", props.Select(p => "@" + p.Name));
            string query = $"INSERT INTO {Q(table)} ({columns}) VALUES ({values})";
            return CUD(query, data) > 0;
        }

        public bool Update(string table, object data, string keyColumn = "Id")
        {
            EnsureConnectionOpen();
            var props = data.GetType().GetProperties().Where(p => !string.Equals(p.Name, keyColumn, StringComparison.OrdinalIgnoreCase));
            var setClause = string.Join(",", props.Select(p => $"{Q(p.Name)} = @{p.Name}"));
            string query = $"UPDATE {Q(table)} SET {setClause} WHERE {Q(keyColumn)} = @{keyColumn}";
            return CUD(query, data) > 0;
        }

        public bool Delete(string table, object data, string keyColumn = "Id")
        {
            EnsureConnectionOpen();
            string query = $"DELETE FROM {Q(table)} WHERE {Q(keyColumn)} = @{keyColumn}";
            return CUD(query, data) > 0;
        }

        public object Max(string table, string column) => Scalar($"SELECT MAX({Q(column)}) FROM {Q(table)}");
        public object Min(string table, string column) => Scalar($"SELECT MIN({Q(column)}) FROM {Q(table)}");

        public int Count(string table, string condition = "")
        {
            var where = string.IsNullOrWhiteSpace(condition) ? "" : " WHERE " + condition;
            return Convert.ToInt32(Scalar($"SELECT COUNT(*) FROM {Q(table)}{where}"));
        }

        public bool Exists(string table, string condition) => Count(table, condition) > 0;

        public DataRow GetById(string table, object id, string keyColumn = "Id")
        {
            string query = $"SELECT * FROM {Q(table)} WHERE {Q(keyColumn)} = @{keyColumn}";
            var dt = TableData(query, new Dictionary<string, object> { [keyColumn] = id });
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public DataTable Search(string table, string[] columns, string keyword)
        {
            string likeClause = string.Join(" OR ", columns.Select(c => $"{Q(c)} LIKE @keyword"));
            string query = $"SELECT * FROM {Q(table)} WHERE {likeClause}";
            return TableData(query, new { keyword = "%" + keyword + "%" });
        }

        public decimal Sum(string table, string column, string condition = "")
        {
            string where = string.IsNullOrWhiteSpace(condition) ? "" : " WHERE " + condition;
            var obj = Scalar($"SELECT SUM({Q(column)}) FROM {Q(table)}{where}");
            return obj == null || obj is DBNull ? 0m : Convert.ToDecimal(obj);
        }

        public decimal Average(string table, string column, string condition = "")
        {
            string where = string.IsNullOrWhiteSpace(condition) ? "" : " WHERE " + condition;
            var obj = Scalar($"SELECT AVG({Q(column)}) FROM {Q(table)}{where}");
            return obj == null || obj is DBNull ? 0m : Convert.ToDecimal(obj);
        }

        public DataTable GetTop(string table, int top = 10, string orderBy = "Id DESC")
        {
            string query = $"SELECT TOP {top} * FROM {Q(table)} ORDER BY {orderBy}";
            return TableData(query);
        }

        public bool ExecuteTransaction(params (string query, object parameters)[] commands)
        {
            EnsureConnectionOpen();
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    foreach (var (query, parameters) in commands)
                    {
                        using (var cmd = CreateCommand(query, parameters, tran))
                            cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public DataTable Paginate(string table, int pageNumber, int pageSize, string orderBy = "Id ASC")
        {
            int offset = (pageNumber - 1) * pageSize;
            string query = $@"
SELECT * FROM {Q(table)}
ORDER BY {orderBy}
OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
            return TableData(query);
        }

        public (DataTable Rows, int TotalCount) PaginateWithCount(string table, int pageNumber, int pageSize, string where = "", string orderBy = "Id ASC")
        {
            EnsureConnectionOpen();
            int offset = (pageNumber - 1) * pageSize;
            string whereClause = string.IsNullOrWhiteSpace(where) ? "" : $"WHERE {where}";
            string query = $@"
SELECT * FROM {Q(table)} {whereClause}
ORDER BY {orderBy}
OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;

SELECT COUNT(*) FROM {Q(table)} {whereClause};";

            using (var cmd = CreateCommand(query))
            using (var reader = cmd.ExecuteReader())
            {
                var dt = new DataTable();
                dt.Load(reader);

                int total = 0;
                if (reader.NextResult() && reader.Read())
                    total = Convert.ToInt32(reader.GetValue(0));

                return (dt, total);
            }
        }

        public DataTable GetDistinct(string table, string column)
            => TableData($"SELECT DISTINCT {Q(column)} FROM {Q(table)}");

        public DataTable Join(string table1, string table2, string onCondition, string columns = "*", string where = "")
        {
            string query = $"SELECT {columns} FROM {Q(table1)} INNER JOIN {Q(table2)} ON {onCondition} " +
                           $"{(string.IsNullOrWhiteSpace(where) ? "" : "WHERE " + where)}";
            return TableData(query);
        }

        public bool Backup(string filePath)
        {
            try
            {
                // Note: requires SQL Server permissions and a valid server path.
                string query = $"BACKUP DATABASE [{conn.Database}] TO DISK = '{filePath.Replace("'", "''")}'";
                CUD(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ClearTable(string table) => CUD($"DELETE FROM {Q(table)}") > 0;

        public bool BulkInsert(string table, DataTable data)
        {
            EnsureConnectionOpen();
            try
            {
                using (var bulk = new SqlBulkCopy(conn) { DestinationTableName = table })
                {
                    bulk.WriteToServer(data);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bulk insert failed:\n" + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Ping()
        {
            try
            {
                EnsureConnectionOpen();
                using (var cmd = new SqlCommand("SELECT 1", conn))
                    return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Stored Procedures (sync)

        public int ExecStoredProcedure(string procName, object parameters = null, int? timeoutSeconds = null)
        {
            EnsureConnectionOpen();
            using (var cmd = new SqlCommand(procName, conn) { CommandType = CommandType.StoredProcedure })
            {
                if (timeoutSeconds.HasValue) cmd.CommandTimeout = timeoutSeconds.Value;
                AddParameters(cmd, parameters);
                Logger?.Invoke($"EXEC {procName}");
                return cmd.ExecuteNonQuery();
            }
        }

        public DataTable QueryStoredProcedure(string procName, object parameters = null, int? timeoutSeconds = null)
        {
            EnsureConnectionOpen();
            using (var cmd = new SqlCommand(procName, conn) { CommandType = CommandType.StoredProcedure })
            {
                if (timeoutSeconds.HasValue) cmd.CommandTimeout = timeoutSeconds.Value;
                AddParameters(cmd, parameters);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        #endregion

        #region Upsert / Soft Delete / Audit (sync)

        public bool Upsert(string table, string keyColumn, object data)
        {
            EnsureConnectionOpen();
            var props = data.GetType().GetProperties();
            var cols = props.Select(p => p.Name).ToList();
            var setClause = string.Join(",", cols.Where(c => !string.Equals(c, keyColumn, StringComparison.OrdinalIgnoreCase))
                                                 .Select(c => $"{Q(c)} = @{c}"));

            string query = $@"
IF EXISTS (SELECT 1 FROM {Q(table)} WHERE {Q(keyColumn)} = @{keyColumn})
    UPDATE {Q(table)} SET {setClause} WHERE {Q(keyColumn)} = @{keyColumn};
ELSE
    INSERT INTO {Q(table)} ({string.Join(",", cols.Select(Q))})
    VALUES ({string.Join(",", cols.Select(c => "@" + c))});";

            return CUD(query, data) > 0;
        }

        public bool SoftDelete(string table, object id, string keyColumn = "Id", string deletedColumn = "IsDeleted")
            => CUD($"UPDATE {Q(table)} SET {Q(deletedColumn)} = 1 WHERE {Q(keyColumn)} = @{keyColumn}",
                   new Dictionary<string, object> { [keyColumn] = id }) > 0;

        public bool Restore(string table, object id, string keyColumn = "Id", string deletedColumn = "IsDeleted")
            => CUD($"UPDATE {Q(table)} SET {Q(deletedColumn)} = 0 WHERE {Q(keyColumn)} = @{keyColumn}",
                   new Dictionary<string, object> { [keyColumn] = id }) > 0;

        public bool Touch(string table, object id, string keyColumn = "Id", string column = "UpdatedAt")
            => CUD($"UPDATE {Q(table)} SET {Q(column)} = SYSDATETIME() WHERE {Q(keyColumn)} = @{keyColumn}",
                   new Dictionary<string, object> { [keyColumn] = id }) > 0;

        #endregion

        #region POCO mapping (sync)

        public T QuerySingle<T>(string query, object parameters = null) where T : new()
        {
            EnsureConnectionOpen();
            using (var cmd = CreateCommand(query, parameters))
            using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (!reader.Read()) return default;
                return MapRecord<T>(reader);
            }
        }

        public List<T> QueryList<T>(string query, object parameters = null) where T : new()
        {
            EnsureConnectionOpen();
            using (var cmd = CreateCommand(query, parameters))
            using (var reader = cmd.ExecuteReader())
            {
                var list = new List<T>();
                while (reader.Read())
                    list.Add(MapRecord<T>(reader));
                return list;
            }
        }

        private static T MapRecord<T>(IDataRecord record) where T : new()
        {
            var obj = new T();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < record.FieldCount; i++)
            {
                var name = record.GetName(i);
                var prop = props.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
                if (prop == null) continue;

                var val = record.IsDBNull(i) ? null : record.GetValue(i);
                if (val == null) { prop.SetValue(obj, null); continue; }

                var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                prop.SetValue(obj, Convert.ChangeType(val, targetType));
            }
            return obj;
        }

        #endregion

        #region Retry / Transaction helpers (sync)

        public T ExecuteWithRetry<T>(Func<T> operation, int maxRetries = 3, int delayMs = 200)
        {
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try { return operation(); }
                catch (SqlException ex) when (IsTransient(ex))
                {
                    if (attempt == maxRetries) throw;
                    System.Threading.Thread.Sleep(delayMs);
                    delayMs = Math.Min(delayMs * 2, 4000);
                }
            }
            return default;
        }

        private static bool IsTransient(SqlException ex)
        {
            // Extend with other transient numbers as needed.
            return ex.Number == -2 /* timeout */ || ex.Number == 1205 /* deadlock */;
        }

        /// <summary>
        /// Transaction wrapper with custom work that receives the active SqlTransaction.
        /// </summary>
        public bool ExecuteTransaction(Func<SqlTransaction, bool> work)
        {
            EnsureConnectionOpen();
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    bool ok = work(tran);
                    if (ok) tran.Commit(); else tran.Rollback();
                    return ok;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        #endregion

        #region Schema / Metadata (sync)

        public DataTable GetSchemaTables() => conn.GetSchema("Tables");

        public DataTable GetSchemaColumns(string table)
            => conn.GetSchema("Columns", new[] { null, null, table, null });

        public bool ColumnExists(string table, string column)
        {
            var cols = GetSchemaColumns(table);
            return cols.AsEnumerable()
                       .Any(r => string.Equals(r["COLUMN_NAME"]?.ToString(), column, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region WinForms helpers (kept) + parameterized overloads

        public void Table(DataTable dt, DataGridView dgv, string[] header = null)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;

            dgv.DataSource = dt;

            if (header != null)
                foreach (DataGridViewColumn dgvcolumn in dgv.Columns)
                    dgvcolumn.HeaderText = header[dgvcolumn.Index];

            dgv.ClearSelection();
        }

        public void Table(string query, DataGridView dgv, string[] header = null)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;

            dgv.DataSource = TableData(query);

            if (header != null)
                foreach (DataGridViewColumn dgvcolumn in dgv.Columns)
                    dgvcolumn.HeaderText = header[dgvcolumn.Index];

            dgv.ClearSelection();
        }

        public void Table(string query, object parameters, DataGridView dgv, string[] header = null)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically;

            dgv.DataSource = TableData(query, parameters);

            if (header != null)
                foreach (DataGridViewColumn dgvcolumn in dgv.Columns)
                    dgvcolumn.HeaderText = header[dgvcolumn.Index];

            dgv.ClearSelection();
        }

        public void Table(string query, ListView lv, string[] headers = null)
        {
            try
            {
                lv.Items.Clear();
                lv.Columns.Clear();
                lv.View = View.Details;
                lv.FullRowSelect = true;
                lv.GridLines = true;
                lv.MultiSelect = false;

                var dt = TableData(query);
                if (dt.Rows.Count == 0) return;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string headerText = headers != null && i < headers.Length ? headers[i] : dt.Columns[i].ColumnName;
                    lv.Columns.Add(headerText, -2, HorizontalAlignment.Left);
                }

                foreach (DataRow row in dt.Rows)
                {
                    var item = new ListViewItem(row[0]?.ToString());
                    for (int i = 1; i < dt.Columns.Count; i++)
                        item.SubItems.Add(row[i]?.ToString());
                    lv.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data into ListView:\n" + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void list(string query, ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            using (var cmd = CreateCommand(query))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var value = reader[0].ToString();
                    comboBox.Items.Add(value);
                    comboBox.AutoCompleteCustomSource.Add(value);
                }
            }
        }

        #endregion

        // Add this method to the SqlDatabase class
        public object Query(string query)
        {
            return TableData(query);
        }
    }
}

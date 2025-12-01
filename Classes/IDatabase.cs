using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace red_framework
{
    /// <summary>
    /// Synchronous database abstraction for SQL Server.
    /// Implemented by <see cref="SqlDatabase"/>.
    /// </summary>
    public interface IDatabase : IDisposable
    {
        // ---------- Configuration / Diagnostics ----------
        /// <summary>Default command timeout (seconds) applied to all commands.</summary>
        int CommandTimeoutSeconds { get; set; }

        /// <summary>Optional SQL logger (e.g., Debug.WriteLine).</summary>
        Action<string> Logger { get; set; }

        // ---------- Core ----------
        /// <summary>Executes INSERT/UPDATE/DELETE or any non-query command.</summary>
        int CUD(string query, object parameters = null);

        /// <summary>Executes a scalar query and returns the first column of the first row.</summary>
        object Scalar(string query, object parameters = null);

        /// <summary>Executes a query and returns results as a <see cref="DataTable"/>.</summary>
        DataTable TableData(string query, object parameters = null);

        /// <summary>Executes a query and returns a forward-only reader (caller must consume it).</summary>
        SqlDataReader select(string query);

        /// <summary>Closes the underlying connection.</summary>
        void Close();

        // ---------- CRUD helpers ----------
        /// <summary>Inserts a row using the object's public properties as columns.</summary>
        bool Save(string table, object data);

        /// <summary>Updates a row by <paramref name="keyColumn"/> using object properties.</summary>
        bool Update(string table, object data, string keyColumn = "Id");

        /// <summary>Deletes a row by <paramref name="keyColumn"/> using object properties.</summary>
        bool Delete(string table, object data, string keyColumn = "Id");

        /// <summary>Returns a single row by primary key.</summary>
        DataRow GetById(string table, object id, string keyColumn = "Id");

        // ---------- Aggregates / checks ----------
        object Max(string table, string column);
        object Min(string table, string column);

        /// <summary>Counts rows in <paramref name="table"/> optionally filtered by <paramref name="condition"/> (SQL WHERE without keyword).</summary>
        int Count(string table, string condition = "");

        /// <summary>Returns true if any row matches <paramref name="condition"/>.</summary>
        bool Exists(string table, string condition);

        /// <summary>Performs a LIKE search across the specified <paramref name="columns"/> using <paramref name="keyword"/>.</summary>
        DataTable Search(string table, string[] columns, string keyword);

        /// <summary>SUM aggregate with optional WHERE condition.</summary>
        decimal Sum(string table, string column, string condition = "");

        /// <summary>AVG aggregate with optional WHERE condition.</summary>
        decimal Average(string table, string column, string condition = "");

        // ---------- Listing / paging ----------
        /// <summary>Returns TOP N rows ordered by <paramref name="orderBy"/>.</summary>
        DataTable GetTop(string table, int top = 10, string orderBy = "Id DESC");

        /// <summary>Paginates rows using OFFSET/FETCH.</summary>
        DataTable Paginate(string table, int pageNumber, int pageSize, string orderBy = "Id ASC");

        /// <summary>Paginates and returns both page rows and total count for UI grids.</summary>
        (DataTable Rows, int TotalCount) PaginateWithCount(string table, int pageNumber, int pageSize, string where = "", string orderBy = "Id ASC");

        /// <summary>Returns distinct values of a column.</summary>
        DataTable GetDistinct(string table, string column);

        /// <summary>Performs INNER JOIN with custom column selection and optional WHERE.</summary>
        DataTable Join(string table1, string table2, string onCondition, string columns = "*", string where = "");

        // ---------- Bulk / admin ----------
        /// <summary>Clears (deletes all rows from) a table.</summary>
        bool ClearTable(string table);

        /// <summary>Bulk inserts rows from a DataTable (column names must match).</summary>
        bool BulkInsert(string table, DataTable data);

        /// <summary>Backs up the current database to the specified file path (requires permissions).</summary>
        bool Backup(string filePath);

        /// <summary>Quick connectivity check (SELECT 1).</summary>
        bool Ping();

        // ---------- Transactions ----------
        /// <summary>
        /// Executes multiple parameterized commands within a transaction.
        /// Returns true on success; false if rolled back on error.
        /// </summary>
        bool ExecuteTransaction(params (string query, object parameters)[] commands);

        /// <summary>
        /// Transaction wrapper that provides the active SqlTransaction to user code.
        /// Implementation may open, commit, and rollback as appropriate.
        /// </summary>
        bool ExecuteTransaction(Func<SqlTransaction, bool> work);

        // ---------- Higher-level helpers ----------
        /// <summary>Insert-or-update based on presence of <paramref name="keyColumn"/>.</summary>
        bool Upsert(string table, string keyColumn, object data);

        /// <summary>Marks a row as deleted (soft delete) by setting a flag column.</summary>
        bool SoftDelete(string table, object id, string keyColumn = "Id", string deletedColumn = "IsDeleted");

        /// <summary>Restores a soft-deleted row.</summary>
        bool Restore(string table, object id, string keyColumn = "Id", string deletedColumn = "IsDeleted");

        /// <summary>Updates a timestamp column (e.g., UpdatedAt) to current time.</summary>
        bool Touch(string table, object id, string keyColumn = "Id", string column = "UpdatedAt");

        // ---------- Stored procedures ----------
        /// <summary>Execute a stored procedure that does not return a result set.</summary>
        int ExecStoredProcedure(string procName, object parameters = null, int? timeoutSeconds = null);

        /// <summary>Execute a stored procedure and return results as DataTable.</summary>
        DataTable QueryStoredProcedure(string procName, object parameters = null, int? timeoutSeconds = null);

        // ---------- POCO mapping ----------
        /// <summary>Returns a single record mapped to <typeparamref name="T"/> by matching column/property names.</summary>
        T QuerySingle<T>(string query, object parameters = null) where T : new();

        /// <summary>Returns a list of records mapped to <typeparamref name="T"/> by matching column/property names.</summary>
        List<T> QueryList<T>(string query, object parameters = null) where T : new();

        // ---------- Retry ----------
        /// <summary>
        /// Executes the provided operation with simple transient-error retries (e.g., timeouts, deadlocks).
        /// </summary>
        T ExecuteWithRetry<T>(Func<T> operation, int maxRetries = 3, int delayMs = 200);

        // ---------- Schema / Metadata ----------
        /// <summary>Returns schema information for all tables in the current database.</summary>
        DataTable GetSchemaTables();

        /// <summary>Returns schema information for columns of the specified table.</summary>
        DataTable GetSchemaColumns(string table);

        /// <summary>Checks if a column exists on a given table.</summary>
        bool ColumnExists(string table, string column);

        // ---------- WinForms helpers ----------

        void Table(DataTable dt, DataGridView dgv, string[] header = null);

        /// <summary>Binds the result of a query to a DataGridView, with optional custom headers.</summary>
        void Table(string query, DataGridView dgv, string[] header = null);

        /// <summary>Binds a parameterized query result to a DataGridView, with optional custom headers.</summary>
        void Table(string query, object parameters, DataGridView dgv, string[] header = null);

        /// <summary>Binds the result of a query to a ListView, with optional custom headers.</summary>
        void Table(string query, ListView lv, string[] headers = null);

        /// <summary>Fills a ComboBox with values from the first column of the query result.</summary>
        void list(string query, ComboBox comboBox);
        object Query(string v);
    }
}
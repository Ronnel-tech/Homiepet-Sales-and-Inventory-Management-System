using Guna.Charts.Interfaces;
using Guna.Charts.WinForms;
using LiveCharts;
using LiveCharts.Wpf;
using red_framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LiveChartsAxis = LiveCharts.Wpf.Axis;



namespace Homiepet_Corner_Sales_and_Inventory_Management_System.Forms
{
    public partial class Dashboard_form : Form
    {
        public Dashboard_form()
        {
            InitializeComponent();

            LoadAppointments();
            LoadLowStock();
            LoadTotalSalesToday();
            LoadTotalTransactionsToday();
            LoadAppointmentsToday();

            BindSalesPerDay();

        }

        # region Display Appointments in Data Grid View
        void LoadAppointments()
        {
            var db = AppDb.Instance;

            DataTable dt = db.TableData(@"
    SELECT 
        a.AppointID,
        c.CustomerID,
        s.ServiceID,
        c.FirstName,
        c.LastName,
        c.Contact,
        c.Barangay,
        c.Province,
        s.ServiceName AS ServiceType,
        s.Price,
        a.DateBooked,
        a.Status
    FROM Appointments a
    INNER JOIN Customers c ON a.CustomerID = c.CustomerID
    INNER JOIN Services s ON a.ServiceID = s.ServiceID
    ORDER BY a.DateBooked DESC
");

            dgv_appointment.DataSource = dt;

            dgv_appointment.Columns["CustomerID"].Visible = false;
            dgv_appointment.Columns["ServiceID"].Visible = false;
            dgv_appointment.Columns["Contact"].Visible = false;
            dgv_appointment.Columns["Barangay"].Visible = false;
            dgv_appointment.Columns["Province"].Visible = false;
            dgv_appointment.Columns["AppointID"].Visible = false;
            dgv_appointment.Columns["FirstName"].Visible = false;
            dgv_appointment.Columns["LastName"].Visible = false;

            // Optional: Format columns
            dgv_appointment.Columns["Price"].DefaultCellStyle.Format = "N2"; // two decimal places
            dgv_appointment.Columns["DateBooked"].DefaultCellStyle.Format = "yyyy-MM-dd"; // formatted date
        }
        #endregion

        #region Display Low Stock Products in Data Grid View
        void LoadLowStock()
        {
            var db = AppDb.Instance;

            DataTable dt = db.TableData(@"
        SELECT 
            p.ProductID,
            p.Name,
            c.CategoryName,
            p.Price,
            p.StockQty
        FROM Product p
        INNER JOIN Category c ON p.CategoryID = c.CategoryID
        WHERE p.StockQty <= 50
        ORDER BY p.StockQty ASC
    ");

            dgv_low_stock.DataSource = dt;

            dgv_low_stock.Columns["ProductID"].Visible = false;
            dgv_low_stock.Columns["Price"].Visible = false;

        }
        #endregion

        #region Display Total Sales
void LoadTotalSalesToday()
{
    var db = AppDb.Instance;

    object result = db.Scalar(@"
        SELECT 
            ISNULL(SUM(TotalAmount), 0) +
            ISNULL((
                SELECT SUM(s.Price)
                FROM Appointments a
                INNER JOIN Services s ON a.ServiceID = s.ServiceID
                WHERE a.Status = 'Completed'
                AND CAST(a.DateBooked AS DATE) = CAST(GETDATE() AS DATE)
            ), 0)
        FROM Sales
        WHERE CAST(Date AS DATE) = CAST(GETDATE() AS DATE)
    ");

    decimal totalSales = Convert.ToDecimal(result);
    lbl_sales_today.Text = totalSales.ToString("₱0.00");
}
        #endregion

        #region Display Total Transactions
        void LoadTotalTransactionsToday()
        {
            var db = AppDb.Instance;

            object result = db.Scalar(@"
        SELECT COUNT(*) 
        FROM Sales
        WHERE CAST(Date AS DATE) = CAST(GETDATE() AS DATE)
    ");

            int salesCount = Convert.ToInt32(result);

            lbl_transaction_today.Text = salesCount.ToString();
        }

        #endregion

        #region Display Appointments Today
        void LoadAppointmentsToday()
        {
            var db = AppDb.Instance;

            object result = db.Scalar(@"
        SELECT COUNT(*) 
        FROM Appointments
        WHERE DateBooked >= CAST(GETDATE() AS DATE)
          AND DateBooked < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
    ");

            int apptCount = 0;

            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                apptCount = count;
            }

            lbl_appointment_today.Text = apptCount.ToString();
        }

        #endregion

        #region Chart - Sales Per Day in Last 7 Days
        private void BindSalesPerDay()
        {
            var series = new LiveCharts.SeriesCollection();
            var salesCounts = new ChartValues<double>();
            var labels = new List<string>();

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ronne\\source\\repos\\Homiepet_Corner_Sales_and_Inventory_Management_System\\HomieDb.mdf;Integrated Security=True";
            string query = @"
        SELECT 
            CONVERT(VARCHAR(10), Date, 120) AS SaleDate, 
            COUNT(*) AS SaleCount
        FROM Sales
        WHERE Date >= DATEADD(DAY, -7, GETDATE())
        GROUP BY CONVERT(VARCHAR(10), Date, 120)
        ORDER BY SaleDate;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string date = Convert.ToDateTime(reader["SaleDate"]).ToString("ddd"); // Mon, Tue, etc.
                        double count = Convert.ToDouble(reader["SaleCount"]);

                        salesCounts.Add(count);
                        labels.Add(date);
                    }
                }
            }

            series.Add(new LineSeries
            {
                Title = "Sales Count",
                Values = salesCounts,
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 8
            });

            sales_daily_in_week.Series = series;

            sales_daily_in_week.AxisX.Clear();
            sales_daily_in_week.AxisX.Add(new LiveChartsAxis
            {
                Title = "Day of Week",
                Labels = labels
            });

            sales_daily_in_week.AxisY.Clear();
            sales_daily_in_week.AxisY.Add(new LiveChartsAxis
            {
                Title = "Number of Sales"
            });
        }
        #endregion 


        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void Dashboard_form_Load(object sender, EventArgs e)
        {
        }
        private void dgv_appointment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void lbl_transaction_today_Click(object sender, EventArgs e)
        {

        }
    }
}

using red_framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
                                                                                                                                                                                                                                                                                            
using LiveCharts;
using LiveCharts.Wpf; // for Series types
using LiveCharts.WinForms;

namespace Homiepet_Corner_Sales_and_Inventory_Management_System.Forms
{
    public partial class Report_form : Form
    {
        public Report_form()
        {
            InitializeComponent();
            LoadOverallSales();
            LoadSalesThisWeek();
            LoadServicesThisWeek();
            LoadAverageSalesPerDay();
            LoadBestSellingProducts();
            BindBestSellingToCartesian();
            BindQuantityAndRevenue();
        }

        #region Revenue This Month
        void LoadOverallSales()
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
                  AND YEAR(a.DateBooked) = YEAR(GETDATE())
                  AND MONTH(a.DateBooked) = MONTH(GETDATE())
            ), 0)
        FROM Sales
        WHERE YEAR(Date) = YEAR(GETDATE())
          AND MONTH(Date) = MONTH(GETDATE())
    ");

            decimal salesThisMonth = 0;

            if (result != null && decimal.TryParse(result.ToString(), out decimal total))
            {
                salesThisMonth = total;
            }

            lbl_overall.Text = salesThisMonth.ToString("₱0.00");
        }

        #endregion

        #region Revenue this week
        void LoadSalesThisWeek()
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
                  AND a.DateBooked >= DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE))
            ), 0)
        FROM Sales
        WHERE Date >= DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE))
    ");

            decimal salesThisWeek = 0;

            if (result != null && decimal.TryParse(result.ToString(), out decimal total))
            {
                salesThisWeek = total;
            }

            lbl_weekly.Text = salesThisWeek.ToString("₱0.00");
        }
        #endregion

        #region Load Services This Week
        void LoadServicesThisWeek()
        {
            var db = AppDb.Instance;

            object result = db.Scalar(@"
SELECT COUNT(*)
FROM Appointments
WHERE Status = 'Completed'
AND DateBooked >= DATEADD(WEEK, DATEDIFF(WEEK, 0, DATEADD(DAY, -1, GETDATE())), 0)
AND DateBooked< DATEADD(WEEK, DATEDIFF(WEEK, 0, DATEADD(DAY, -1, GETDATE())) + 1, 0)
    ");

            int servicesThisWeek = 0;

            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                servicesThisWeek = count;
            }

            lbl_service.Text = servicesThisWeek.ToString();
        }

        #endregion

        #region Load Average Sales Per Day
        void LoadAverageSalesPerDay()
        {
            var db = AppDb.Instance;

            // Count sales per day
            DataTable dt = db.TableData(@"
        SELECT CAST(Date AS DATE) AS SaleDate, COUNT(*) AS SalesCount
        FROM Sales
        GROUP BY CAST(Date AS DATE)
    ");

            if (dt.Rows.Count == 0)
            {
                lbl_average.Text = "0";
                return;
            }

            // Calculate average
            double avgSales = dt.AsEnumerable()
                                .Average(r => Convert.ToDouble(r["SalesCount"]));

            lbl_average.Text = Math.Round(avgSales, 2).ToString();
        }

        #endregion

        #region Load Best Selling Products
        void LoadBestSellingProducts()
        {
            var db = AppDb.Instance;

            DataTable dt = db.TableData(@"
        SELECT 
            p.Name AS ProductName,
            SUM(si.Quantity) AS QuantitySold,
            SUM(si.Subtotal) AS RevenueGenerated
        FROM SalesItems si
        INNER JOIN Product p ON si.ProductID = p.ProductID
        GROUP BY p.Name
        HAVING SUM(si.Subtotal) >= 10000   -- optional: filter by revenue instead of quantity
        ORDER BY RevenueGenerated DESC;    -- now sorted by revenue
    ");

            // Bind to DataGridView
            dgv_best_selling.DataSource = dt;

            // Format columns
            dgv_best_selling.Columns["ProductName"].HeaderText = "Product Name";
            dgv_best_selling.Columns["QuantitySold"].HeaderText = "Quantity Sold";
            dgv_best_selling.Columns["RevenueGenerated"].HeaderText = "Revenue Generated";
            dgv_best_selling.Columns["RevenueGenerated"].DefaultCellStyle.Format = "₱0.00";
        }
        #endregion

        #region Best Selling Chart Binding
        private void BindBestSellingToCartesian()
        {
            var series = new SeriesCollection();
            var values = new ChartValues<double>();
            var labels = new List<string>();

            foreach (DataGridViewRow row in dgv_best_selling.Rows)
            {
                if (row.Cells["QuantitySold"].Value != null)
                {
                    double qty = Convert.ToDouble(row.Cells["QuantitySold"].Value);
                    values.Add(qty);

                    string product = row.Cells["ProductName"].Value?.ToString();
                    labels.Add(product);
                }
            }

            series.Add(new LineSeries
            {
                Title = "Quantity Sold",
                Values = values,
                PointGeometry = DefaultGeometries.Circle, // optional: show points
                PointGeometrySize = 10
            });

            cartesianChart1.Series = series;

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Product",
                Labels = labels
            });

            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Quantity Sold"
            });
        }



        private void BindQuantityAndRevenue()
        {
            var series = new SeriesCollection();
            var quantities = new ChartValues<double>();
            var revenues = new ChartValues<double>();
            var labels = new List<string>();

            foreach (DataGridViewRow row in dgv_best_selling.Rows)
            {
                if (row.Cells["QuantitySold"].Value != null && row.Cells["RevenueGenerated"].Value != null)
                {
                    double qty = Convert.ToDouble(row.Cells["QuantitySold"].Value);
                    double rev = Convert.ToDouble(row.Cells["RevenueGenerated"].Value);

                    quantities.Add(qty);
                    revenues.Add(rev);

                    string product = row.Cells["ProductName"].Value?.ToString();
                    labels.Add(product);
                }
            }

            series.Add(new LineSeries
            {
                Title = "Quantity Sold",
                Values = quantities,
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 8
            });

            series.Add(new LineSeries
            {
                Title = "Revenue Generated",
                Values = revenues,
                PointGeometry = DefaultGeometries.Square,
                PointGeometrySize = 8
            });

            cartesianChart1.Series = series;

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Product",
                Labels = labels
            });

            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Sales"
            });
        }

        #endregion


        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void gunaChart1_Load(object sender, EventArgs e)
        {

        }
        private void Report_form_Load(object sender, EventArgs e)
        {

        }
        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        private void dgv_best_selling_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void lbl_weekly_Click(object sender, EventArgs e)
        {

        }
        private void lbl_overall_Click(object sender, EventArgs e)
        {

        }
        private void lbl_service_Click(object sender, EventArgs e)
        {

        }
        private void lbl_average_Click(object sender, EventArgs e)
        {

        }
    }
}

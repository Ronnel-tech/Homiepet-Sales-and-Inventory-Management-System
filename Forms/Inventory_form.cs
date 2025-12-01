using Homiepet_Corner_Sales_and_Inventory_Management_System.Classes;
using red_framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homiepet_Corner_Sales_and_Inventory_Management_System.Forms
{
    public partial class Inventory_form : Form
    {
        private int _selectedId = 0; // selected id - use in Update & Delete

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ronne\source\repos\Homiepet_Corner_Sales_and_Inventory_Management_System\HomieDb.mdf;Integrated Security=True");


        public Inventory_form()
        {
            InitializeComponent();
            LoadProducts();
        }
        #region Clear Inputs
        void clearInputs()
        {
            txt_prod_name.Clear();
            txt_price.Clear();
            txt_stock.Clear();
            date_date_added.Value = DateTime.Now;
            cmbu_category.SelectedIndex = 0;
            cmbu_status.SelectedIndex = 0;
            _selectedId = 0; // reset selected id
        }
        #endregion

        #region Inventory Form Load
        private void Inventory_form_Load(object sender, EventArgs e)
        {
            var db = AppDb.Instance;

            // Load Category table with BOTH columns
            DataTable dt = db.TableData("SELECT CategoryID, CategoryName FROM Category");

            cmbu_category.DataSource = dt;
            cmbu_category.DisplayMember = "CategoryName";
            cmbu_category.ValueMember = "CategoryID";
        }
        #endregion

        #region Add Product
        private void btn_add_product_Click(object sender, EventArgs e)
        {
            var db = AppDb.Instance;

            // Check for empty inputs
            if (string.IsNullOrWhiteSpace(txt_prod_name.Text) ||
                cmbu_category.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txt_price.Text) ||
                string.IsNullOrWhiteSpace(txt_stock.Text) ||
                cmbu_status.SelectedIndex == -1)
            {
                DialogHelper.ShowInfo("Please add product information before saving.");
                return;
            }

            // Validate price
            if (!decimal.TryParse(txt_price.Text, out decimal price))
            {
                DialogHelper.ShowInfo("Invalid price. Please enter numbers only.");
                return;
            }

            // Validate stock
            if (!int.TryParse(txt_stock.Text, out int stockQty))
            {
                DialogHelper.ShowInfo("Invalid stock quantity. Please enter whole numbers only.");
                return;
            }

            if (_selectedId == 0)
            {
                // Save Product
                bool saved = db.Save("Product", new
                {
                    Name = txt_prod_name.Text,
                    CategoryID = Convert.ToInt32(cmbu_category.SelectedValue),
                    Price = price,          // SAFE
                    StockQty = stockQty,    // SAFE
                    Status = cmbu_status.Text
                });

                if (!saved)
                {
                    MessageBox.Show("Failed to save product.");
                    return;
                }

                // Get last inserted ProductID
                int productId = Convert.ToInt32(db.Scalar("SELECT MAX(ProductID) FROM Product"));

                // Save inventory log
                db.Save("InventoryLog", new
                {
                    ProductID = productId,
                    Qty_In = stockQty,
                    Qty_Out = 0,
                    Date = date_date_added.Value
                });

                DialogHelper.ShowOk("Product added successfully!");
            }

            LoadProducts();
            clearInputs();
        }
        #endregion

        #region Display Products in DataGridView
        void LoadProducts()
        {
            var db = AppDb.Instance;

            DataTable dt = db.TableData(@"
SELECT 
    p.ProductID,
    p.Name,
    p.CategoryID,
    c.CategoryName,
    p.Price,
    p.StockQty,
    p.Status,
    ISNULL(
        (SELECT TOP 1 il.Date 
         FROM InventoryLog il 
         WHERE il.ProductID = p.ProductID 
         ORDER BY il.Date DESC),
        GETDATE()
    ) AS LastStockDate
FROM Product p
JOIN Category c ON p.CategoryID = c.CategoryID
");
            dgv_inventory.DataSource = dt;

            // ⭐ Format the date column (remove time)
            dgv_inventory.Columns["LastStockDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            // Hide ProductID column so user doesn't see it
            if (dgv_inventory.Columns["ProductID"] != null)
                dgv_inventory.Columns["ProductID"].Visible = false;

            // Optional: hide CategoryID as well
            if (dgv_inventory.Columns["CategoryID"] != null)
                dgv_inventory.Columns["CategoryID"].Visible = false;
        }

        #endregion

        #region Update Product

        private void btn_update_Click(object sender, EventArgs e)
        {
            // Check for empty inputs
            if (string.IsNullOrWhiteSpace(txt_prod_name.Text) ||
                cmbu_category.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txt_price.Text) ||
                string.IsNullOrWhiteSpace(txt_stock.Text) ||
                cmbu_status.SelectedIndex == -1)
            {
                DialogHelper.ShowInfo("Please fill in all product information before updating.");
                return;
            }

            con.Open();

            // 1️⃣ Update Product
            string sql = @"
        UPDATE Product
        SET Name=@Name,
            CategoryID=@CategoryID,
            Price=@Price,
            StockQty=@StockQty,
            Status=@Status
        WHERE ProductID=@ProductID
    ";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@Name", txt_prod_name.Text);
                cmd.Parameters.AddWithValue("@CategoryID", Convert.ToInt32(cmbu_category.SelectedValue));
                cmd.Parameters.AddWithValue("@Price", decimal.Parse(txt_price.Text));
                cmd.Parameters.AddWithValue("@StockQty", int.Parse(txt_stock.Text));
                cmd.Parameters.AddWithValue("@Status", cmbu_status.Text);
                cmd.Parameters.AddWithValue("@ProductID", _selectedId);

                cmd.ExecuteNonQuery();
            }

            // 2️⃣ Update InventoryLog date for this product
            string sqlLog = @"
        UPDATE InventoryLog
        SET Date = @Date
        WHERE ProductID = @ProductID
    ";

            using (SqlCommand cmdLog = new SqlCommand(sqlLog, con))
            {
                cmdLog.Parameters.AddWithValue("@Date", date_date_added.Value);
                cmdLog.Parameters.AddWithValue("@ProductID", _selectedId);
                cmdLog.ExecuteNonQuery();
            }

            con.Close();

            DialogHelper.ShowOk("Product updated successfully!");
            LoadProducts();
            clearInputs();
        }

        #endregion

        #region DataGridView Cell Click

        private void dgv_inventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var drv = dgv_inventory.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (drv == null) return;

            //_selectedId = Convert.ToInt32(drv["ProductID"]);
            _selectedId = Convert.ToInt32(dgv_inventory.SelectedRows[0].Cells["ProductID"].Value);


            // Safely set LastStockDate
            var lastStockObj = drv["LastStockDate"];
            DateTime lastStockDate;
            if (lastStockObj != null && DateTime.TryParse(lastStockObj.ToString(), out lastStockDate))
                date_date_added.Value = lastStockDate;
            else
                date_date_added.Value = DateTime.Now;

            txt_prod_name.Text = drv["Name"]?.ToString() ?? "";
            txt_price.Text = drv["Price"]?.ToString() ?? "";
            txt_stock.Text = drv["StockQty"]?.ToString() ?? "";

            cmbu_category.SelectedValue = Convert.ToInt32(drv["CategoryID"]);
            cmbu_status.SelectedItem = drv["Status"]?.ToString() ?? "";
        }
        #endregion

        #region Clear Button Click
        private void btn_clear_Click(object sender, EventArgs e)
        {
            clearInputs();
        }
        #endregion

        #region Delete Product
        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                DialogHelper.ShowInfo("Please select a product to delete.");
                return;
            }

            var confirm = DialogHelper.ShowYesNo(
                "Are you sure you want to delete this product?\n" +
                "All related sales items and inventory logs will also be deleted.",
                "Confirm Delete"
            );

            if (confirm)
            {
                var db = AppDb.Instance;

                // ❗ Delete related SalesItems first
                db.CUD(
                    "DELETE FROM SalesItems WHERE ProductID = @Id",
                    new { Id = _selectedId }
                );

                // ❗ Delete related Inventory Logs
                db.CUD(
                    "DELETE FROM InventoryLog WHERE ProductID = @Id",
                    new { Id = _selectedId }
                );

                // ❗ Now safely delete the Product
                var res = db.CUD(
                    "DELETE FROM Product WHERE ProductID = @Id",
                    new { Id = _selectedId }
                );

                DialogHelper.ShowOk("Product deleted successfully.");

                LoadProducts();
                clearInputs();
                _selectedId = 0;
            }
        }

        #endregion

        #region Search Functionality
        private void btn_search_Click(object sender, EventArgs e)
        {
            var db = AppDb.Instance;
            string keyword = txt_search.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                LoadProducts();
                return;
            }

            string sql = @"
SELECT 
    p.ProductID,
    p.Name,
    p.CategoryID,
    c.CategoryName,
    p.Price,
    p.StockQty,
    p.Status,
    ISNULL(
        (SELECT TOP 1 il.Date 
         FROM InventoryLog il 
         WHERE il.ProductID = p.ProductID 
         ORDER BY il.Date DESC),
        GETDATE()
    ) AS LastStockDate
FROM Product p
JOIN Category c ON p.CategoryID = c.CategoryID
WHERE 
    p.Name LIKE @kw
    OR c.CategoryName LIKE @kw
    OR p.Status LIKE @kw
    OR p.Price LIKE @kw
    OR p.StockQty LIKE @kw
    OR CAST(YEAR(ISNULL(
        (SELECT TOP 1 il.Date 
         FROM InventoryLog il 
         WHERE il.ProductID = p.ProductID 
         ORDER BY il.Date DESC),
        GETDATE()
    )) AS NVARCHAR) LIKE @kw
    OR CAST(MONTH(ISNULL(
        (SELECT TOP 1 il.Date 
         FROM InventoryLog il 
         WHERE il.ProductID = p.ProductID 
         ORDER BY il.Date DESC),
        GETDATE()
    )) AS NVARCHAR) LIKE @kw
    OR CAST(DAY(ISNULL(
        (SELECT TOP 1 il.Date 
         FROM InventoryLog il 
         WHERE il.ProductID = p.ProductID 
         ORDER BY il.Date DESC),
        GETDATE()
    )) AS NVARCHAR) LIKE @kw
";

            DataTable dt = db.TableData(sql, new { kw = "%" + keyword + "%" });

            dgv_inventory.DataSource = dt;

            if (dgv_inventory.Columns["ProductID"] != null)
                dgv_inventory.Columns["ProductID"].Visible = false;

            if (dgv_inventory.Columns["CategoryID"] != null)
                dgv_inventory.Columns["CategoryID"].Visible = false;

        }
        #endregion  


        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void dgv_inventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void txt_search_TextChanged(object sender, EventArgs e)
        {

        }
        //KEY PRESS EVENTS FOR INPUT VALIDATION
        private void txt_prod_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key pressed is a digit
            if (char.IsDigit(e.KeyChar))
            {
                // Block it
                e.Handled = true;
            }

            // Optional: allow control keys like Backspace
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

        }

        private void txt_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            // Allow control keys (Backspace, Delete, etc.)
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            // Block everything else
            else
            {
                e.Handled = true;
            }

        }

        private void txt_stock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            // Allow control keys (Backspace, Delete, etc.)
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            // Block everything else
            else
            {
                e.Handled = true;
            }

        }
    }
}
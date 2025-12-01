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
    public partial class Sales_form : Form
    {
        DataTable cartTable;


        public Sales_form()
        {
            InitializeComponent();
            InitializeCartTable();


            LoadProducts();


        }

        #region Helpers
        void InitializeCartTable()
        {
            cartTable = new DataTable();

            cartTable.Columns.Add("ProductID", typeof(int));        // hidden
            cartTable.Columns.Add("Product", typeof(string));       // visible
            cartTable.Columns.Add("Quantity", typeof(int));         // visible
            cartTable.Columns.Add("Price", typeof(decimal));        // hidden
            cartTable.Columns.Add("Subtotal", typeof(decimal));     // visible

            dgv_cart.DataSource = cartTable;

            // Hide what the user shouldn't see
            dgv_cart.Columns["ProductID"].Visible = false;
            dgv_cart.Columns["Price"].Visible = false;
        }
        #endregion

        #region Load Products
        void LoadProducts()
        {
            var db = AppDb.Instance;

            DataTable dt = db.TableData(@"
        SELECT 
            ProductID,
            Name AS ProductName,
            StockQty AS AvailableStock,
            Price
        FROM Product
    ");

            dgv_product_list.DataSource = dt;

            // Adjust column headers
            dgv_product_list.Columns["ProductName"].HeaderText = "Product Name";
            dgv_product_list.Columns["AvailableStock"].HeaderText = "Available Stock";
            dgv_product_list.Columns["Price"].HeaderText = "Price";

            // Hide ProductID (needed for cart logic but user should not see it)
            dgv_product_list.Columns["ProductID"].Visible = false;
        }

        #endregion

        #region Update Total Amount
        void UpdateTotalAmount()
        {
            decimal total = 0;

            foreach (DataRow row in cartTable.Rows)
            {
                if (row["Subtotal"] != DBNull.Value)
                    total += Convert.ToDecimal(row["Subtotal"]);
            }

            lbl_total.Text = total.ToString("0.00");
        }
        #endregion

        #region Add to Cart
        private void btn_add_to_cart_Click(object sender, EventArgs e)
        {
            if (dgv_product_list.CurrentRow == null) return;

            int productId = Convert.ToInt32(dgv_product_list.CurrentRow.Cells["ProductID"].Value);
            string productName = dgv_product_list.CurrentRow.Cells["ProductName"].Value.ToString();

            object priceObj = dgv_product_list.CurrentRow.Cells["Price"].Value;
            if (priceObj == null || priceObj == DBNull.Value)
            {
                MessageBox.Show($"Price is missing for {productName}");
                return;
            }

            decimal price = Convert.ToDecimal(priceObj);

            // Check if already in cart
            DataRow existing = cartTable.Rows
                .Cast<DataRow>()
                .FirstOrDefault(r => (int)r["ProductID"] == productId);

            if (existing == null)
            {
                // Add new row
                cartTable.Rows.Add(productId, productName, 1, price, price);
            }
            else
            {
                int qty = (int)existing["Quantity"] + 1;
                existing["Quantity"] = qty;
                existing["Subtotal"] = qty * price;
            }

            UpdateTotalAmount();
        }
        #endregion

        #region Cart Cell Click
        private void dgv_cart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txt_update.Text = dgv_cart.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
        }
        #endregion

        #region Update Quantity
        private void btn_update_Click(object sender, EventArgs e)
        {
            if (dgv_cart.CurrentRow == null) return;

            if (!int.TryParse(txt_update.Text, out int qty) || qty <= 0)
            {
                DialogHelper.ShowInfo("Please enter a valid quantity (positive integer).");
                return;
            }

            DataRow row = ((DataRowView)dgv_cart.CurrentRow.DataBoundItem).Row;

            decimal price = Convert.ToDecimal(row["Price"]);  // SAFE: price always stored

            row["Quantity"] = qty;
            row["Subtotal"] = qty * price;   // ✔ Correct subtotal

            UpdateTotalAmount();
        }
        #endregion

        #region Cancel Order
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cartTable.Rows.Clear();   // ← removes all items
            UpdateTotalAmount();      // ← refresh total label
            txt_update.Clear();
        }
        #endregion

        #region Remove Item
        private void btn_remove_item_Click(object sender, EventArgs e)
        {
            if (dgv_cart.CurrentRow == null)
            {
                DialogHelper.ShowInfo("Please select an item to remove.");
                return;
            }

            // Grab the DataRow behind the selected grid row
            DataRow row = ((DataRowView)dgv_cart.CurrentRow.DataBoundItem).Row;

            // Tell the cart it no longer exists
            cartTable.Rows.Remove(row);

            UpdateTotalAmount();
        }

        #endregion

        #region Proceed Order
        private void btn_proceed_order_Click(object sender, EventArgs e)
        {
            if (cartTable.Rows.Count == 0)
            {
                DialogHelper.ShowInfo("Cart is empty. Please add items before proceeding.");
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ronne\source\repos\Homiepet_Corner_Sales_and_Inventory_Management_System\HomieDb.mdf;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Calculate total amount
                        decimal totalAmount = 0;
                        foreach (DataRow row in cartTable.Rows)
                            totalAmount += Convert.ToDecimal(row["Subtotal"]);

                        // 2. Insert into Sales and get SaleID
                        int saleId;
                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Sales (Date, TotalAmount) OUTPUT INSERTED.SaleID VALUES (@Date, @TotalAmount)", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                            saleId = (int)cmd.ExecuteScalar();
                        }

                        // 3. Loop through cart items
                        foreach (DataRow row in cartTable.Rows)
                        {
                            int productId = Convert.ToInt32(row["ProductID"]);
                            int quantity = Convert.ToInt32(row["Quantity"]);
                            decimal subtotal = Convert.ToDecimal(row["Subtotal"]);

                            // --- Check available stock ---
                            int availableStock = 0;
                            using (SqlCommand cmd = new SqlCommand(
                                "SELECT StockQty FROM Product WHERE ProductID = @ProductID", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@ProductID", productId);
                                availableStock = (int)cmd.ExecuteScalar();
                            }

                            if (quantity > availableStock)
                            {
                                DialogHelper.ShowInfo($"Cannot order {quantity} items. Only {availableStock} available in stock.");
                                tran.Rollback();
                                return; // Stop processing
                            }

                            // Insert into SalesItems
                            using (SqlCommand cmd = new SqlCommand(
                                "INSERT INTO SalesItems (SaleID, ProductID, Quantity, Subtotal) VALUES (@SaleID, @ProductID, @Quantity, @Subtotal)", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@SaleID", saleId);
                                cmd.Parameters.AddWithValue("@ProductID", productId);
                                cmd.Parameters.AddWithValue("@Quantity", quantity);
                                cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                                cmd.ExecuteNonQuery();
                            }

                            // Update Product stock
                            using (SqlCommand cmd = new SqlCommand(
                                "UPDATE Product SET StockQty = StockQty - @Qty WHERE ProductID = @ProductID", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@Qty", quantity);
                                cmd.Parameters.AddWithValue("@ProductID", productId);
                                cmd.ExecuteNonQuery();
                            }

                            // Insert into InventoryLog
                            using (SqlCommand cmd = new SqlCommand(
                                "INSERT INTO InventoryLog (ProductID, Qty_Out, Date) VALUES (@ProductID, @QtyOut, @Date)", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@ProductID", productId);
                                cmd.Parameters.AddWithValue("@QtyOut", quantity);
                                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                        DialogHelper.ShowOk("Order processed successfully!");
                        cartTable.Clear();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Error processing order: " + ex.Message);
                    }
                }
            }

            LoadProducts();
        }

        #endregion

        #region Search Products
        private void btn_search_Click(object sender, EventArgs e)
        {

            var db = AppDb.Instance;
            string keyword = txt_search.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                LoadProducts(); // Load all products if search is empty
                return;
            }

            string sql = @"
        SELECT
            p.ProductID,
            p.Name AS ProductName,
            p.Price,
            p.StockQty AS AvailableStock
        FROM Product p
        WHERE p.Name LIKE @kw
    ";

            DataTable dt = db.TableData(sql, new { kw = "%" + keyword + "%" });
            dgv_product_list.DataSource = dt;

            // Hide the ProductID column
            if (dgv_product_list.Columns["ProductID"] != null)
                dgv_product_list.Columns["ProductID"].Visible = false;

        }

        #endregion


        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        //key press event for input validation
        private void txt_update_KeyPress(object sender, KeyPressEventArgs e)
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


    


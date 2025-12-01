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
    public partial class Pet_Services_form : Form
    {
        private int _selectedID = 0;

        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Homiepet_Corner_DB;Integrated Security=True");
        public Pet_Services_form()
        {
            InitializeComponent();

            LoadAppointments();

        }

            #region Clear Fields
            void ClearFields()
            {
                txt_fname.Clear();
                txt_lname.Clear();
                txt_contact.Clear();
                txt_barangay.Clear();
                txt_province.Clear();
                cmbu_type.SelectedIndex = 0;
                cmbu_status.SelectedIndex = 0;
                txt_price.Clear();
                date_date.Value = DateTime.Now;
                _selectedID = 0;
            }

            #endregion

        #region ComboBox Service Type Selection Changed
        private void cmbu_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbu_type.SelectedIndex != -1)
            {
                DataRowView row = (DataRowView)cmbu_type.SelectedItem;
                decimal price = Convert.ToDecimal(row["Price"]);
                txt_price.Text = price.ToString("0.00");
            }
            else
            {
                txt_price.Clear();
            }
        }

        #endregion

        #region Load Appointments into DataGridView
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

            // Optional: Format columns
            dgv_appointment.Columns["Price"].DefaultCellStyle.Format = "N2"; // two decimal places
            dgv_appointment.Columns["DateBooked"].DefaultCellStyle.Format = "yyyy-MM-dd"; // formatted date
        }
        #endregion

        #region Form Load - Populate Service Types
        private void Pet_Services_form_Load(object sender, EventArgs e)
        {
            var db = AppDb.Instance;

            // Get all services from the database
            DataTable dt = db.TableData("SELECT ServiceID, ServiceName, Price FROM Services");

            cmbu_type.DataSource = dt;           // Bind the list
            cmbu_type.DisplayMember = "ServiceName";   // What the user sees
            cmbu_type.ValueMember = "ServiceID";   // The ID we can use in code
            cmbu_type.SelectedIndex = -1;
        }
        #endregion

        #region Add Appointment Button Click
        private void btn_add_product_Click(object sender, EventArgs e)
        {
            // 🔍 VALIDATION — check for blank fields
            if (string.IsNullOrWhiteSpace(txt_fname.Text) ||
                string.IsNullOrWhiteSpace(txt_lname.Text) ||
                string.IsNullOrWhiteSpace(txt_contact.Text) ||
                string.IsNullOrWhiteSpace(txt_barangay.Text) ||
                string.IsNullOrWhiteSpace(txt_province.Text) ||
                cmbu_type.SelectedIndex == -1 ||
                cmbu_status.SelectedIndex == -1)
            {
                DialogHelper.ShowInfo("Please fill in all customer and appointment information before saving.");
                return;
            }

            var db = AppDb.Instance;

            if (_selectedID == 0)
            {
                bool saved = db.Save("Customers", new
                {
                    FirstName = txt_fname.Text,
                    LastName = txt_lname.Text,
                    Contact = txt_contact.Text,
                    Barangay = txt_barangay.Text,
                    Province = txt_province.Text,
                });

                if (!saved)
                {
                    MessageBox.Show("Failed to save customer.");
                    return;
                }

                int CustomerID = Convert.ToInt32(db.Scalar("SELECT MAX(CustomerID) FROM Customers"));

                db.Save("Appointments", new
                {
                    CustomerID = CustomerID,
                    ServiceID = Convert.ToInt32(cmbu_type.SelectedValue),
                    DateBooked = date_date.Value,
                    Status = cmbu_status.Text
                });

                DialogHelper.ShowOk("Appointment added successfully!");

                LoadAppointments();
                ClearFields();
            }
        }

        #endregion

        #region Appointment Cell Click

        private void dgv_appointment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgv_appointment.Rows[e.RowIndex];

            _selectedID = Convert.ToInt32(row.Cells["AppointID"].Value);

            txt_fname.Text = row.Cells["FirstName"].Value.ToString();
            txt_lname.Text = row.Cells["LastName"].Value.ToString();
            txt_contact.Text = row.Cells["Contact"].Value.ToString();
            txt_barangay.Text = row.Cells["Barangay"].Value.ToString();
            txt_province.Text = row.Cells["Province"].Value.ToString();

            cmbu_type.Text = row.Cells["ServiceType"].Value.ToString();
            txt_price.Text = Convert.ToDecimal(row.Cells["Price"].Value).ToString("0.00");

            cmbu_status.Text = row.Cells["Status"].Value.ToString();

            date_date.Value = Convert.ToDateTime(row.Cells["DateBooked"].Value);
        }

        #endregion

        #region Update Appointment Button Click
        private void btn_update_Click(object sender, EventArgs e)
        {
            if (dgv_appointment.CurrentRow == null)
            {
                DialogHelper.ShowInfo("Please select an appointment to update.");
                return;
            }

            // Check if fields are empty
            if (string.IsNullOrWhiteSpace(txt_fname.Text) ||
                string.IsNullOrWhiteSpace(txt_lname.Text) ||
                string.IsNullOrWhiteSpace(txt_contact.Text) ||
                string.IsNullOrWhiteSpace(txt_barangay.Text) ||
                string.IsNullOrWhiteSpace(txt_province.Text) ||
                cmbu_type.SelectedValue == null ||
                string.IsNullOrWhiteSpace(cmbu_status.Text))
            {
                DialogHelper.ShowInfo("Please fill in all required fields before updating.");
                return;
            }

            // Get selected row
            DataGridViewRow row = dgv_appointment.CurrentRow;
            int customerID = Convert.ToInt32(row.Cells["CustomerID"].Value);
            int appointID = Convert.ToInt32(row.Cells["AppointID"].Value);

            try
            {
                using (SqlConnection con = new SqlConnection(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ronne\source\repos\Homiepet_Corner_Sales_and_Inventory_Management_System\HomieDb.mdf;Integrated Security=True"))
                {
                    con.Open();

                    // Update Customers
                    string updateCustomer = @"
                UPDATE Customers
                SET FirstName = @FirstName,
                    LastName = @LastName,
                    Contact = @Contact,
                    Barangay = @Barangay,
                    Province = @Province
                WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(updateCustomer, con))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", txt_fname.Text);
                        cmd.Parameters.AddWithValue("@LastName", txt_lname.Text);
                        cmd.Parameters.AddWithValue("@Contact", txt_contact.Text);
                        cmd.Parameters.AddWithValue("@Barangay", txt_barangay.Text);
                        cmd.Parameters.AddWithValue("@Province", txt_province.Text);
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        cmd.ExecuteNonQuery();
                    }

                    // Update Appointments
                    string updateAppointment = @"
                UPDATE Appointments
                SET ServiceID = @ServiceID,
                    DateBooked = @DateBooked,
                    Status = @Status
                WHERE AppointID = @AppointID";

                    using (SqlCommand cmd = new SqlCommand(updateAppointment, con))
                    {
                        cmd.Parameters.AddWithValue("@ServiceID", Convert.ToInt32(cmbu_type.SelectedValue));
                        cmd.Parameters.AddWithValue("@DateBooked", date_date.Value);
                        cmd.Parameters.AddWithValue("@Status", cmbu_status.Text);
                        cmd.Parameters.AddWithValue("@AppointID", appointID);

                        cmd.ExecuteNonQuery();
                    }

                    DialogHelper.ShowOk("Appointment updated successfully!");
                }

                // Refresh DataGridView
                LoadAppointments();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        #endregion

        #region Delete Appointment Button Click
        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (_selectedID == 0)
            {
                DialogHelper.ShowInfo("Please select an appointment to delete.");
                return;
            }
            var confirm = DialogHelper.ShowYesNo("Are you sure you want to delete this appointment?");

            if (confirm)
            {
                var db = AppDb.Instance;
                var res = db.CUD(
                    "DELETE FROM Appointments WHERE AppointID = @Id",
                    new { Id = _selectedID }
                );
                DialogHelper.ShowOk("Appointment deleted successfully.");
                LoadAppointments();
                ClearFields();
                _selectedID = 0;
            }
        }
        #endregion

        #region Clear Fields Button Click
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        #endregion

        #region Search Appointments
        private void btn_search_Click(object sender, EventArgs e)
        {
            var db = AppDb.Instance;

            string keyword = txt_search.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                // If search box is empty, load all appointments
                LoadAppointments();
                return;
            }

            // Use parameterized query to avoid SQL issues
            string sql = @"
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
        WHERE 
            c.FirstName LIKE @kw
            OR c.LastName LIKE @kw
            OR s.ServiceName LIKE @kw
        ORDER BY a.DateBooked DESC
    ";

            DataTable dt = db.TableData(sql, new { kw = "%" + keyword + "%" });

            dgv_appointment.DataSource = dt;

            // Hide IDs and sensitive columns
            dgv_appointment.Columns["CustomerID"].Visible = false;
            dgv_appointment.Columns["ServiceID"].Visible = false;
            dgv_appointment.Columns["Contact"].Visible = false;
            dgv_appointment.Columns["Barangay"].Visible = false;
            dgv_appointment.Columns["Province"].Visible = false;
            dgv_appointment.Columns["AppointID"].Visible = false;

            // Optional formatting
            dgv_appointment.Columns["Price"].DefaultCellStyle.Format = "N2";
            dgv_appointment.Columns["DateBooked"].DefaultCellStyle.Format = "yyyy-MM-dd";
        }

        #endregion




        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void cmbu_status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void dgv_appointment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        // Key Press Events for Input Validation
        private void txt_fname_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_lname_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_barangay_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_province_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_contact_KeyPress(object sender, KeyPressEventArgs e)
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



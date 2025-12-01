using Homiepet_Corner_Sales_and_Inventory_Management_System.Classes;
using Homiepet_Corner_Sales_and_Inventory_Management_System.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homiepet_Corner_Sales_and_Inventory_Management_System
{
    public partial class Login : Form
    {

        
        public Login()
        {
            InitializeComponent();
        }

        #region Login Button Click Event
        private void btn_login_Click(object sender, EventArgs e)
        {
            Mainform mainform = new Mainform();

            string savedUsername = UserCredentials.Username;
            string savedPassword = UserCredentials.Password;

            if (txt_username.Text == savedUsername && txt_password.Text == savedPassword)
            {
                this.Hide();
                mainform.Show();
            }
            else if (string.IsNullOrWhiteSpace(txt_username.Text) || string.IsNullOrWhiteSpace(txt_password.Text))
            {
                DialogHelper.ShowInfo("Please enter your username and password.");
            }
            else if (txt_username.Text != savedUsername)
            {
                DialogHelper.ShowInfo("Invalid Username, Try Again");
            }
            else
            {
                DialogHelper.ShowInfo("Invalid Password, Try Again");
            }
        }

        #endregion

        #region Forgot Password Button Click Event
        private void btn_forgotPassword_Click(object sender, EventArgs e)
        {
            // Open the Forgot Password form
            ForgotPasswordForm forgotForm = new ForgotPasswordForm();
            forgotForm.ShowDialog();
        }
        #endregion

        #region Form Load Event
        private void Login_Load(object sender, EventArgs e)
        {
            txt_password.UseSystemPasswordChar = true;

        }
        #endregion


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotPasswordForm = new ForgotPasswordForm();
            forgotPasswordForm.Show();
            

        }
     }
}


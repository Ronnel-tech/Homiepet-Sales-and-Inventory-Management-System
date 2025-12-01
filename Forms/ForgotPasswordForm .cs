using Homiepet_Corner_Sales_and_Inventory_Management_System.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homiepet_Corner_Sales_and_Inventory_Management_System.Forms
{
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
        }
        #region Change Password Functionality
        private void btn_new_password_Click(object sender, EventArgs e)
        {
            if (txt_newPassword.Text == "" || txt_confirmPassword.Text == "")
            {
                DialogHelper.ShowInfo("Please enter and confirm your new password.");
                return;
            }

            if (txt_newPassword.Text != txt_confirmPassword.Text)
            {
                DialogHelper.ShowInfo("Passwords do not match.");
                return;
            }

            // ✅ Save to Settings via UserCredentials
            UserCredentials.Password = txt_newPassword.Text;

            DialogHelper.ShowInfo("Password successfully changed!");

            //UserCredentials.Password = txt_newPassword.Text;
            //MessageBox.Show("Saved password: " + UserCredentials.Password);
            this.Close();
        }
        #endregion

        #region Form Load Event
        private void ForgotPasswordForm_Load(object sender, EventArgs e)
        {
            txt_newPassword.UseSystemPasswordChar = true;
            txt_confirmPassword.UseSystemPasswordChar = true;
        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void txt_confirmPassword_TextChanged(object sender, EventArgs e)
        {

        }


    }
}

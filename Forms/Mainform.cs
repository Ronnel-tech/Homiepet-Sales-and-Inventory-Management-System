using Guna.UI2.WinForms;
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
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();

        }

        #region Child Form Handling

        private Form activeForm = null;
            
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.MdiParent = this;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }

        #endregion

        private void Mainform_Load(object sender, EventArgs e)
        {
            OpenChildForm(new Dashboard_form());
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Pet_Services_form());
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            
            OpenChildForm(new Dashboard_form());
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Sales_form());
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Inventory_form());
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Report_form());
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

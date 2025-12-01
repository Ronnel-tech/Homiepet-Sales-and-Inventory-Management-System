namespace Homiepet_Corner_Sales_and_Inventory_Management_System.Forms
{
    partial class Pet_Services_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_appoint = new Guna.UI2.WinForms.Guna2Button();
            this.date_date = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cmbu_status = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbu_type = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_price = new Guna.UI2.WinForms.Guna2TextBox();
            this.txt_fname = new Guna.UI2.WinForms.Guna2TextBox();
            this.btn_delete = new Guna.UI2.WinForms.Guna2Button();
            this.dgv_appointment = new Guna.UI2.WinForms.Guna2DataGridView();
            this.txt_search = new Guna.UI2.WinForms.Guna2TextBox();
            this.btn_update = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.btn_search = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txt_barangay = new Guna.UI2.WinForms.Guna2TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_contact = new Guna.UI2.WinForms.Guna2TextBox();
            this.txt_lname = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_province = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_appointment)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_appoint
            // 
            this.btn_appoint.Animated = true;
            this.btn_appoint.AutoRoundedCorners = true;
            this.btn_appoint.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_appoint.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_appoint.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_appoint.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_appoint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_appoint.ForeColor = System.Drawing.Color.White;
            this.btn_appoint.Location = new System.Drawing.Point(667, 559);
            this.btn_appoint.Name = "btn_appoint";
            this.btn_appoint.Size = new System.Drawing.Size(159, 32);
            this.btn_appoint.TabIndex = 47;
            this.btn_appoint.Text = "Appoint";
            this.btn_appoint.Click += new System.EventHandler(this.btn_add_product_Click);
            // 
            // date_date
            // 
            this.date_date.Animated = true;
            this.date_date.AutoRoundedCorners = true;
            this.date_date.Checked = true;
            this.date_date.FillColor = System.Drawing.Color.White;
            this.date_date.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.date_date.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.date_date.Location = new System.Drawing.Point(576, 420);
            this.date_date.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.date_date.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.date_date.Name = "date_date";
            this.date_date.Size = new System.Drawing.Size(250, 39);
            this.date_date.TabIndex = 46;
            this.date_date.Value = new System.DateTime(2025, 11, 20, 2, 49, 37, 795);
            this.date_date.ValueChanged += new System.EventHandler(this.guna2DateTimePicker1_ValueChanged);
            // 
            // cmbu_status
            // 
            this.cmbu_status.AutoRoundedCorners = true;
            this.cmbu_status.BackColor = System.Drawing.Color.Transparent;
            this.cmbu_status.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbu_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbu_status.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbu_status.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbu_status.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbu_status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbu_status.ItemHeight = 30;
            this.cmbu_status.Items.AddRange(new object[] {
            "In Progress",
            "Completed",
            "Pending"});
            this.cmbu_status.Location = new System.Drawing.Point(573, 484);
            this.cmbu_status.Name = "cmbu_status";
            this.cmbu_status.Size = new System.Drawing.Size(253, 36);
            this.cmbu_status.TabIndex = 45;
            this.cmbu_status.SelectedIndexChanged += new System.EventHandler(this.cmbu_status_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(573, 462);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 16);
            this.label9.TabIndex = 43;
            this.label9.Text = "Status";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(573, 398);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 16);
            this.label8.TabIndex = 42;
            this.label8.Text = "Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(573, 339);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 16);
            this.label7.TabIndex = 41;
            this.label7.Text = "Price";
            // 
            // cmbu_type
            // 
            this.cmbu_type.AutoRoundedCorners = true;
            this.cmbu_type.BackColor = System.Drawing.Color.Transparent;
            this.cmbu_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbu_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbu_type.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbu_type.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbu_type.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbu_type.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbu_type.ItemHeight = 30;
            this.cmbu_type.Items.AddRange(new object[] {
            "Pet Grooming",
            "Pet Boarding",
            "Pet Sitting",
            "Dog Walking",
            "Pet Daycare",
            "Vet Service"});
            this.cmbu_type.Location = new System.Drawing.Point(573, 300);
            this.cmbu_type.Name = "cmbu_type";
            this.cmbu_type.Size = new System.Drawing.Size(253, 36);
            this.cmbu_type.TabIndex = 40;
            this.cmbu_type.SelectedIndexChanged += new System.EventHandler(this.cmbu_type_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(573, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 16);
            this.label6.TabIndex = 39;
            this.label6.Text = "Type";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(573, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 31;
            this.label5.Text = "First Name";
            // 
            // txt_price
            // 
            this.txt_price.Animated = true;
            this.txt_price.AutoRoundedCorners = true;
            this.txt_price.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_price.DefaultText = "";
            this.txt_price.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_price.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_price.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_price.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_price.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_price.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_price.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_price.Location = new System.Drawing.Point(573, 357);
            this.txt_price.Name = "txt_price";
            this.txt_price.PlaceholderText = "";
            this.txt_price.ReadOnly = true;
            this.txt_price.SelectedText = "";
            this.txt_price.Size = new System.Drawing.Size(253, 34);
            this.txt_price.TabIndex = 37;
            // 
            // txt_fname
            // 
            this.txt_fname.Animated = true;
            this.txt_fname.AutoRoundedCorners = true;
            this.txt_fname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_fname.DefaultText = "";
            this.txt_fname.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_fname.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_fname.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_fname.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_fname.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_fname.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_fname.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_fname.Location = new System.Drawing.Point(576, 127);
            this.txt_fname.Name = "txt_fname";
            this.txt_fname.PlaceholderText = "";
            this.txt_fname.SelectedText = "";
            this.txt_fname.Size = new System.Drawing.Size(116, 34);
            this.txt_fname.TabIndex = 36;
            this.txt_fname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_fname_KeyPress);
            // 
            // btn_delete
            // 
            this.btn_delete.Animated = true;
            this.btn_delete.AutoRoundedCorners = true;
            this.btn_delete.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_delete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_delete.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_delete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_delete.FillColor = System.Drawing.Color.Red;
            this.btn_delete.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_delete.ForeColor = System.Drawing.Color.White;
            this.btn_delete.Location = new System.Drawing.Point(225, 559);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(159, 32);
            this.btn_delete.TabIndex = 35;
            this.btn_delete.Text = "Delete";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // dgv_appointment
            // 
            this.dgv_appointment.AllowUserToAddRows = false;
            this.dgv_appointment.AllowUserToDeleteRows = false;
            this.dgv_appointment.AllowUserToResizeColumns = false;
            this.dgv_appointment.AllowUserToResizeRows = false;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv_appointment.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle25;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_appointment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this.dgv_appointment.ColumnHeadersHeight = 30;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_appointment.DefaultCellStyle = dataGridViewCellStyle27;
            this.dgv_appointment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_appointment.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv_appointment.Location = new System.Drawing.Point(15, 107);
            this.dgv_appointment.Name = "dgv_appointment";
            this.dgv_appointment.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_appointment.RowHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.dgv_appointment.RowHeadersVisible = false;
            this.dgv_appointment.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_appointment.Size = new System.Drawing.Size(532, 434);
            this.dgv_appointment.TabIndex = 33;
            this.dgv_appointment.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv_appointment.ThemeStyle.AlternatingRowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_appointment.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv_appointment.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv_appointment.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv_appointment.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgv_appointment.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv_appointment.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgv_appointment.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_appointment.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_appointment.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgv_appointment.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_appointment.ThemeStyle.HeaderStyle.Height = 30;
            this.dgv_appointment.ThemeStyle.ReadOnly = false;
            this.dgv_appointment.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv_appointment.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv_appointment.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_appointment.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv_appointment.ThemeStyle.RowsStyle.Height = 22;
            this.dgv_appointment.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv_appointment.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv_appointment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_appointment_CellClick);
            this.dgv_appointment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_appointment_CellContentClick);
            // 
            // txt_search
            // 
            this.txt_search.Animated = true;
            this.txt_search.AutoRoundedCorners = true;
            this.txt_search.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_search.DefaultText = "";
            this.txt_search.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_search.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_search.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_search.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_search.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_search.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_search.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_search.Location = new System.Drawing.Point(648, 5);
            this.txt_search.Name = "txt_search";
            this.txt_search.PlaceholderText = "Search Pet Service";
            this.txt_search.SelectedText = "";
            this.txt_search.Size = new System.Drawing.Size(185, 32);
            this.txt_search.TabIndex = 4;
            // 
            // btn_update
            // 
            this.btn_update.Animated = true;
            this.btn_update.AutoRoundedCorners = true;
            this.btn_update.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_update.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_update.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_update.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_update.FillColor = System.Drawing.Color.SpringGreen;
            this.btn_update.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_update.ForeColor = System.Drawing.Color.Black;
            this.btn_update.Location = new System.Drawing.Point(390, 559);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(159, 32);
            this.btn_update.TabIndex = 34;
            this.btn_update.Text = "Update";
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.Coral;
            this.guna2Panel2.BorderRadius = 100;
            this.guna2Panel2.Controls.Add(this.btn_search);
            this.guna2Panel2.Controls.Add(this.txt_search);
            this.guna2Panel2.CustomBorderColor = System.Drawing.Color.Black;
            this.guna2Panel2.Location = new System.Drawing.Point(6, 50);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(840, 41);
            this.guna2Panel2.TabIndex = 32;
            // 
            // btn_search
            // 
            this.btn_search.Animated = true;
            this.btn_search.BackColor = System.Drawing.Color.White;
            this.btn_search.BorderColor = System.Drawing.Color.Transparent;
            this.btn_search.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_search.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_search.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_search.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_search.FillColor = System.Drawing.Color.Transparent;
            this.btn_search.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_search.ForeColor = System.Drawing.Color.Transparent;
            this.btn_search.Image = global::Homiepet_Corner_Sales_and_Inventory_Management_System.Properties.Resources.search_957;
            this.btn_search.Location = new System.Drawing.Point(797, 11);
            this.btn_search.Name = "btn_search";
            this.btn_search.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btn_search.Size = new System.Drawing.Size(23, 22);
            this.btn_search.TabIndex = 4;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Coral;
            this.guna2Panel1.BorderRadius = 100;
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.CustomBorderColor = System.Drawing.Color.Black;
            this.guna2Panel1.Location = new System.Drawing.Point(6, 6);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(840, 38);
            this.guna2Panel1.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pet Care Hub";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 24;
            this.guna2Elipse1.TargetControl = this.guna2Panel1;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.BorderRadius = 24;
            this.guna2Elipse2.TargetControl = this.guna2Panel2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(570, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 49;
            this.label1.Text = "Barangay";
            // 
            // txt_barangay
            // 
            this.txt_barangay.Animated = true;
            this.txt_barangay.AutoRoundedCorners = true;
            this.txt_barangay.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_barangay.DefaultText = "";
            this.txt_barangay.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_barangay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_barangay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_barangay.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_barangay.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_barangay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_barangay.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_barangay.Location = new System.Drawing.Point(576, 185);
            this.txt_barangay.Name = "txt_barangay";
            this.txt_barangay.PlaceholderText = "";
            this.txt_barangay.SelectedText = "";
            this.txt_barangay.Size = new System.Drawing.Size(116, 34);
            this.txt_barangay.TabIndex = 50;
            this.txt_barangay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_barangay_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(573, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 16);
            this.label10.TabIndex = 51;
            this.label10.Text = "Contact Number";
            // 
            // txt_contact
            // 
            this.txt_contact.Animated = true;
            this.txt_contact.AutoRoundedCorners = true;
            this.txt_contact.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_contact.DefaultText = "";
            this.txt_contact.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_contact.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_contact.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_contact.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_contact.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_contact.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_contact.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_contact.Location = new System.Drawing.Point(576, 244);
            this.txt_contact.Name = "txt_contact";
            this.txt_contact.PlaceholderText = "";
            this.txt_contact.SelectedText = "";
            this.txt_contact.Size = new System.Drawing.Size(253, 34);
            this.txt_contact.TabIndex = 52;
            this.txt_contact.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_contact_KeyPress);
            // 
            // txt_lname
            // 
            this.txt_lname.Animated = true;
            this.txt_lname.AutoRoundedCorners = true;
            this.txt_lname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_lname.DefaultText = "";
            this.txt_lname.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_lname.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_lname.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_lname.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_lname.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_lname.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_lname.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_lname.Location = new System.Drawing.Point(698, 127);
            this.txt_lname.Name = "txt_lname";
            this.txt_lname.PlaceholderText = "";
            this.txt_lname.SelectedText = "";
            this.txt_lname.Size = new System.Drawing.Size(131, 34);
            this.txt_lname.TabIndex = 53;
            this.txt_lname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_lname_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(695, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 54;
            this.label3.Text = "Last Name";
            // 
            // txt_province
            // 
            this.txt_province.Animated = true;
            this.txt_province.AutoRoundedCorners = true;
            this.txt_province.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_province.DefaultText = "";
            this.txt_province.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_province.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_province.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_province.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_province.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_province.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_province.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_province.Location = new System.Drawing.Point(698, 185);
            this.txt_province.Name = "txt_province";
            this.txt_province.PlaceholderText = "";
            this.txt_province.SelectedText = "";
            this.txt_province.Size = new System.Drawing.Size(128, 34);
            this.txt_province.TabIndex = 55;
            this.txt_province.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_province_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(695, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 56;
            this.label4.Text = "Province";
            // 
            // guna2Button1
            // 
            this.guna2Button1.Animated = true;
            this.guna2Button1.AutoRoundedCorners = true;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Coral;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(576, 559);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(85, 32);
            this.guna2Button1.TabIndex = 57;
            this.guna2Button1.Text = "Clear";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // guna2Elipse3
            // 
            this.guna2Elipse3.BorderRadius = 24;
            this.guna2Elipse3.TargetControl = this.dgv_appointment;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = global::Homiepet_Corner_Sales_and_Inventory_Management_System.Properties.Resources.ser;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(-110, 439);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(329, 257);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 48;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.UseTransparentBackground = true;
            this.guna2PictureBox1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // Pet_Services_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 632);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_province);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_lname);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_contact);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_barangay);
            this.Controls.Add(this.btn_appoint);
            this.Controls.Add(this.date_date);
            this.Controls.Add(this.cmbu_status);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbu_type);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_price);
            this.Controls.Add(this.txt_fname);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.dgv_appointment);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.guna2PictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Pet_Services_form";
            this.Text = "Pet_Services";
            this.Load += new System.EventHandler(this.Pet_Services_form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_appointment)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btn_appoint;
        private Guna.UI2.WinForms.Guna2DateTimePicker date_date;
        private Guna.UI2.WinForms.Guna2ComboBox cmbu_status;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2ComboBox cmbu_type;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox txt_price;
        private Guna.UI2.WinForms.Guna2TextBox txt_fname;
        private Guna.UI2.WinForms.Guna2Button btn_delete;
        private Guna.UI2.WinForms.Guna2DataGridView dgv_appointment;
        private Guna.UI2.WinForms.Guna2CircleButton btn_search;
        private Guna.UI2.WinForms.Guna2TextBox txt_search;
        private Guna.UI2.WinForms.Guna2Button btn_update;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txt_barangay;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2TextBox txt_contact;
        private Guna.UI2.WinForms.Guna2TextBox txt_lname;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txt_province;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
    }
}
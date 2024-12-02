namespace QLHD_CLB
{
    partial class FormThanhVien
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.thanhVienTableAdapter = new QLHD_CLB.QuanLyCauLacBoDataSetTableAdapters.ThanhVienTableAdapter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_lamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btn_themThanhVien = new Guna.UI2.WinForms.Guna2Button();
            this.btn_sua = new Guna.UI2.WinForms.Guna2Button();
            this.btn_luu = new Guna.UI2.WinForms.Guna2Button();
            this.btn_xoa = new Guna.UI2.WinForms.Guna2Button();
            this.dtg_hdthamgia = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.dtg_DSTV = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.inputMaTV = new System.Windows.Forms.Label();
            this.comboBox_DSBan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comboBox_TrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.radioButton_Nu = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radioButton_Nam = new Guna.UI2.WinForms.Guna2RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.inputDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.inputSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.inputEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.inputHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_locBan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.inputTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_timkiem = new Guna.UI2.WinForms.Guna2Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.guna2MessageDialog1 = new Guna.UI2.WinForms.Guna2MessageDialog();
            this.thanhVienBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_hdthamgia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_DSTV)).BeginInit();
            this.guna2GroupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thanhVienBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // thanhVienTableAdapter
            // 
            this.thanhVienTableAdapter.ClearBeforeFill = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.hScrollBar1);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.dtg_hdthamgia);
            this.panel2.Controls.Add(this.guna2GroupBox2);
            this.panel2.Controls.Add(this.dtg_DSTV);
            this.panel2.Controls.Add(this.guna2GroupBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.comboBox_locBan);
            this.panel2.Controls.Add(this.inputTimKiem);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.btn_timkiem);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1223, 910);
            this.panel2.TabIndex = 0;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(840, 12);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(8, 8);
            this.hScrollBar1.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_lamMoi);
            this.groupBox1.Controls.Add(this.btn_themThanhVien);
            this.groupBox1.Controls.Add(this.btn_sua);
            this.groupBox1.Controls.Add(this.btn_luu);
            this.groupBox1.Controls.Add(this.btn_xoa);
            this.groupBox1.Location = new System.Drawing.Point(425, 747);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(787, 100);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thao tác";
            // 
            // btn_lamMoi
            // 
            this.btn_lamMoi.BorderRadius = 10;
            this.btn_lamMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_lamMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_lamMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_lamMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_lamMoi.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_lamMoi.ForeColor = System.Drawing.Color.White;
            this.btn_lamMoi.Location = new System.Drawing.Point(6, 39);
            this.btn_lamMoi.Name = "btn_lamMoi";
            this.btn_lamMoi.Size = new System.Drawing.Size(90, 45);
            this.btn_lamMoi.TabIndex = 24;
            this.btn_lamMoi.Text = "Làm mới";
            this.btn_lamMoi.Click += new System.EventHandler(this.btn_lamMoi_Click);
            // 
            // btn_themThanhVien
            // 
            this.btn_themThanhVien.Animated = true;
            this.btn_themThanhVien.BorderRadius = 10;
            this.btn_themThanhVien.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_themThanhVien.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_themThanhVien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_themThanhVien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_themThanhVien.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_themThanhVien.ForeColor = System.Drawing.Color.White;
            this.btn_themThanhVien.Location = new System.Drawing.Point(171, 39);
            this.btn_themThanhVien.Name = "btn_themThanhVien";
            this.btn_themThanhVien.Size = new System.Drawing.Size(90, 45);
            this.btn_themThanhVien.TabIndex = 20;
            this.btn_themThanhVien.Text = "Thêm";
            this.btn_themThanhVien.Click += new System.EventHandler(this.btn_themThanhVien_Click);
            // 
            // btn_sua
            // 
            this.btn_sua.BorderRadius = 10;
            this.btn_sua.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_sua.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_sua.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_sua.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_sua.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sua.ForeColor = System.Drawing.Color.White;
            this.btn_sua.Location = new System.Drawing.Point(333, 39);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(90, 45);
            this.btn_sua.TabIndex = 21;
            this.btn_sua.Text = "Sửa";
            this.btn_sua.Click += new System.EventHandler(this.btn_sua_Click);
            // 
            // btn_luu
            // 
            this.btn_luu.BorderRadius = 10;
            this.btn_luu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_luu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_luu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_luu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_luu.Enabled = false;
            this.btn_luu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_luu.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_luu.ForeColor = System.Drawing.Color.White;
            this.btn_luu.Location = new System.Drawing.Point(500, 39);
            this.btn_luu.Name = "btn_luu";
            this.btn_luu.Size = new System.Drawing.Size(90, 45);
            this.btn_luu.TabIndex = 23;
            this.btn_luu.Text = "Lưu";
            this.btn_luu.Click += new System.EventHandler(this.btn_luu_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.BorderRadius = 10;
            this.btn_xoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_xoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_xoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_xoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_xoa.FillColor = System.Drawing.Color.Red;
            this.btn_xoa.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoa.ForeColor = System.Drawing.Color.White;
            this.btn_xoa.Location = new System.Drawing.Point(664, 39);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(90, 45);
            this.btn_xoa.TabIndex = 22;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // dtg_hdthamgia
            // 
            this.dtg_hdthamgia.AllowUserToResizeColumns = false;
            this.dtg_hdthamgia.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dtg_hdthamgia.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtg_hdthamgia.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtg_hdthamgia.ColumnHeadersHeight = 30;
            this.dtg_hdthamgia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtg_hdthamgia.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtg_hdthamgia.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dtg_hdthamgia.Location = new System.Drawing.Point(425, 467);
            this.dtg_hdthamgia.Name = "dtg_hdthamgia";
            this.dtg_hdthamgia.RowHeadersVisible = false;
            this.dtg_hdthamgia.RowHeadersWidth = 40;
            this.dtg_hdthamgia.Size = new System.Drawing.Size(787, 233);
            this.dtg_hdthamgia.TabIndex = 0;
            this.dtg_hdthamgia.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dtg_hdthamgia.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dtg_hdthamgia.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dtg_hdthamgia.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dtg_hdthamgia.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dtg_hdthamgia.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dtg_hdthamgia.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dtg_hdthamgia.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtg_hdthamgia.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dtg_hdthamgia.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtg_hdthamgia.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dtg_hdthamgia.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dtg_hdthamgia.ThemeStyle.HeaderStyle.Height = 30;
            this.dtg_hdthamgia.ThemeStyle.ReadOnly = false;
            this.dtg_hdthamgia.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dtg_hdthamgia.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtg_hdthamgia.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtg_hdthamgia.ThemeStyle.RowsStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dtg_hdthamgia.ThemeStyle.RowsStyle.Height = 22;
            this.dtg_hdthamgia.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dtg_hdthamgia.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // guna2GroupBox2
            // 
            this.guna2GroupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.guna2GroupBox2.CustomBorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.guna2GroupBox2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox2.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox2.Location = new System.Drawing.Point(425, 424);
            this.guna2GroupBox2.Name = "guna2GroupBox2";
            this.guna2GroupBox2.Size = new System.Drawing.Size(787, 40);
            this.guna2GroupBox2.TabIndex = 25;
            this.guna2GroupBox2.Text = "Hoạt động tham gia";
            // 
            // dtg_DSTV
            // 
            this.dtg_DSTV.AllowUserToResizeColumns = false;
            this.dtg_DSTV.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dtg_DSTV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtg_DSTV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtg_DSTV.ColumnHeadersHeight = 40;
            this.dtg_DSTV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtg_DSTV.DefaultCellStyle = dataGridViewCellStyle6;
            this.dtg_DSTV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dtg_DSTV.Location = new System.Drawing.Point(16, 99);
            this.dtg_DSTV.Name = "dtg_DSTV";
            this.dtg_DSTV.RowHeadersVisible = false;
            this.dtg_DSTV.Size = new System.Drawing.Size(1196, 309);
            this.dtg_DSTV.TabIndex = 17;
            this.dtg_DSTV.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dtg_DSTV.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dtg_DSTV.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dtg_DSTV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dtg_DSTV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dtg_DSTV.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dtg_DSTV.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dtg_DSTV.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtg_DSTV.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dtg_DSTV.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtg_DSTV.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dtg_DSTV.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dtg_DSTV.ThemeStyle.HeaderStyle.Height = 40;
            this.dtg_DSTV.ThemeStyle.ReadOnly = false;
            this.dtg_DSTV.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dtg_DSTV.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtg_DSTV.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtg_DSTV.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dtg_DSTV.ThemeStyle.RowsStyle.Height = 22;
            this.dtg_DSTV.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dtg_DSTV.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dtg_DSTV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtg_DSTV_CellClick);
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.Controls.Add(this.inputMaTV);
            this.guna2GroupBox1.Controls.Add(this.comboBox_DSBan);
            this.guna2GroupBox1.Controls.Add(this.comboBox_TrangThai);
            this.guna2GroupBox1.Controls.Add(this.radioButton_Nu);
            this.guna2GroupBox1.Controls.Add(this.radioButton_Nam);
            this.guna2GroupBox1.Controls.Add(this.panel6);
            this.guna2GroupBox1.Controls.Add(this.label3);
            this.guna2GroupBox1.Controls.Add(this.label4);
            this.guna2GroupBox1.Controls.Add(this.inputDiaChi);
            this.guna2GroupBox1.Controls.Add(this.label5);
            this.guna2GroupBox1.Controls.Add(this.inputSoDienThoai);
            this.guna2GroupBox1.Controls.Add(this.label6);
            this.guna2GroupBox1.Controls.Add(this.inputEmail);
            this.guna2GroupBox1.Controls.Add(this.label7);
            this.guna2GroupBox1.Controls.Add(this.label15);
            this.guna2GroupBox1.Controls.Add(this.inputHoTen);
            this.guna2GroupBox1.Controls.Add(this.label16);
            this.guna2GroupBox1.CustomBorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox1.Location = new System.Drawing.Point(16, 424);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(381, 423);
            this.guna2GroupBox1.TabIndex = 20;
            this.guna2GroupBox1.Text = "Thông tin thành viên";
            // 
            // inputMaTV
            // 
            this.inputMaTV.AutoSize = true;
            this.inputMaTV.Location = new System.Drawing.Point(269, 102);
            this.inputMaTV.Name = "inputMaTV";
            this.inputMaTV.Size = new System.Drawing.Size(0, 20);
            this.inputMaTV.TabIndex = 38;
            this.inputMaTV.Visible = false;
            // 
            // comboBox_DSBan
            // 
            this.comboBox_DSBan.BackColor = System.Drawing.Color.Transparent;
            this.comboBox_DSBan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox_DSBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DSBan.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBox_DSBan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBox_DSBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBox_DSBan.ForeColor = System.Drawing.Color.Black;
            this.comboBox_DSBan.ItemHeight = 30;
            this.comboBox_DSBan.Location = new System.Drawing.Point(117, 356);
            this.comboBox_DSBan.Name = "comboBox_DSBan";
            this.comboBox_DSBan.Size = new System.Drawing.Size(238, 36);
            this.comboBox_DSBan.TabIndex = 37;
            // 
            // comboBox_TrangThai
            // 
            this.comboBox_TrangThai.BackColor = System.Drawing.Color.Transparent;
            this.comboBox_TrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox_TrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TrangThai.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBox_TrangThai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBox_TrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBox_TrangThai.ForeColor = System.Drawing.Color.Black;
            this.comboBox_TrangThai.ItemHeight = 30;
            this.comboBox_TrangThai.Location = new System.Drawing.Point(117, 314);
            this.comboBox_TrangThai.Name = "comboBox_TrangThai";
            this.comboBox_TrangThai.Size = new System.Drawing.Size(238, 36);
            this.comboBox_TrangThai.TabIndex = 36;
            // 
            // radioButton_Nu
            // 
            this.radioButton_Nu.AutoSize = true;
            this.radioButton_Nu.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_Nu.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton_Nu.CheckedState.BorderThickness = 0;
            this.radioButton_Nu.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton_Nu.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioButton_Nu.CheckedState.InnerOffset = -4;
            this.radioButton_Nu.Location = new System.Drawing.Point(182, 96);
            this.radioButton_Nu.Name = "radioButton_Nu";
            this.radioButton_Nu.Size = new System.Drawing.Size(47, 24);
            this.radioButton_Nu.TabIndex = 35;
            this.radioButton_Nu.Tag = "Nữ";
            this.radioButton_Nu.Text = "Nữ";
            this.radioButton_Nu.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioButton_Nu.UncheckedState.BorderThickness = 2;
            this.radioButton_Nu.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioButton_Nu.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radioButton_Nu.UseVisualStyleBackColor = false;
            // 
            // radioButton_Nam
            // 
            this.radioButton_Nam.AutoSize = true;
            this.radioButton_Nam.BackColor = System.Drawing.Color.Transparent;
            this.radioButton_Nam.Checked = true;
            this.radioButton_Nam.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton_Nam.CheckedState.BorderThickness = 0;
            this.radioButton_Nam.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioButton_Nam.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radioButton_Nam.CheckedState.InnerOffset = -4;
            this.radioButton_Nam.Location = new System.Drawing.Point(117, 96);
            this.radioButton_Nam.Name = "radioButton_Nam";
            this.radioButton_Nam.Size = new System.Drawing.Size(59, 24);
            this.radioButton_Nam.TabIndex = 34;
            this.radioButton_Nam.TabStop = true;
            this.radioButton_Nam.Tag = "Nam";
            this.radioButton_Nam.Text = "Nam";
            this.radioButton_Nam.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioButton_Nam.UncheckedState.BorderThickness = 2;
            this.radioButton_Nam.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radioButton_Nam.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radioButton_Nam.UseVisualStyleBackColor = false;
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(562, 518);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(8, 8);
            this.panel6.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(15, 356);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 30;
            this.label3.Text = "Chọn ban";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 314);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 28;
            this.label4.Text = "Trạng thái";
            // 
            // inputDiaChi
            // 
            this.inputDiaChi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.inputDiaChi.DefaultText = "";
            this.inputDiaChi.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.inputDiaChi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.inputDiaChi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputDiaChi.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputDiaChi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputDiaChi.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputDiaChi.ForeColor = System.Drawing.Color.Black;
            this.inputDiaChi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputDiaChi.Location = new System.Drawing.Point(117, 228);
            this.inputDiaChi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.inputDiaChi.Multiline = true;
            this.inputDiaChi.Name = "inputDiaChi";
            this.inputDiaChi.PasswordChar = '\0';
            this.inputDiaChi.PlaceholderText = "Nhập địa chỉ";
            this.inputDiaChi.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.inputDiaChi.SelectedText = "";
            this.inputDiaChi.Size = new System.Drawing.Size(238, 78);
            this.inputDiaChi.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(12, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 20);
            this.label5.TabIndex = 26;
            this.label5.Text = "Địa chỉ";
            // 
            // inputSoDienThoai
            // 
            this.inputSoDienThoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.inputSoDienThoai.DefaultText = "";
            this.inputSoDienThoai.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.inputSoDienThoai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.inputSoDienThoai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputSoDienThoai.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputSoDienThoai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputSoDienThoai.ForeColor = System.Drawing.Color.Black;
            this.inputSoDienThoai.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputSoDienThoai.Location = new System.Drawing.Point(117, 183);
            this.inputSoDienThoai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.inputSoDienThoai.Name = "inputSoDienThoai";
            this.inputSoDienThoai.PasswordChar = '\0';
            this.inputSoDienThoai.PlaceholderText = "Nhập số điện thoại";
            this.inputSoDienThoai.SelectedText = "";
            this.inputSoDienThoai.Size = new System.Drawing.Size(238, 35);
            this.inputSoDienThoai.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(12, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 20);
            this.label6.TabIndex = 24;
            this.label6.Text = "Số điện thoại";
            // 
            // inputEmail
            // 
            this.inputEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.inputEmail.DefaultText = "";
            this.inputEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.inputEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.inputEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputEmail.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputEmail.ForeColor = System.Drawing.Color.Black;
            this.inputEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputEmail.Location = new System.Drawing.Point(117, 140);
            this.inputEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.inputEmail.Name = "inputEmail";
            this.inputEmail.PasswordChar = '\0';
            this.inputEmail.PlaceholderText = "Nhập email";
            this.inputEmail.SelectedText = "";
            this.inputEmail.Size = new System.Drawing.Size(238, 35);
            this.inputEmail.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(13, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "Email";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(13, 96);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 20);
            this.label15.TabIndex = 20;
            this.label15.Text = "Giới tính";
            // 
            // inputHoTen
            // 
            this.inputHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.inputHoTen.DefaultText = "";
            this.inputHoTen.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.inputHoTen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.inputHoTen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputHoTen.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputHoTen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputHoTen.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputHoTen.ForeColor = System.Drawing.Color.Black;
            this.inputHoTen.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputHoTen.Location = new System.Drawing.Point(117, 45);
            this.inputHoTen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.inputHoTen.Name = "inputHoTen";
            this.inputHoTen.PasswordChar = '\0';
            this.inputHoTen.PlaceholderText = "Nhập họ tên";
            this.inputHoTen.SelectedText = "";
            this.inputHoTen.Size = new System.Drawing.Size(238, 35);
            this.inputHoTen.TabIndex = 19;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(12, 51);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 20);
            this.label16.TabIndex = 18;
            this.label16.Text = "Họ tên";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(903, 12);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.label1.Size = new System.Drawing.Size(59, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bộ lọc";
            // 
            // comboBox_locBan
            // 
            this.comboBox_locBan.BackColor = System.Drawing.Color.Transparent;
            this.comboBox_locBan.BorderRadius = 10;
            this.comboBox_locBan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox_locBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_locBan.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBox_locBan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBox_locBan.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.comboBox_locBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBox_locBan.ItemHeight = 30;
            this.comboBox_locBan.Location = new System.Drawing.Point(968, 9);
            this.comboBox_locBan.Name = "comboBox_locBan";
            this.comboBox_locBan.Size = new System.Drawing.Size(244, 36);
            this.comboBox_locBan.TabIndex = 19;
            this.comboBox_locBan.SelectedIndexChanged += new System.EventHandler(this.comboBox_locBan_SelectedIndexChanged);
            // 
            // inputTimKiem
            // 
            this.inputTimKiem.BorderRadius = 10;
            this.inputTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.inputTimKiem.DefaultText = "";
            this.inputTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.inputTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.inputTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.inputTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputTimKiem.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.inputTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.inputTimKiem.Location = new System.Drawing.Point(16, 4);
            this.inputTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputTimKiem.Name = "inputTimKiem";
            this.inputTimKiem.PasswordChar = '\0';
            this.inputTimKiem.PlaceholderText = "Tìm kiếm theo họ tên, số điện thoại, địa chỉ ";
            this.inputTimKiem.SelectedText = "";
            this.inputTimKiem.Size = new System.Drawing.Size(535, 40);
            this.inputTimKiem.TabIndex = 8;
            this.inputTimKiem.TextChanged += new System.EventHandler(this.inputTimKiem_TextChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(16, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1196, 52);
            this.panel3.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Danh sách thành viên";
            // 
            // btn_timkiem
            // 
            this.btn_timkiem.BorderRadius = 10;
            this.btn_timkiem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_timkiem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_timkiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_timkiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_timkiem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_timkiem.ForeColor = System.Drawing.Color.White;
            this.btn_timkiem.Location = new System.Drawing.Point(557, 4);
            this.btn_timkiem.Name = "btn_timkiem";
            this.btn_timkiem.Size = new System.Drawing.Size(103, 40);
            this.btn_timkiem.TabIndex = 10;
            this.btn_timkiem.Text = "Tìm kiếm";
            this.btn_timkiem.Click += new System.EventHandler(this.btn_timkiem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1229, 925);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // guna2MessageDialog1
            // 
            this.guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            this.guna2MessageDialog1.Caption = null;
            this.guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.None;
            this.guna2MessageDialog1.Parent = null;
            this.guna2MessageDialog1.Style = Guna.UI2.WinForms.MessageDialogStyle.Default;
            this.guna2MessageDialog1.Text = null;
            // 
            // FormThanhVien
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1230, 922);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormThanhVien";
            this.Text = "FormThanhVien";
            this.Load += new System.EventHandler(this.FormThanhVien_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtg_hdthamgia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_DSTV)).EndInit();
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.thanhVienBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource thanhVienBindingSource;
        private QuanLyCauLacBoDataSetTableAdapters.ThanhVienTableAdapter thanhVienTableAdapter;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2DataGridView dtg_DSTV;
        private Guna.UI2.WinForms.Guna2Button btn_themThanhVien;
        private Guna.UI2.WinForms.Guna2Button btn_xoa;
        private Guna.UI2.WinForms.Guna2Button btn_luu;
        private Guna.UI2.WinForms.Guna2Button btn_sua;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox comboBox_locBan;
        private Guna.UI2.WinForms.Guna2TextBox inputTimKiem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button btn_timkiem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Button btn_lamMoi;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox2;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private System.Windows.Forms.Label inputMaTV;
        private Guna.UI2.WinForms.Guna2ComboBox comboBox_DSBan;
        private Guna.UI2.WinForms.Guna2ComboBox comboBox_TrangThai;
        private Guna.UI2.WinForms.Guna2RadioButton radioButton_Nu;
        private Guna.UI2.WinForms.Guna2RadioButton radioButton_Nam;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox inputDiaChi;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox inputSoDienThoai;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox inputEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label15;
        private Guna.UI2.WinForms.Guna2TextBox inputHoTen;
        private System.Windows.Forms.Label label16;
        private Guna.UI2.WinForms.Guna2DataGridView dtg_hdthamgia;
        private System.Windows.Forms.GroupBox groupBox1;
        private Guna.UI2.WinForms.Guna2MessageDialog guna2MessageDialog1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}
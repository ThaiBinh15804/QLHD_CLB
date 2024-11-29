using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLHD_CLB.Model;

namespace QLHD_CLB
{
    public partial class FormNhaTaiTroCu : Form
    {
        public FormNhaTaiTroCu()
        {
            InitializeComponent();
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormNhaTaiTro_Load(object sender, EventArgs e)
        {
            DBConnect data = new DBConnect();

            string sql = "SELECT * FROM NhaTaiTro;";
            DataTable dt = data.getSqlDataAdapter(sql);
            dgvDSNTT.DataSource = dt;

            sql = "SELECT DISTINCT DiaChi FROM NhaTaiTro";
            cbDiaChi.DataSource = data.getSqlDataAdapter(sql);
            cbDiaChi.DisplayMember = "DiaChi";
            cbDiaChi.ValueMember = "DiaChi";

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }


    }
}

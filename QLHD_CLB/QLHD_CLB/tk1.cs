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

namespace QLHD_CLB
{
    public partial class tk1 : Form
    {

        public tk1()
        {
            InitializeComponent();
        }

        private DataTable GetReportData()
        {
            using (SqlConnection conn = new SqlConnection("Data Source = THAIBINH-LAPTOP; Initial Catalog = QuanLyCauLacBo; User ID = sa; Password = 123"))
            {
                conn.Open();
                string query = @"
            SELECT SuKien.TenSuKien, 
                   FORMAT(NgayThucHien, 'yyyy-MM') AS Thang, 
                   SUM(DuChi) AS TongDuChi, 
                   SUM(ThucChi) AS TongThucChi,
                   SuKien.DiaDiem
            FROM ChiTieu
            INNER JOIN SuKien ON ChiTieu.MaSuKien = SuKien.MaSuKien
            GROUP BY FORMAT(NgayThucHien, 'yyyy-MM'), SuKien.TenSuKien, SuKien.DiaDiem";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private void ShowReport()
        {
            try
            {
                // Gọi hàm GetReportData để lấy dữ liệu
                DataTable reportData = GetReportData();

                // Tạo đối tượng báo cáo và gán dữ liệu
                CrystalReport1 rpt = new CrystalReport1();
                rpt.SetDataSource(reportData); // Gán DataTable vào báo cáo

                // Cấu hình viewer và gán báo cáo
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.DisplayStatusBar = false;
                crystalReportViewer1.DisplayToolbar = true;
                crystalReportViewer1.ShowGroupTreeButton = false;
                crystalReportViewer1.EnableDrillDown = false;

                // Ẩn Tool Panel nếu cần
                crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị báo cáo: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Gọi hàm ShowReport khi form được load
        private void tk1_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

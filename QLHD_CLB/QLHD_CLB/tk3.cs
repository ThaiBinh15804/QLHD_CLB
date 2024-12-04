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
    public partial class tk3 : Form
    {
        public tk3()
        {
            InitializeComponent();
        }

        private DataTable GetReportData()
        {
            using (SqlConnection conn = new SqlConnection("Data Source = PHAMTHUAN\\MSSQLSERVER01; Initial Catalog = QuanLyCauLacBo; User ID = sa; Password = 123"))
            {
                conn.Open();

                // Tạo SqlCommand để gọi Stored Procedure
                SqlCommand cmd = new SqlCommand("sp_GetReportData", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Tạo SqlDataAdapter để lấy dữ liệu từ Stored Procedure
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                // Tạo DataTable để lưu kết quả
                DataTable dtCombined = new DataTable();

                // Điền dữ liệu từ SqlDataAdapter vào DataTable
                da.Fill(dtCombined);

                return dtCombined;
            }
        }

        private void ShowReport()
        {
            try
            {
                // Gọi hàm GetReportData để lấy dữ liệu
                DataTable reportData = GetReportData();

                // Tạo đối tượng báo cáo và gán dữ liệu
                CrystalReport3 rpt = new CrystalReport3();
                rpt.SetDataSource(reportData); // Gán DataTable vào báo cáo

                // Cấu hình viewer và gán báo cáo
                crystalReportViewer3.ReportSource = rpt;
                crystalReportViewer3.DisplayStatusBar = false;
                crystalReportViewer3.DisplayToolbar = true;
                crystalReportViewer3.ShowGroupTreeButton = false;
                crystalReportViewer3.EnableDrillDown = false;

                // Ẩn Tool Panel nếu cần
                crystalReportViewer3.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị báo cáo: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tk3_Load(object sender, EventArgs e)
        {
            ShowReport();
        }
    }
}

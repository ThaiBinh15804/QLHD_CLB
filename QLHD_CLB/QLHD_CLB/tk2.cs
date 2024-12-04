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
    public partial class tk2 : Form
    {
        public tk2()
        {
            InitializeComponent();
        }

        private DataTable GetReportData()
        {
            using (SqlConnection conn = new SqlConnection("Data Source = PHAMTHUAN\\MSSQLSERVER01; Initial Catalog = QuanLyCauLacBo; User ID = sa; Password = 123"))
            {
                conn.Open();

                // Tạo SqlCommand để gọi Stored Procedure
                SqlCommand cmd = new SqlCommand("GetFinancialSummary", conn);
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
                CrystalReport2 rpt = new CrystalReport2();
                rpt.SetDataSource(reportData); // Gán DataTable vào báo cáo

                // Cấu hình viewer và gán báo cáo
                crystalReportViewer2.ReportSource = rpt;
                crystalReportViewer2.DisplayStatusBar = false;
                crystalReportViewer2.DisplayToolbar = true;
                crystalReportViewer2.ShowGroupTreeButton = false;
                crystalReportViewer2.EnableDrillDown = false;

                // Ẩn Tool Panel nếu cần
                crystalReportViewer2.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị báo cáo: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tk2_Load(object sender, EventArgs e)
        {
            ShowReport();
        }
    }
}

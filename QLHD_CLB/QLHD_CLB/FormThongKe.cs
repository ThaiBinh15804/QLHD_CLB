using QLHD_CLB.Model;
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
using Guna.Charts.WinForms;

namespace QLHD_CLB
{
    public partial class FormThongKe : Form
    {
        private FormGiaoDien parent;

        public FormThongKe()
        {
            InitializeComponent();
        }

        public FormThongKe(FormGiaoDien _parentForm)
        {
            InitializeComponent();
            parent = _parentForm;
        }

        DBConnect db = new DBConnect();

        private void ThongKeSoThanhVien()
        {
            string query = "SELECT COUNT(*) FROM ThanhVien";
            int k = int.Parse(db.getScalar(query).ToString());
            txtThongKeThanhVien.Text = k.ToString();
        }

        private void ThongKeSoNhaTaiTro()
        {
            string query = "SELECT COUNT(*) FROM NhaTaiTro";
            int k = int.Parse(db.getScalar(query).ToString());
            txtThongKeNhaTaiTro.Text = k.ToString();
        }

        private void ThongKeSuKien()
        {
            string query = "SELECT COUNT(*) FROM SuKien";
            int k = int.Parse(db.getScalar(query).ToString());
            txtThongKeSuKien.Text = k.ToString();
        }

        private void ThongKeBan()
        {
            string query = "SELECT COUNT(*) FROM Ban";
            int k = int.Parse(db.getScalar(query).ToString());
            txtThongKeBan.Text = k.ToString();
        }

        private void LoadDataToGunaChart()
        {
            // Kết nối tới SQL Server
            string connectionString = @"Data Source = THAIBINH-LAPTOP; Initial Catalog = QuanLyCauLacBo; User ID = sa; Password = 123";
            string query = @"
        SELECT 
            FORMAT(NgayThucHien, 'yyyy-MM') AS Thang, 
            SUM(DuChi) AS TongDuChi, 
            SUM(ThucChi) AS TongThucChi
        FROM 
            ChiTieu
        GROUP BY 
            FORMAT(NgayThucHien, 'yyyy-MM')
        ORDER BY 
            Thang";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                // Tạo danh sách màu cho từng tháng
                Dictionary<string, Color> monthColors = new Dictionary<string, Color>();
                Random random = new Random();

                // Tạo dataset cho Guna Chart
                GunaBarDataset duChiDataset = new GunaBarDataset
                {
                    Label = "Dự Chi",
                    FillColors = new ColorCollection() // Khởi tạo đúng kiểu
                };

                GunaBarDataset thucChiDataset = new GunaBarDataset
                {
                    Label = "Thực Chi",
                    FillColors = new ColorCollection() // Khởi tạo đúng kiểu
                };

                // Thêm dữ liệu vào datasets
                while (reader.Read())
                {
                    string thang = reader["Thang"].ToString();
                    double tongDuChi = Convert.ToDouble(reader["TongDuChi"]);
                    double tongThucChi = Convert.ToDouble(reader["TongThucChi"]);

                    // Tạo màu sắc cố định cho mỗi tháng
                    if (!monthColors.ContainsKey(thang))
                    {
                        monthColors[thang] = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    }

                    // Thêm dữ liệu vào dataset
                    duChiDataset.DataPoints.Add(thang, tongDuChi);
                    thucChiDataset.DataPoints.Add(thang, tongThucChi);

                    // Đặt màu giống nhau cho cùng một tháng
                    duChiDataset.FillColors.Add(monthColors[thang]); // Màu tháng cho Dự Chi
                    thucChiDataset.FillColors.Add(Color.FromArgb(
                        Math.Max(0, monthColors[thang].R - 50),
                        Math.Max(0, monthColors[thang].G - 50),
                        Math.Max(0, monthColors[thang].B - 50))); // Màu tối hơn cho Thực Chi
                }

                // Gán datasets vào chart
                gunaChart1.Datasets.Clear();
                gunaChart1.Datasets.Add(duChiDataset);
                gunaChart1.Datasets.Add(thucChiDataset);

                // Cấu hình biểu đồ
                gunaChart1.XAxes.Display = true;
                gunaChart1.YAxes.Display = true;
                gunaChart1.Update(); // Cập nhật giao diện
            }
        }

        private void LocThanhVienTheoThang()
        {
            string query = "Select distinct FORMAT(NgayThamGia, 'yyyy-MM') as ThangNam from ThanhVien where NgayThamGia >= DATEADD(MONTH, -6, GETDATE()) order by ThangNam DESC";
            DataTable dt= db.getSqlDataAdapter(query);
            comboBoxlocThanhVienTheoThang.DataSource = dt;
            comboBoxlocThanhVienTheoThang.DisplayMember = "ThangNam";
            comboBoxlocThanhVienTheoThang.ValueMember = "ThangNam";
            if (dt.Rows.Count > 0)
            {
                comboBoxlocThanhVienTheoThang.SelectedIndex = 0;  // Chọn tháng gần nhất
            }
        }

        private void HienThiThanhVienTheoThang()
        {
            string query = "SELECT ThanhVien.HoTen as N'Họ tên', ThanhVien.GioiTinh as N'Giới tính', Ban.TenBan as N'Thuộc ban' FROM ThanhVien JOIN Ban ON Ban.MaBan = ThanhVien.MaBan WHERE FORMAT(ThanhVien.NgayThamGia, 'yyyy-MM') = (SELECT TOP 1 FORMAT(NgayThamGia, 'yyyy-MM') FROM ThanhVien WHERE NgayThamGia >= DATEADD(MONTH, -6, GETDATE()) ORDER BY NgayThamGia DESC);";
            DataTable dt = db.getSqlDataAdapter(query);
            dtg_thongkeThanhVienThamGia.DataSource = dt;

        }

        private void LocHienThiThanhVienTheoThang(string thangNam)
        {
            string query = " SELECT ThanhVien.HoTen as N'Họ tên', ThanhVien.GioiTinh as N'Giới tính', Ban.TenBan as N'Thuộc ban' FROM ThanhVien JOIN Ban ON Ban.MaBan = ThanhVien.MaBan WHERE FORMAT(ThanhVien.NgayThamGia, 'yyyy-MM') ='" + thangNam + "'";
            DataTable dt = db.getSqlDataAdapter(query);
            dtg_thongkeThanhVienThamGia.DataSource = dt;

        }

        private void comboBoxlocThanhVienTheoThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxlocThanhVienTheoThang.SelectedIndex != -1)
            {
                // Lấy tháng đã chọn
                string thangNam = comboBoxlocThanhVienTheoThang.SelectedValue.ToString();

                // Cập nhật danh sách thành viên theo tháng đã chọn
                LocHienThiThanhVienTheoThang(thangNam);
            }
        }

        public bool checkEmpty(DataTable dataTable)
        {
            return dataTable.Rows.Count > 0;
        }

        public void ChartPie(GunaChart chart, DataTable data, string nameChart)
        {
            if (data != null && data.Rows.Count > 0)  // Kiểm tra dữ liệu có rỗng không
            {
                // Xóa dữ liệu cũ trong biểu đồ
                chart.Datasets.Clear();
                chart.Legend.Position = LegendPosition.Right;  // Đặt vị trí Legend
                chart.Legend.Display = true;  // Hiển thị Legend
                chart.XAxes.Display = false;  // Ẩn trục X
                chart.YAxes.Display = false;  // Ẩn trục Y
                chart.Title.Text = nameChart;  // Đặt tên biểu đồ

                // Tạo một dataset cho Pie chart
                var dataset = new GunaPieDataset();

                // Duyệt qua dữ liệu để thêm vào dataset
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    // Thêm mỗi điểm dữ liệu vào dataset (nhãn và giá trị)
                    dataset.DataPoints.Add(
                        Convert.ToString(data.Rows[i]["GioiTinh"]),  // Thêm nhãn (Giới tính)
                        Convert.ToDouble(data.Rows[i]["SoLuong"])    // Thêm giá trị (Số lượng)
                    );
                }

                // Thêm dataset vào biểu đồ
                chart.Datasets.Add(dataset);
            }
            else
            {
                MessageBox.Show("Dữ liệu không hợp lệ hoặc rỗng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ChartHorizontalBar(GunaChart chart, DataTable data, string nameChart)
        {
            if (checkEmpty(data))
            {
                chart.Datasets.Clear();
                //Chart configuration 
                chart.Legend.Display = false;
                chart.XAxes.Display = true;
                chart.YAxes.Display = true;
                chart.Title.Text = nameChart;

                var dataset = new GunaHorizontalBarDataset();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    dataset.DataPoints.Add(
                      Convert.ToString(data.Rows[i][0]),
                      Convert.ToDouble(data.Rows[i][1])
                      );
                }
                chart.Datasets.Add(dataset);
            }
            else
                MessageBox.Show("Данных не достаточно.", "Ошибка");
        }

        private void ThongKeGioiTinh()
        {
            // Câu truy vấn SQL để lấy dữ liệu thống kê giới tính
            string query = @"SELECT 
                     GioiTinh,
                     COUNT(*) AS SoLuong
                     FROM ThanhVien
                     GROUP BY GioiTinh";
            DataTable dt = db.getSqlDataAdapter(query);
            
            ChartPie(gunaChart2, dt, "Thống kê giới tính thành viên");
        }

        private void ThongKeDongQuy()
        {
            string query = @"
                SELECT 
                    khdq.TenKeHoach AS N'Tên kế hoạch',
                    SUM(khdq.SoTienCanDong) AS N'Tổng số tiền đã đóng'
                from KeHoachDongQuy khdq 
                join DongQuy dq on khdq.MaKeHoach = dq.MaKeHoach 
                join ThanhVien tv on dq.MaThanhVien = tv.MaThanhVien 
                where dq.TrangThai IS NOT NULL
                    AND dq.TrangThai != 0
                    AND dq.NgayDong IS NOT NULL
                GROUP BY 
                    khdq.MaKeHoach, khdq.TenKeHoach;";

            DataTable dt = db.getSqlDataAdapter(query);
            ChartHorizontalBar(gunaChart3, dt, "Thống kê các kế hoạch đóng quỹ");
        }

        private void ThongKeSuKienSapDienRaHoacDangDienRa()
        {
            string query = "SELECT SuKien.TenSuKien as N'Tên sự kiện', SuKien.NgayBatDau as N'Ngày bắt đầu', SuKien.NgayKetThuc as N'Ngày kết thúc', SuKien.NganSachDuChi as N'Ngân sách dự chi', SuKien.ChiTieuThucTe as N'Ngân sách thực chi', SuKien.DiaDiem as N'Địa điểm', STRING_AGG(NhaTaiTro.TenNhaTaiTro, ', ') AS N'Nhà tài trợ' FROM SuKien JOIN TaiTro ON TaiTro.MaSuKien = SuKien.MaSuKien JOIN NhaTaiTro ON TaiTro.MaNhaTaiTro = NhaTaiTro.MaNhaTaiTro WHERE (NgayBatDau <= GETDATE() AND NgayKetThuc >= GETDATE()) OR (NgayBatDau > GETDATE()) GROUP BY SuKien.MaSuKien, SuKien.TenSuKien, SuKien.MoTa, SuKien.NgayBatDau, SuKien.NgayKetThuc, SuKien.NganSachDuChi, SuKien.ChiTieuThucTe, SuKien.DiaDiem;";
            DataTable dt = db.getSqlDataAdapter(query);
            dtg_dsSuKien.DataSource = dt;
            dtg_dsSuKien.Columns[0].ReadOnly = true;
            dtg_dsSuKien.Columns[1].ReadOnly = true;
            dtg_dsSuKien.Columns[2].ReadOnly = true;
            dtg_dsSuKien.Columns[3].ReadOnly = true;
            dtg_dsSuKien.Columns[4].ReadOnly = true;
            dtg_dsSuKien.Columns[5].ReadOnly = true;
            dtg_dsSuKien.Columns[6].ReadOnly = true;
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            ThongKeSoThanhVien();
            ThongKeSoNhaTaiTro();
            ThongKeSuKien();
            ThongKeBan();
            LoadDataToGunaChart();

            LocThanhVienTheoThang();
            HienThiThanhVienTheoThang();
            ThongKeGioiTinh();
            ThongKeSuKienSapDienRaHoacDangDienRa();
            ThongKeDongQuy();
        }

        private void btn_tk1_Click(object sender, EventArgs e)
        {
            if (GlobalValue.ChucVu_NguoiDung == "CV004" || GlobalValue.ChucVu_NguoiDung == "CV005")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                parent.container(new tk1());
            }
        }
    }
}

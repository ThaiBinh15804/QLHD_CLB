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
using System.IO;
using System.Data.SqlClient;

namespace QLHD_CLB
{
    public partial class FormNguoiDungCLB : Form
    {
        bool flat = false;
        DBConnect db = new DBConnect();
        public FormNguoiDungCLB()
        {
            InitializeComponent();
        }

        private void HienThiDS()
        {
            string chuoi="select * from NguoiDung";
            DataTable dt = db.getSqlDataAdapter(chuoi);
            dgvDSNguoiDung.DataSource = dt;

            string chuoi2 = "select DISTINCT TrangThai from NguoiDung";
            DataTable dt2 = db.getSqlDataAdapter(chuoi2);
            cbbTrangThai.DataSource = dt2;
            cbbTrangThai.ValueMember = "TrangThai";
            cbbTrangThai.SelectedIndex = -1;

            cbbLoc_TT.DataSource = dt2;
            cbbLoc_TT.ValueMember = "TrangThai";
            cbbLoc_TT.SelectedIndex = -1;
        }

        private void FormNguoiDungCLB_Load(object sender, EventArgs e)
        {
            string maNguoiDung = SinhMaNguoiDung();
            if (maNguoiDung != null)
            {
                txtMaNguoiDung.Text = maNguoiDung;
            }
            HienThiDS();
        }

        private string SinhMaNguoiDung()
        {
            // Câu lệnh SQL để lấy mã người dùng lớn nhất
            string query = "SELECT MAX(CAST(SUBSTRING(MaNguoiDung, 3, LEN(MaNguoiDung)) AS INT)) FROM NguoiDung";

            try
            {
                // Lấy kết quả trả về từ CSDL
                int maCuoi = db.getScalar(query);

                // Tạo mã người dùng mới (Cộng thêm 1 vào mã lớn nhất)
                int maMoi = maCuoi + 1;

                // Đảm bảo mã mới có dạng ND001, ND002, ...
                string maNguoiDungMoi = "ND" + maMoi.ToString("D3");  // D3 đảm bảo có 3 chữ số

                return maNguoiDungMoi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy mã người dùng: " + ex.Message);
                return null;
            }
        }


        private bool KTMaNguoiDung(string mand)
        {
            string m = "select count(*) from NguoiDung where MaNguoiDung='" + mand + "'";
            int kq = db.getScalar(m);
            if(kq==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string manguoidung = txtMaNguoiDung.Text.Trim();
            string hoten = txtHoTen.Text.Trim();
            string tentk = txtTenTK.Text.Trim();
            string matkhau = txtMatKhau.Text.Trim();
            string anhdaidien = txtAnhDaiDien.Text.Trim();
            string trangthai = cbbTrangThai.SelectedIndex != -1 ? cbbTrangThai.SelectedValue.ToString().Trim() : ""; // Kiểm tra SelectedIndex

            // Kiểm tra dữ liệu đầu vào không rỗng
            if (string.IsNullOrEmpty(manguoidung) || string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(tentk) || string.IsNullOrEmpty(matkhau) || string.IsNullOrEmpty(anhdaidien) || string.IsNullOrEmpty(trangthai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Kiểm tra xem mã người dùng đã tồn tại chưa
            if (!KTMaNguoiDung(manguoidung))
            {
                MessageBox.Show("Mã người dùng đã tồn tại.");
                return;
            }

            // Kiểm tra tính hợp lệ của tên tài khoản và mật khẩu (ví dụ không chứa ký tự đặc biệt)
            if (tentk.Contains(" ") || matkhau.Contains(" "))
            {
                MessageBox.Show("Tên tài khoản và mật khẩu không được chứa khoảng trắng.");
                return;
            }

            try
            {
                // Câu lệnh SQL để thêm người dùng
                string them = "INSERT INTO NguoiDung (MaNguoiDung, HoTen, TenTaiKhoan, MatKhau, AnhDaiDien, TrangThai) " +
                              "VALUES ('" + manguoidung + "', N'" + hoten + "', N'" + tentk + "', N'" + matkhau + "', N'" + anhdaidien + "', N'" + trangthai + "')";

                // Thực thi câu lệnh SQL
                int kq = db.getNonQuery(them);
                if (kq == 0)
                {
                    MessageBox.Show("Thêm người dùng không thành công.");
                }
                else
                {
                    MessageBox.Show("Thêm người dùng thành công.");
                    HienThiDS(); // Cập nhật lại danh sách người dùng
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
            string maNguoiDung = SinhMaNguoiDung();
            if (maNguoiDung != null)
            {
                txtMaNguoiDung.Text = maNguoiDung;
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu cả Mã Người Dùng và Họ Tên đều rỗng
            if (string.IsNullOrEmpty(txtMaNguoiDung.Text) && string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Người Dùng hoặc Họ Tên để xóa.");
                return;
            }

            // Câu lệnh SQL để xóa người dùng
            string xoa = "delete from NguoiDung where MaNguoiDung=N'" + txtMaNguoiDung.Text.Trim() + "' or HoTen=N'" + txtHoTen.Text.Trim() + "'";

            try
            {
                int kq = db.getNonQuery(xoa);  // Thực thi câu lệnh SQL
                if (kq == 0)
                {
                    MessageBox.Show("Xóa người dùng không thành công.");
                }
                else
                {
                    MessageBox.Show("Xóa người dùng thành công.");
                    HienThiDS();  // Cập nhật lại danh sách người dùng
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }

            // Xóa các trường nhập liệu sau khi xóa thành công
            txtHoTen.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtAnhDaiDien.Clear();
            cbbTrangThai.SelectedIndex = -1;
            string maNguoiDung = SinhMaNguoiDung();
            if (maNguoiDung != null)
            {
                txtMaNguoiDung.Text = maNguoiDung;
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            flat = true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            flat = false;
            txtHoTen.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtAnhDaiDien.Clear();
            cbbTrangThai.SelectedIndex = -1;
            string maNguoiDung = SinhMaNguoiDung();
            if (maNguoiDung != null)
            {
                txtMaNguoiDung.Text = maNguoiDung;
            }
        }

        private void dgvDSNguoiDung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvDSNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && flat==true)
            {
                string manguoidung = dgvDSNguoiDung.Rows[e.RowIndex].Cells["MaNguoiDung"].Value.ToString();
                string hoten = dgvDSNguoiDung.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
                string tentk = dgvDSNguoiDung.Rows[e.RowIndex].Cells["TenTaiKhoan"].Value.ToString();
                string mk = dgvDSNguoiDung.Rows[e.RowIndex].Cells["MatKhau"].Value.ToString();
                string anh = dgvDSNguoiDung.Rows[e.RowIndex].Cells["AnhDaiDien"].Value.ToString();
                string trangthai = dgvDSNguoiDung.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();
                

                txtMaNguoiDung.Text = manguoidung;
                txtHoTen.Text = hoten;
                txtTenTK.Text = tentk;
                txtMatKhau.Text = mk;
                txtAnhDaiDien.Text = anh;
                cbbTrangThai.Text = trangthai;
            
            }          
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string trangthai = cbbTrangThai.SelectedValue != null ? cbbTrangThai.SelectedValue.ToString().Trim() : "";
            string manguoidung = txtMaNguoiDung.Text.Trim();
            string hoten = txtHoTen.Text.Trim();
            string tentk = txtTenTK.Text.Trim();
            string matkhau = txtMatKhau.Text.Trim();
            string anhdaidien = txtAnhDaiDien.Text.Trim();

            // Kiểm tra các trường nhập vào không bị rỗng
            if (string.IsNullOrEmpty(manguoidung) || string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(tentk) || string.IsNullOrEmpty(matkhau) || string.IsNullOrEmpty(anhdaidien) || string.IsNullOrEmpty(trangthai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Kiểm tra mã người dùng có tồn tại trong CSDL không
            string checkMaNguoiDung = "SELECT COUNT(*) FROM NguoiDung WHERE MaNguoiDung = N'" + manguoidung + "'";
            int count = db.getScalar(checkMaNguoiDung);

            if (count == 0)
            {
                MessageBox.Show("Mã người dùng không tồn tại trong hệ thống.");
                return;
            }

            try
            {
                // Câu lệnh SQL để cập nhật người dùng
                string luu = "UPDATE NguoiDung SET HoTen = N'" + hoten + "', TenTaiKhoan = N'" + tentk + "', MatKhau = N'" + matkhau + "', AnhDaiDien = N'" + anhdaidien + "', TrangThai = N'" + trangthai + "' WHERE MaNguoiDung = N'" + manguoidung + "'";

                // Thực thi câu lệnh SQL
                int kq = db.getNonQuery(luu);
                if (kq == 0)
                {
                    MessageBox.Show("Cập nhật không thành công. Vui lòng kiểm tra lại.");
                }
                else
                {
                    MessageBox.Show("Cập nhật thành công.");
                    HienThiDS(); // Cập nhật lại danh sách người dùng
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "file hinh|*.png|all file|*.*";
            dlg.InitialDirectory = @"E:\";
            dlg.Multiselect = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] dsFile = dlg.FileNames;
                foreach (string tenFile in dsFile)
                {
                    FileInfo fi = new FileInfo(tenFile);
                    string[] xxx = tenFile.Split('\\');

                    string dd1 = Directory.GetParent(Application.StartupPath).Parent.FullName;
                    string des = dd1 + @"\HinhAnh\" + xxx[xxx.Length - 1];

                    if (File.Exists(des))
                        File.Delete(des);

                    fi.CopyTo(des);

                    pictureBox1.Image = Image.FromFile(des);
                    txtAnhDaiDien.Text = tenFile;
                }
                MessageBox.Show("Thành công");
                dlg.Dispose();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = txtTimKiem.Text.Trim();  // Lấy giá trị từ textbox tìm kiếm chung
            string trangThai = cbbLoc_TT.SelectedValue != null ? cbbLoc_TT.SelectedValue.ToString() : string.Empty;  // Kiểm tra và lấy giá trị trạng thái từ combobox

            // Xây dựng câu truy vấn SQL với các điều kiện tìm kiếm
            string query = "SELECT * FROM NguoiDung WHERE 1=1";  // Điều kiện 1=1 để dễ dàng thêm các điều kiện sau

            // Thêm điều kiện tìm kiếm nếu có giá trị nhập vào
            if (!string.IsNullOrEmpty(timKiem))
            {
                query += " AND (HoTen LIKE N'%" + timKiem + "%' OR MaNguoiDung LIKE N'%" + timKiem + "%' OR TenTaiKhoan LIKE N'%" + timKiem + "%')";
            }

            // Thêm điều kiện lọc theo trạng thái nếu có
            if (!string.IsNullOrEmpty(trangThai))  // Kiểm tra xem trạng thái có hợp lệ không
            {
                query += " AND TrangThai = N'" + trangThai + "'";
            }

            // Thực hiện truy vấn và hiển thị kết quả
            try
            {
                // Thực thi truy vấn
                DataTable dt = db.getSqlDataAdapter(query);

                // Hiển thị kết quả tìm kiếm trong DataGridView
                if (dt.Rows.Count > 0)
                {
                    dgvDSNguoiDung.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }
    }
}

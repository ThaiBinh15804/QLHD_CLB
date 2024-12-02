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
    public partial class formcv : Form
    {
        bool flat = false;
        DBConnect db = new DBConnect();    
        public formcv()
        {
            InitializeComponent();
        }

        private void HienThiDS()
        {
            string chuoi = "select * from ChucVu";
            DataTable dt = db.getSqlDataAdapter(chuoi);
            dgvDSChucVu.DataSource = dt;
        }
        private void FormChucVuCLB_Load(object sender, EventArgs e)
        {
            string maChucVu = SinhMaChucVu();
            if (maChucVu != null)
            {
                txtMaChucVu.Text = maChucVu;
            }
            HienThiDS();
        }

        private string SinhMaChucVu()
        {
            // Câu lệnh SQL để lấy mã lớn nhất
            string query = "SELECT MAX(CAST(SUBSTRING(MaChucVu, 3, LEN(MaChucVu)) AS INT)) FROM ChucVu";

            try
            {
                // Lấy kết quả trả về từ CSDL
                int maCuoi = db.getScalar(query);

                // Tạo mã mới (Cộng thêm 1 vào mã lớn nhất)
                int maMoi = maCuoi + 1;

                // Đảm bảo mã mới có dạng CV001, CV002, ...
                string maChucVuMoi = "CV" + maMoi.ToString("D3");  // D3 đảm bảo có 3 chữ số

                return maChucVuMoi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy mã chức vụ: " + ex.Message);
                return null;
            }
        }




        private bool KTMaChucVu(string macv)
        {
            string ss = "select count(*) from ChucVu where MaChucVu='" + macv + "'";
            int kq = db.getScalar(ss);
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
            string maChucVu = txtMaChucVu.Text.Trim();
            string tenChucVu = txtTenChucVu.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            // Kiểm tra các trường nhập vào không bị rỗng
            if (string.IsNullOrEmpty(maChucVu) || string.IsNullOrEmpty(tenChucVu) || string.IsNullOrEmpty(moTa))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin Mã Chức Vụ, Tên Chức Vụ và Mô Tả.");
                return;
            }

            // Kiểm tra xem mã chức vụ đã tồn tại trong CSDL chưa
            if (!KTMaChucVu(maChucVu))  // Giả sử KTMaChucVu kiểm tra mã chức vụ đã tồn tại
            {
                MessageBox.Show("Mã Chức Vụ đã tồn tại. Vui lòng nhập mã khác.");
                return;
            }

            try
            {
                // Câu lệnh SQL để thêm chức vụ mới vào cơ sở dữ liệu
                string them = "INSERT INTO ChucVu (MaChucVu, TenChucVu, MoTa) VALUES ('" + maChucVu + "', N'" + tenChucVu + "', N'" + moTa + "')";

                // Thực hiện câu lệnh SQL
                int kq = db.getNonQuery(them);
                if (kq == 0)
                {
                    MessageBox.Show("Không thể thêm chức vụ. Vui lòng thử lại.");
                }
                else
                {
                    MessageBox.Show("Thêm chức vụ thành công.");
                    HienThiDS();  // Cập nhật lại danh sách chức vụ
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
            string maChucVu2 = SinhMaChucVu();
            if (maChucVu2 != null)
            {
                txtMaChucVu.Text = maChucVu2;
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu cả Mã Chức Vụ và Tên Chức Vụ đều rỗng
            if (string.IsNullOrEmpty(txtMaChucVu.Text) && string.IsNullOrEmpty(txtTenChucVu.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Chức Vụ hoặc Tên Chức Vụ để xóa.");
                return;
            }

            // Câu lệnh SQL để xóa chức vụ
            string xoa = "delete from ChucVu where(MaChucVu='" + txtMaChucVu.Text.Trim() + "' or TenChucVu=N'" + txtTenChucVu.Text.Trim() + "')";

            try
            {
                int kq = db.getNonQuery(xoa);  // Thực thi câu lệnh SQL
                if (kq == 0)
                {
                    MessageBox.Show("Chưa xóa được chức vụ.");
                }
                else
                {
                    MessageBox.Show("Đã xóa thành công chức vụ.");
                    HienThiDS();  // Cập nhật lại danh sách chức vụ
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }

            // Xóa các trường nhập liệu sau khi xóa thành công
            this.txtTenChucVu.Clear();
            this.txtMoTa.Clear();
            string maChucVu = SinhMaChucVu();
            if (maChucVu != null)
            {
                txtMaChucVu.Text = maChucVu;
            }

        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            flat = true;
            
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            flat = false;

            this.txtTenChucVu.Clear();
            this.txtMoTa.Clear();
            string maChucVu = SinhMaChucVu();
            if (maChucVu != null)
            {
                txtMaChucVu.Text = maChucVu;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maChucVu = txtMaChucVu.Text.Trim();
            string tenChucVu = txtTenChucVu.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            // Kiểm tra các trường nhập vào không bị rỗng
            if (string.IsNullOrEmpty(tenChucVu) || string.IsNullOrEmpty(moTa))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin Tên Chức Vụ và Mô Tả.");
                return;
            }
            if (!KTMaChucVu(maChucVu)) 
            {
                MessageBox.Show("Mã Chức Vụ không tồn tại. Không thể cập nhật.");
                return;
            }

            try
            {
                // Câu lệnh SQL để cập nhật chức vụ
                string sua = "UPDATE ChucVu SET TenChucVu = N'" + tenChucVu + "', MoTa = N'" + moTa + "' WHERE MaChucVu = '" + maChucVu + "'";

                // Thực thi câu lệnh SQL
                int kq = db.getNonQuery(sua);
                if (kq == 0)
                {
                    MessageBox.Show("Cập nhật không thành công.");
                }
                else
                {
                    MessageBox.Show("Cập nhật thành công.");
                    HienThiDS();  // Cập nhật lại danh sách chức vụ
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
            if (maChucVu != null)
            {
                txtMaChucVu.Text = maChucVu;
            }
        }


        private void dgvDSChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && flat == true)
            {
                string machucvu = dgvDSChucVu.Rows[e.RowIndex].Cells["MaChucVu"].Value.ToString();
                string tenchucvu = dgvDSChucVu.Rows[e.RowIndex].Cells["TenChucVu"].Value.ToString();
                string mota = dgvDSChucVu.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();

                txtMaChucVu.Text = machucvu;
                txtTenChucVu.Text = tenchucvu;
                txtMoTa.Text = mota;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = txtTimKiem.Text.Trim();  
            string query = "SELECT * FROM ChucVu WHERE 1=1"; 
            if (!string.IsNullOrEmpty(timKiem))
            {
                query += " AND (MaChucVu LIKE N'%" + timKiem + "%' OR TenChucVu LIKE N'%" + timKiem + "%')";
            }
            try
            {
                // Thực thi truy vấn
                DataTable dt = db.getSqlDataAdapter(query);
                if (dt.Rows.Count > 0)
                {
                    dgvDSChucVu.DataSource = dt;
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

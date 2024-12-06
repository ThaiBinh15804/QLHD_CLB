using QLHD_CLB.Model;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
            string chuoi = "select * from NguoiDung";
            DataTable dt = db.getSqlDataAdapter(chuoi);
            dgvDSNguoiDung.DataSource = dt;

            // Lấy dữ liệu trạng thái người dùng
            string chuoi2 = "select DISTINCT TrangThai from NguoiDung";
            DataTable dt2 = db.getSqlDataAdapter(chuoi2);

            // Cập nhật ComboBox cbbTrangThai
            cbbTrangThai.DataSource = dt2;
            cbbTrangThai.ValueMember = "TrangThai";
            cbbTrangThai.SelectedIndex = -1;  // Đặt mặc định là không có lựa chọn

            // Thêm "Chọn cả 2" vào cbbLoc_TT
            DataTable dtLoc = dt2.Copy();  // Tạo một bản sao của dt2
            DataRow row = dtLoc.NewRow();
            row["TrangThai"] = "Chọn cả 2";  // Gán giá trị cho "Chọn cả 2"
            dtLoc.Rows.InsertAt(row, 2);  // Thêm vào đầu danh sách

            // Cập nhật ComboBox cbbLoc_TT
            cbbLoc_TT.DataSource = dtLoc;
            cbbLoc_TT.ValueMember = "TrangThai";
            cbbLoc_TT.SelectedIndex = -1;  // Đặt mặc định là không có lựa chọn
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
                int maCuoi = int.Parse(db.getScalar(query).ToString());

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
            int kq = int.Parse(db.getScalar(m).ToString());
            if (kq == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ValidateInputs()
        {
            // Không được để trống
            if (string.IsNullOrWhiteSpace(txtTenTK.Text))
            {
                MessageBox.Show("Tài khoản không được để trống!");
                txtTenTK.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu không được để trống!");
                txtMatKhau.Focus();
                return false;
            }

            // Độ dài tối thiểu
            if (txtTenTK.Text.Length < 6)
            {
                MessageBox.Show("Tài khoản phải có ít nhất 6 ký tự!");
                txtTenTK.Focus();
                return false;
            }

            if (txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!");
                txtMatKhau.Focus();
                return false;
            }

            // Ký tự hợp lệ cho tài khoản
            if (!Regex.IsMatch(txtTenTK.Text, "^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Tài khoản chỉ được chứa ký tự chữ và số!");
                txtTenTK.Focus();
                return false;
            }

            // Ký tự hợp lệ cho mật khẩu
            if (!Regex.IsMatch(txtMatKhau.Text, "^[a-zA-Z0-9@#$%^&+=]+$"))
            {
                MessageBox.Show("Mật khẩu chỉ được chứa ký tự chữ, số, và các ký tự đặc biệt: @#$%^&+=");
                txtMatKhau.Focus();
                return false;
            }

            // Kiểm tra mật khẩu mạnh (ví dụ: có ít nhất một ký tự chữ hoa, một ký tự đặc biệt)
            if (!Regex.IsMatch(txtMatKhau.Text, @"[A-Z]")) // ít nhất 1 chữ hoa
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một chữ cái hoa!");
                txtMatKhau.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtMatKhau.Text, @"[0-9]")) // ít nhất 1 số
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một số!");
                txtMatKhau.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtMatKhau.Text, @"[@#$%^&+=]")) // ít nhất 1 ký tự đặc biệt
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất một ký tự đặc biệt: @#$%^&+=");
                txtMatKhau.Focus();
                return false;
            }

            return true; // Đạt tất cả các điều kiện
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            bool isValid = false;

            // Lặp lại cho đến khi các điều kiện đầu vào hợp lệ
            while (!isValid)
            {
                string tk = txtTenTK.Text;
                string mk = txtMatKhau.Text;

                // Kiểm tra ràng buộc đầu vào
                if (!ValidateInputs())
                {
                    // Nếu ValidateInputs trả về false, không cho phép tiếp tục
                    return; // Quay lại vòng lặp và yêu cầu người dùng nhập lại
                }

                // Nếu đến đây, có nghĩa là ValidateInputs() trả về true, đầu vào hợp lệ
                isValid = true; // Thoát vòng lặp
            }
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
            txtHoTen.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtAnhDaiDien.Clear();
            cbbTrangThai.SelectedIndex = -1;
            pictureBox1.Image = null;
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu cả Mã Người Dùng và Họ Tên đều rỗng
            if (string.IsNullOrEmpty(txtMaNguoiDung.Text) && string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Người Dùng hoặc Họ Tên để xóa.");
                return;
            }
            string kiemTra = "SELECT COUNT(*) FROM DamNhiem WHERE  MaNguoiDung = N'" + txtMaNguoiDung.Text.Trim() + "'";

            int count = Convert.ToInt32(db.getScalar(kiemTra));  // Thực thi câu lệnh SELECT để kiểm tra số lượng bản ghi

            if (count > 0) // Nếu có dữ liệu trong bảng DamnhiemChucVu thì không cho phép xóa
            {
                MessageBox.Show("Không thể xóa người dùng này vì người dùng đang thuộc về 1 ban.");
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
            pictureBox1.Image = null;
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            flat = true;
            btnThem.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Reset trạng thái của các ô nhập liệu
            flat = false;
            txtHoTen.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtAnhDaiDien.Clear();
            cbbTrangThai.SelectedIndex = -1;  // Reset ComboBox
            pictureBox1.Image = null;  // Xóa ảnh đại diện

            // Tạo mã người dùng mới
            string maNguoiDung = SinhMaNguoiDung();
            if (maNguoiDung != null)
            {
                txtMaNguoiDung.Text = maNguoiDung;  // Gán mã người dùng mới vào textbox
            }

            // Làm mới DataGridView (hiển thị toàn bộ dữ liệu)
            string query = "SELECT * FROM NguoiDung";  // Truy vấn lấy toàn bộ dữ liệu
            try
            {
                DataTable dt = db.getSqlDataAdapter(query);  // Thực thi truy vấn
                dgvDSNguoiDung.DataSource = dt;  // Gán kết quả vào DataGridView

                // Kiểm tra nếu không có dữ liệu
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi làm mới dữ liệu: " + ex.Message);
            }
            btnThem.Enabled = true;
        }


        private void dgvDSNguoiDung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDSNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && flat == true)
            {
                // Lấy giá trị từ các ô của dòng đã chọn
                string manguoidung = dgvDSNguoiDung.Rows[e.RowIndex].Cells["MaNguoiDung"].Value.ToString();
                string hoten = dgvDSNguoiDung.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
                string tentk = dgvDSNguoiDung.Rows[e.RowIndex].Cells["TenTaiKhoan"].Value.ToString();
                string mk = dgvDSNguoiDung.Rows[e.RowIndex].Cells["MatKhau"].Value.ToString();
                string anh = dgvDSNguoiDung.Rows[e.RowIndex].Cells["AnhDaiDien"].Value.ToString();  // Tên ảnh trong cơ sở dữ liệu (không phải đường dẫn đầy đủ)
                string trangthai = dgvDSNguoiDung.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();

                // Hiển thị thông tin vào các TextBox
                txtMaNguoiDung.Text = manguoidung;
                txtHoTen.Text = hoten;
                txtTenTK.Text = tentk;
                txtMatKhau.Text = mk;
                txtAnhDaiDien.Text = anh;  // Hiển thị tên ảnh vào TextBox
                cbbTrangThai.Text = trangthai;

                // Tạo đường dẫn đầy đủ tới thư mục HinhAnh\AnhDaiDien
                string projectDirectory = Directory.GetParent(Application.StartupPath).Parent.FullName;
                string folderPath = Path.Combine(projectDirectory, "HinhAnh", "AnhDaiDien");

                // Kiểm tra nếu tên ảnh không rỗng và thư mục chứa ảnh tồn tại
                if (!string.IsNullOrEmpty(anh))
                {
                    string fullPath = Path.Combine(folderPath, anh); // Đường dẫn đầy đủ tới ảnh

                    // Kiểm tra nếu ảnh tồn tại tại đường dẫn đó
                    if (File.Exists(fullPath))
                    {
                        try
                        {
                            // Tải ảnh từ đường dẫn và hiển thị lên PictureBox
                            pictureBox1.Image = Image.FromFile(fullPath);
                        }
                        catch (Exception ex)
                        {
                            pictureBox1.Image = null;  // Nếu có lỗi, clear PictureBox
                            MessageBox.Show("Đã có lỗi xảy ra khi tải ảnh: " + ex.Message);
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;  // Nếu không tìm thấy ảnh, clear PictureBox
                        MessageBox.Show("Không tìm thấy ảnh tại đường dẫn: " + fullPath);
                    }
                }
                else
                {
                    pictureBox1.Image = null;  // Nếu không có ảnh, clear PictureBox
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool isValid = false;

            // Lặp lại cho đến khi các điều kiện đầu vào hợp lệ
            while (!isValid)
            {
                string tk = txtTenTK.Text;
                string mk = txtMatKhau.Text;

                // Kiểm tra ràng buộc đầu vào
                if (!ValidateInputs())
                {
                    // Nếu ValidateInputs trả về false, không cho phép tiếp tục
                    return; // Quay lại vòng lặp và yêu cầu người dùng nhập lại
                }

                // Nếu đến đây, có nghĩa là ValidateInputs() trả về true, đầu vào hợp lệ
                isValid = true; // Thoát vòng lặp
            }
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
            int count = int.Parse(db.getScalar(checkMaNguoiDung).ToString());

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
            string maNguoiDung = SinhMaNguoiDung();
            if (maNguoiDung != null)
            {
                txtMaNguoiDung.Text = maNguoiDung;
            }
            txtHoTen.Clear();
            txtTenTK.Clear();
            txtMatKhau.Clear();
            txtAnhDaiDien.Clear();
            cbbTrangThai.SelectedIndex = -1;
            pictureBox1.Image = null;
            btnThem.Enabled = true;
            flat = false;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif|All files|*.*";
            dlg.InitialDirectory = @"E:\";  // Đường dẫn gốc khi mở hộp thoại
            dlg.Multiselect = true; // Cho phép chọn nhiều tệp

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Đường dẫn đến thư mục chứa ảnh
                string dsFile = Directory.GetParent(Application.StartupPath).Parent.FullName;
                string folderPath = Path.Combine(dsFile, "HinhAnh", "AnhDaiDien");

                // Kiểm tra xem thư mục đã tồn tại chưa, nếu chưa thì tạo mới
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Duyệt qua danh sách các tệp tin được chọn
                foreach (string tenFile in dlg.FileNames)
                {
                    FileInfo fi = new FileInfo(tenFile);
                    string[] xxx = tenFile.Split('\\');  // Tách tên file từ đường dẫn đầy đủ
                    string destinationPath = Path.Combine(folderPath, xxx[xxx.Length - 1]);

                    // Kiểm tra nếu ảnh đã tồn tại trong thư mục đích thì xóa nó
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }

                    fi.CopyTo(destinationPath);

                    // Cập nhật PictureBox và TextBox
                    pictureBox1.Image = Image.FromFile(destinationPath);
                    txtAnhDaiDien.Text = xxx[xxx.Length - 1];  // Tên ảnh trong TextBox
                }

                MessageBox.Show("Thêm ảnh thành công");
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

            // Kiểm tra xem người dùng có chọn "Chọn cả 2" hay không
            if (!string.IsNullOrEmpty(trangThai) && trangThai != "Chọn cả 2")  // Nếu không phải "Chọn cả 2"
            {
                // Thêm điều kiện lọc theo trạng thái
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

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Image imageToShow = pictureBox1.Image;

            if (imageToShow == null)
            {
                MessageBox.Show("Không có ảnh nào để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                FormXemAnh formXemAnh = new FormXemAnh(imageToShow);
                formXemAnh.ShowDialog();
            }
        }

        private void eyeOpen_Click(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = true;
            eyeOpen.Visible = false;
            eyeHide.Visible = true;
        }

        private void eyeHide_Click(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = false;
            txtMatKhau.PasswordChar = '\0';
            eyeOpen.Visible = true;
            eyeHide.Visible = false;
        }

        private void dgvDSNguoiDung_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra cột và kiểu dữ liệu
            if (dgvDSNguoiDung.Columns[e.ColumnIndex].Name == "MatKhau" && e.Value != null)
            {
                // Thay thế giá trị mật khẩu bằng dấu *
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }
    }
}

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
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace QLHD_CLB
{
    public partial class FormBanCLB : Form
    {
        private bool flat = false;

        DBConnect db = new DBConnect();

        public FormBanCLB()
        {
            InitializeComponent();
        }

        public void HienThiDSBan()
        {
            tv_dsban.Nodes.Clear();  // Xóa hết các node cũ trong TreeView

            string ds = @"
    select b.MaBan, b.TenBan, b.MoTa, c.MaChucVu, c.TenChucVu, n.HoTen 
    from Ban b
    left join DamNhiem d on b.MaBan = d.MaBan
    left join ChucVu c on c.MaChucVu = d.MaChucVu
    left join NguoiDung n on n.MaNguoiDung = d.MaNguoiDung";
            DataTable dt = db.getSqlDataAdapter(ds);

            string chucvuQuery = "select MaChucVu, TenChucVu from ChucVu";
            DataTable chucvuTable = db.getSqlDataAdapter(chucvuQuery);

            // Duyệt qua từng dòng dữ liệu của Ban
            foreach (DataRow dr in dt.Rows)
            {
                string maban = dr["MaBan"].ToString();
                string tenban = dr["TenBan"].ToString();
                string mota = dr["MoTa"].ToString();

                // Kiểm tra xem node của bàn này đã tồn tại chưa
                TreeNode existingNode = tv_dsban.Nodes.Cast<TreeNode>().FirstOrDefault(t => t.Tag != null && ((Tuple<string, string, bool>)t.Tag).Item1 == maban);

                if (existingNode == null)
                {
                    // Nếu chưa có thì tạo mới node cho ban
                    existingNode = new TreeNode(tenban);
                    existingNode.Tag = new Tuple<string, string, bool>(maban, mota, true);
                    tv_dsban.Nodes.Add(existingNode);
                }

                // Duyệt qua các chức vụ
                foreach (DataRow chucvuRow in chucvuTable.Rows)
                {
                    string chucvu = chucvuRow["TenChucVu"].ToString();
                    string hoten = "Chưa gán vai trò";

                    bool isBanQuanLy = tenban.Contains("Ban Quản Lý"); 

                    // Kiểm tra xem có người dùng nào đã được gán vào chức vụ này cho bàn hiện tại không
                    DataRow[] rows = dt.Select(string.Format("MaBan = '{0}' AND TenChucVu = '{1}'", maban, chucvu));

                    if (rows.Length > 0 && rows[0]["HoTen"] != DBNull.Value)
                    {
                        hoten = rows[0]["HoTen"].ToString();
                    }

                    string chucvu_hoten = chucvu + " - " + hoten;

                    if (isBanQuanLy)
                    {
                        if (chucvu == "Chủ nhiệm CLB" || chucvu == "Phó chủ nhiệm CLB" || chucvu == "Thư Ký")
                        {
                            // Kiểm tra xem node con cho chức vụ này đã tồn tại chưa
                            bool nodeExists = existingNode.Nodes.Cast<TreeNode>().Any(n => n.Text == chucvu_hoten);

                            if (!nodeExists)
                            {
                                // Nếu chưa tồn tại, thêm node con cho chức vụ
                                existingNode.Nodes.Add(new TreeNode(chucvu_hoten) { Tag = new Tuple<string, string, bool>(maban, mota, false) });
                            }
                        }
                    }
                    else
                    {
                        // Nếu là ban khác, ta chỉ thêm 2 chức vụ: Trưởng ban và Phó ban
                        if (chucvu == "Trưởng Ban" || chucvu == "Phó Ban")
                        {
                            // Kiểm tra xem node con cho chức vụ này đã tồn tại chưa
                            bool nodeExists = existingNode.Nodes.Cast<TreeNode>().Any(n => n.Text == chucvu_hoten);

                            if (!nodeExists)
                            {
                                // Nếu chưa tồn tại, thêm node con cho chức vụ
                                existingNode.Nodes.Add(new TreeNode(chucvu_hoten) { Tag = new Tuple<string, string, bool>(maban, mota, false) });
                            }
                        }
                    }
                }
            }
        }



        private void HienThiDS_ChucVu()
        {

            string chuoi2 = "select * from NguoiDung";
            DataTable dt2 = db.getSqlDataAdapter(chuoi2);
            cbb_HoTen.DataSource = dt2;
            cbb_HoTen.DisplayMember = "HoTen";
            cbb_HoTen.ValueMember = "MaNguoiDung";
            cbb_HoTen.SelectedIndex = -1; 
            cbb_HoTen.SelectedItem = null;
        }

        private void FormBanCLB_Load(object sender, EventArgs e)
        {
            string maBan = SinhMaBan();
            if (maBan != null)
            {
                txtMaBan.Text = maBan;
            }
            HienThiDSBan();
            HienThiDS_ChucVu();

            if (GlobalValue.ChucVu_NguoiDung == "CV002")
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnLamMoi.Enabled = false;
            }

        }

        private string SinhMaBan()
        {
            string query = "SELECT MAX(CAST(SUBSTRING(MaBan, 2, LEN(MaBan)) AS INT)) FROM Ban";

            try
            {
                // Lấy kết quả trả về từ CSDL
                int maCuoi = int.Parse(db.getScalar(query).ToString());

                // Tạo mã bàn mới (Cộng thêm 1 vào mã lớn nhất)
                int maMoi = maCuoi + 1;

                // Đảm bảo mã mới có dạng B001, B002, ...
                string maBanMoi = "B" + maMoi.ToString("D3");  // D3 đảm bảo có 3 chữ số

                return maBanMoi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy mã bàn: " + ex.Message);
                return null;
            }
        }


        public bool KTBan(string maban)
        {
            string m = "select count(*) from Ban where MaBan= '"+maban+"'";
            int kq = int.Parse(db.getScalar(m).ToString());
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
            if (string.IsNullOrEmpty(txtMaBan.Text) || string.IsNullOrEmpty(txtTenBan.Text) || string.IsNullOrEmpty(txtMoTa.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin Mã Ban, Tên Ban và Mô Tả Ban.");
                return;
            }
            if (txtMaBan.Text.Contains(" ") || !txtMaBan.Text.All(char.IsLetterOrDigit))
            {
                MessageBox.Show("Mã Ban không hợp lệ. Không được chứa khoảng trắng hoặc ký tự đặc biệt.");
                return;
            }
            if (!KTBan(txtMaBan.Text))  
            {
                MessageBox.Show("Không thể thêm vì mã Ban đã tồn tại.");
                return;
            }
            string chuoi = "INSERT INTO Ban (MaBan, TenBan, MoTa) VALUES ('" + txtMaBan.Text.Trim() + "', N'" + txtTenBan.Text.Trim() + "', N'" + txtMoTa.Text.Trim() + "')";
            try
            {
                int kq = db.getNonQuery(chuoi);
                if (kq == 0)
                {
                    MessageBox.Show("Thêm mới không thành công.");
                }
                else
                {
                    MessageBox.Show("Thêm thành công.");
                    HienThiDS_ChucVu();
                    HienThiDSBan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
            string maBan = SinhMaBan();
            if (maBan != null)
            {
                txtMaBan.Text = maBan;
            }
            this.txtTenBan.Clear();
            this.txtMoTa.Clear();
            this.txtChucVu.Clear();
            this.cbb_HoTen.SelectedItem = null;
            cbb_HoTen.Enabled = false;
        }

        private bool isXoaBan = false;
        private void tv_dsban_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!flat)
                return;

            TreeNode selectedNode = e.Node;

            if (selectedNode.Tag != null)
            {
                var tag = (Tuple<string, string, bool>)selectedNode.Tag; 
                string maBan = tag.Item1;   
                string moTaBan = tag.Item2; 
                bool isBanNode = tag.Item3; 
                if (isBanNode)  
                {
                    isXoaBan = true; 
                    txtMaBan.Text = maBan;
                    txtTenBan.Text = selectedNode.Text;
                    txtMoTa.Text = moTaBan;
                    txtChucVu.Clear();
                    cbb_HoTen.SelectedIndex = -1;  
                }
                else 
                {
                    isXoaBan = false;  
                    string chucVuHoTen = selectedNode.Text;
                    string[] parts = chucVuHoTen.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        txtChucVu.Text = parts[0].Trim();  
                        cbb_HoTen.SelectedItem = parts[1].Trim();  
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Lấy các giá trị từ form
            string maBan = txtMaBan.Text.Trim();
            string tenBan = txtTenBan.Text.Trim();
            string chucVu = txtChucVu.Text.Trim();
            string nguoiDung = cbb_HoTen.SelectedValue != null ? cbb_HoTen.SelectedValue.ToString() : "";
            string manguoidung = cbb_HoTen.SelectedValue != null ? cbb_HoTen.SelectedValue.ToString().Trim() : null;
            string maChucVu = GetMaChucVu(chucVu);

            if (string.IsNullOrEmpty(maBan) || string.IsNullOrEmpty(tenBan))
            {
                MessageBox.Show("Vui lòng chọn một ban để xóa.");
                return;
            }

            SqlConnection connection = db.con;
            try
            {
                connection.Open();

                if (isXoaBan)
                {
                    bool coNguoiDung = true; // Biến kiểm tra có người dùng trong bàn hay không
                    while (coNguoiDung)
                    {
                        // Kiểm tra xem trong bàn có người dùng thuộc chức vụ nào không
                        string kiemTraNguoiDung = "SELECT COUNT(*) FROM DamNhiem WHERE MaBan = @MaBan AND MaChucVu IS NOT NULL";
                        using (SqlCommand cmd = new SqlCommand(kiemTraNguoiDung, connection))
                        {
                            cmd.Parameters.AddWithValue("@MaBan", maBan);
                            int soLuongNguoiDung = (int)cmd.ExecuteScalar();

                            if (soLuongNguoiDung > 0) // Nếu có người dùng trong bàn này, không cho phép xóa
                            {
                                MessageBox.Show("Không thể xóa ban vì có người dùng thuộc chức vụ trong ban này.");
                                coNguoiDung = false; // Thoát khỏi vòng lặp
                                return; // Dừng lại ở đây không thực hiện xóa bàn
                            }
                            else
                            {
                                coNguoiDung = false;
                            }
                        }
                    }
                    // Kiểm tra xem ban có thành viên nào không
                    string kiemTraThanhVien = "SELECT COUNT(*) FROM ThanhVien WHERE MaBan = @MaBan";
                    using (SqlCommand cmd = new SqlCommand(kiemTraThanhVien, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaBan", maBan);
                        int soLuongThanhVien = (int)cmd.ExecuteScalar();
                        if (soLuongThanhVien > 0) // Nếu có thành viên trong ban, không cho phép xóa
                        {
                            MessageBox.Show("Không thể xóa ban vì có thành viên đang tham gia ban này.");
                            return; // Dừng lại ở đây không thực hiện xóa ban
                        }
                    }

                    // Kiểm tra xem ban có nhiệm vụ phân công không
                    string kiemTraPhanCong = "SELECT COUNT(*) FROM PhanCong WHERE MaBan = @MaBan";
                    using (SqlCommand cmd = new SqlCommand(kiemTraPhanCong, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaBan", maBan);
                        int soLuongPhanCong = (int)cmd.ExecuteScalar();
                        if (soLuongPhanCong > 0) // Nếu có nhiệm vụ phân công, không cho phép xóa
                        {
                            MessageBox.Show("Không thể xóa ban vì ban này đang được phân công công việc.");
                            return; // Dừng lại ở đây không thực hiện xóa ban
                        }
                    }

                    // Kiểm tra xem có người dùng nào thuộc chức vụ trong ban không
                    string kiemTraDamNhiem = "SELECT COUNT(*) FROM DamNhiem WHERE MaBan = @MaBan";
                    using (SqlCommand cmd = new SqlCommand(kiemTraDamNhiem, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaBan", maBan);
                        int soLuongDamNhiem = (int)cmd.ExecuteScalar();
                        if (soLuongDamNhiem > 0)
                        {
                            // Xóa tất cả nhiệm vụ trong DamNhiem
                            string xoaDamNhiemAll = "DELETE FROM DamNhiem WHERE MaBan = @MaBan";
                            using (SqlCommand cmd2 = new SqlCommand(xoaDamNhiemAll, connection))
                            {
                                cmd2.Parameters.AddWithValue("@MaBan", maBan);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }

                    // Thực hiện xóa ban
                    string xoaBan = "DELETE FROM Ban WHERE MaBan = @MaBan";
                    using (SqlCommand cmd = new SqlCommand(xoaBan, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaBan", maBan);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Xóa ban thành công.");
                }
                else
                {
                    if (string.IsNullOrEmpty(maChucVu) || string.IsNullOrEmpty(manguoidung))
                    {
                        MessageBox.Show("Vui lòng chọn chức vụ và người dùng để xóa liên kết.");
                        return;
                    }

                    string xoaDamNhiem = "DELETE FROM DamNhiem WHERE MaBan = @MaBan AND MaChucVu = @MaChucVu AND MaNguoiDung = @MaNguoiDung";
                    using (SqlCommand cmd = new SqlCommand(xoaDamNhiem, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaBan", maBan);
                        cmd.Parameters.AddWithValue("@MaChucVu", maChucVu);
                        cmd.Parameters.AddWithValue("@MaNguoiDung", manguoidung);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Xóa người dùng thuộc chức vụ thành công.");
                }

                // Làm mới các thông tin trên form sau khi xóa
                this.txtMaBan.Clear();
                this.txtTenBan.Clear();
                this.txtMoTa.Clear();
                HienThiDS_ChucVu();
                HienThiDSBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            string maBan2 = SinhMaBan();
            if (maBan2 != null)
            {
                txtMaBan.Text = maBan2;
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            flat=true;
            cbb_HoTen.Enabled = true;
            btnThem.Enabled = false;
            tv_dsban.SelectedNode = null;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            flat = false;
            this.txtTenBan.Clear();
            this.txtMoTa.Clear();
            this.txtChucVu.Clear();
            this.cbb_HoTen.SelectedItem = null;
            cbb_HoTen.Enabled = false;
            string maBan = SinhMaBan();
            if (maBan != null)
            {
                txtMaBan.Text = maBan;
            }
            btnThem.Enabled = true;
        }

        private void tv_dsban_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!flat)
            {
                return;
            }

            TreeNode tn = e.Node;
            if (tn.Parent == null)  
            {
                Tuple<string, string,bool> tagInfo = tn.Tag as Tuple<string, string,bool>;
                if (tagInfo != null)
                {
                    string maban = tagInfo.Item1; 
                    string mota = tagInfo.Item2; 
                    txtMaBan.Text = maban;
                    txtTenBan.Text = tn.Text; 
                    txtMoTa.Text = mota;
                    txtChucVu.Visible = false;  
                    cbb_HoTen.Visible = false;
                    lbHoTen.Visible = false;
                    lb_ChucVu.Visible = false;
                    cbb_HoTen.SelectedIndex = -1;  
                }
            }
            else  
            {
                string chucvuhoten = tn.Text;  
                string[] catchuoi = chucvuhoten.Split(new string[] { " - " }, StringSplitOptions.None);

                if (catchuoi.Length == 2)
                {
                    string chucvu = catchuoi[0];  
                    string hoten = catchuoi[1];   
                    string tenban = tn.Parent.Text;  
                    Tuple<string, string,bool> tagInfo = tn.Parent.Tag as Tuple<string, string,bool>;
                    if (tagInfo != null)
                    {
                        string maban = tagInfo.Item1; 
                        string mota = tagInfo.Item2;  
                        txtMaBan.Text = maban;
                        txtTenBan.Text = tenban;
                        txtMoTa.Text = mota;
                        txtChucVu.Visible = true; 
                        cbb_HoTen.Visible = true;  
                        lb_ChucVu.Visible = true;
                        lbHoTen.Visible = true;
                        txtChucVu.Text = chucvu;
                        if (hoten == "Chưa gán vai trò")
                        {
                            cbb_HoTen.SelectedIndex = -1;  
                            cbb_HoTen.Enabled = false; 
                        }
                        else
                        {
                            cbb_HoTen.Text = hoten;  
                            cbb_HoTen.Enabled =false;  
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không thể tách Chức vụ và Họ tên.");
                }
            }
        }
        private string GetMaChucVu(string tenChucVu)
        {
            string query = "SELECT MaChucVu FROM ChucVu WHERE TenChucVu = @TenChucVu";

            using (SqlCommand cmd = new SqlCommand(query, db.con))
            {
                cmd.Parameters.AddWithValue("@TenChucVu", tenChucVu);

                db.con.Open();
                object result = cmd.ExecuteScalar();
                db.con.Close();

                if (result != null)
                {
                    return result.ToString();  
                }
                else
                {                   
                    return null;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maBan = txtMaBan.Text.Trim();
            string tenBan = txtTenBan.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            string chucVu = txtChucVu.Text.Trim();
            string hoten = cbb_HoTen.SelectedItem != null ? (cbb_HoTen.SelectedItem as DataRowView)["HoTen"].ToString().Trim() : null;
            string manguoidung = cbb_HoTen.SelectedValue != null ? cbb_HoTen.SelectedValue.ToString().Trim() : null;
            string maChucVu = GetMaChucVu(chucVu);

            if (string.IsNullOrEmpty(maBan) || string.IsNullOrEmpty(tenBan) || string.IsNullOrEmpty(moTa))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin Mã Ban, Tên Ban và Mô Tả Ban.");
                return;
            }

            try
            {
                // Cập nhật thông tin Ban (Mã Ban, Tên Ban, Mô Tả Ban)
                string updateBan = "UPDATE Ban SET TenBan = N'" + tenBan + "', MoTa = N'" + moTa + "' WHERE MaBan = N'" + maBan + "'";
                int resultBan = db.getNonQuery(updateBan);

                if (resultBan == 0)
                {
                    MessageBox.Show("Cập nhật thông tin Ban không thành công.");
                    return;
                }

                // Kiểm tra xem chức vụ này đã có người dùng chưa
                string checkChucVuExist = "SELECT COUNT(*) FROM DamNhiem WHERE MaBan = N'" + maBan + "' AND MaChucVu = N'" + maChucVu + "'";
                int countChucVuExist = int.Parse(db.getScalar(checkChucVuExist).ToString()); // Kiểm tra chức vụ trong ban này đã có người dùng hay chưa

                if (countChucVuExist > 0)
                {
                    // Nếu đã có người dùng, chỉ cần kiểm tra nếu MaNguoiDung thay đổi thì mới thực hiện cập nhật
                    string currentMaNguoiDung = db.getScalar("SELECT MaNguoiDung FROM DamNhiem WHERE MaBan = N'" + maBan + "' AND MaChucVu = N'" + maChucVu + "'").ToString();

                    if (currentMaNguoiDung != manguoidung) // Nếu người dùng mới khác với người dùng cũ
                    {
                        string updateChucVuCu = "UPDATE DamNhiem SET MaNguoiDung = N'" + manguoidung + "' WHERE MaBan = N'" + maBan + "' AND MaChucVu = N'" + maChucVu + "'";
                        int resultUpdate = db.getNonQuery(updateChucVuCu);

                        if (resultUpdate == 0)
                        {
                            MessageBox.Show("Cập nhật chức vụ không thành công.");
                            return;
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(manguoidung) && countChucVuExist==0)
                {
                    // Nếu chưa có người dùng và manguoidung không rỗng, thực hiện insert mới vào bảng DamNhiem
                    string insertChucVuNguoiDung = "INSERT INTO DamNhiem (MaBan, MaChucVu, MaNguoiDung) VALUES (N'" + maBan + "', N'" + maChucVu + "', N'" + manguoidung + "')";
                    int resultChucVu = db.getNonQuery(insertChucVuNguoiDung);

                    if (resultChucVu == 0)
                    {
                        MessageBox.Show("Thêm chức vụ và người dùng không thành công.");
                        return;
                    }
                }

                // Cập nhật TreeView
                MessageBox.Show("Cập nhật thông tin thành công.");
                cbb_HoTen.Enabled = false;
                tv_dsban.Nodes.Clear();
                HienThiDSBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
            this.txtTenBan.Clear();
            this.txtMoTa.Clear();
            this.txtChucVu.Clear();
            this.cbb_HoTen.SelectedItem = null;
            cbb_HoTen.Enabled = false;
            string maBan2 = SinhMaBan();
            if (maBan2 != null)
            {
                txtMaBan.Text = maBan;
            }
            btnThem.Enabled = true;
        }



    }
}

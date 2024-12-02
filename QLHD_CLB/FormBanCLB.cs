﻿using System;
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

namespace QLHD_CLB
{
    public partial class FormBanCLB : Form
    {
        DBConnect db = new DBConnect();
        public FormBanCLB()
        {
            InitializeComponent();
        }

        public void HienThiDSBan()
        {
            tv_dsban.Nodes.Clear();
            string ds = @"
        select b.MaBan, b.TenBan, b.MoTa, c.MaChucVu, c.TenChucVu, n.HoTen 
        from Ban b
        left join DamNhiem d on b.MaBan = d.MaBan
        left join ChucVu c on c.MaChucVu = d.MaChucVu
        left join NguoiDung n on n.MaNguoiDung = d.MaNguoiDung";
            DataTable dt = db.getSqlDataAdapter(ds);  
            string chucvuQuery = "select MaChucVu, TenChucVu from ChucVu";  
            DataTable chucvuTable = db.getSqlDataAdapter(chucvuQuery);
            foreach (DataRow dr in dt.Rows)
            {
                string maban = dr["MaBan"].ToString();
                string tenban = dr["TenBan"].ToString();
                string mota = dr["MoTa"].ToString();
                TreeNode existingNode = tv_dsban.Nodes.Cast<TreeNode>().FirstOrDefault(t => t.Tag != null && ((Tuple<string, string, bool>)t.Tag).Item1 == maban);
                if (existingNode == null)
                {
                    existingNode = new TreeNode(tenban);
                    existingNode.Tag = new Tuple<string, string, bool>(maban, mota, true); 
                    tv_dsban.Nodes.Add(existingNode);
                }
                foreach (DataRow chucvuRow in chucvuTable.Rows)
                {
                    string chucvu = chucvuRow["TenChucVu"].ToString();
                    string hoten = "Chưa gán vai trò"; 
                    DataRow[] rows = dt.Select(string.Format("MaBan = '{0}' AND TenChucVu = '{1}'", maban, chucvu));
                    if (rows.Length > 0 && rows[0]["HoTen"] != DBNull.Value)
                    {
                        hoten = rows[0]["HoTen"].ToString();
                    }
                    string chucvu_hoten = chucvu + " - " + hoten;
                    existingNode.Nodes.Add(new TreeNode(chucvu_hoten) { Tag = new Tuple<string, string, bool>(maban, mota, false) }); // Node con, cờ = false
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
        }

        private string SinhMaBan()
        {
            // Câu lệnh SQL để lấy mã bàn lớn nhất
            string query = "SELECT MAX(CAST(SUBSTRING(MaBan, 2, LEN(MaBan)) AS INT)) FROM Ban";

            try
            {
                // Lấy kết quả trả về từ CSDL
                int maCuoi = db.getScalar(query);

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
        }

        private bool isXoaBan = false;
        private void tv_dsban_AfterSelect(object sender, TreeViewEventArgs e)
        {
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
                    string kiemTraDamNhiem = "SELECT COUNT(*) FROM DamNhiem WHERE MaBan = @MaBan";
                    using (SqlCommand cmd = new SqlCommand(kiemTraDamNhiem, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaBan", maBan);
                        int soLuongDamNhiem = (int)cmd.ExecuteScalar();
                        if (soLuongDamNhiem > 0)
                        {
                            string xoaDamNhiemAll = "DELETE FROM DamNhiem WHERE MaBan = @MaBan";
                            using (SqlCommand cmd2 = new SqlCommand(xoaDamNhiemAll, connection))
                            {
                                cmd2.Parameters.AddWithValue("@MaBan", maBan);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
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

                    MessageBox.Show("Xóa liên kết thành công.");
                }
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
            cbb_HoTen.Enabled = true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
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
        }

        private void tv_dsban_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
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

            if (string.IsNullOrEmpty(maChucVu) || string.IsNullOrEmpty(manguoidung))
            {
                MessageBox.Show("Vui lòng chọn chức vụ và người dùng.");
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
                int countChucVuExist = db.getScalar(checkChucVuExist); // Kiểm tra chức vụ trong ban này đã có người dùng hay chưa

                if (countChucVuExist > 0)
                {
                    // Nếu đã có người dùng, thì chỉ cần cập nhật lại MaNguoiDung (người dùng mới)
                    string updateChucVuCu = "UPDATE DamNhiem SET MaNguoiDung = N'" + manguoidung + "' WHERE MaBan = N'" + maBan + "' AND MaChucVu = N'" + maChucVu + "'";
                    int resultUpdate = db.getNonQuery(updateChucVuCu);

                    if (resultUpdate == 0)
                    {
                        MessageBox.Show("Cập nhật chức vụ không thành công.");
                        return;
                    }
                }
                else
                {
                    // Nếu không có, thực hiện insert mới vào bảng DamNhiem
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
                tv_dsban.Nodes.Clear(); 
                HienThiDSBan(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
        }
        
    }
}

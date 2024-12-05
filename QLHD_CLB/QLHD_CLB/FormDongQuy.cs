using Guna.UI2.WinForms;
using QLHD_CLB.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLHD_CLB
{
    public partial class FormDongQuy : Form
    {
        private FormGiaoDien parentForm;

        public FormDongQuy()
        {
            InitializeComponent();
        }

        public FormDongQuy(FormGiaoDien _parent)
        {
            InitializeComponent();
            parentForm = _parent;
        }

        private void SetControlsEnabledFalse(Control container)
        {
            foreach (Control control in container.Controls)
            {
                // Đặt thuộc tính Enabled = false
                control.Enabled = false;

                // Nếu control chứa các control con, gọi đệ quy
                if (control.HasChildren)
                {
                    SetControlsEnabledFalse(control);
                }
            }
        }

        DBConnect db = new DBConnect();

        private void HienThi_DSKeHoachDongQuy()
        {
            string sqlquery = "select MaKeHoach as N'Mã kế hoạch', TenKeHoach as N'Tên kế hoạch', MoTa as N'Mô tả', NgayBatDau as N'Ngày bắt đầu', NgayKetThuc as N'Ngày kết thúc', SoTienCanDong as N'Số tiền cần đóng', TrangThai as N'Trạng thái' from KeHoachDongQuy";
            DataTable dt = db.getSqlDataAdapter(sqlquery);

            dt.PrimaryKey = new DataColumn[] { dt.Columns["MaThanhVien"] };
            dtg_DSKeHoachDongQuy.DataSource = dt;

            dtg_DSKeHoachDongQuy.Columns[0].ReadOnly = true;
            dtg_DSKeHoachDongQuy.Columns[1].ReadOnly = true;
            dtg_DSKeHoachDongQuy.Columns[2].ReadOnly = true;
            dtg_DSKeHoachDongQuy.Columns[3].ReadOnly = true;
            dtg_DSKeHoachDongQuy.Columns[4].ReadOnly = true;
            dtg_DSKeHoachDongQuy.Columns[5].ReadOnly = true;
            dtg_DSKeHoachDongQuy.Columns[6].ReadOnly = true;

            dtg_DSKeHoachDongQuy.Columns["Số tiền cần đóng"].DefaultCellStyle.Format = "N0";
        }

        private void inputTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputTimKiem.Text))
            {
                 HienThi_DSKeHoachDongQuy();
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string search = inputTimKiem.Text.Trim();
            string query = "SELECT MaKeHoach AS N'Mã kế hoạch', TenKeHoach AS N'Tên kế hoạch', MoTa AS N'Mô tả', NgayBatDau AS N'Ngày bắt đầu', NgayKetThuc AS N'Ngày kết thúc', SoTienCanDong AS N'Số tiền cần đóng', TrangThai AS N'Trạng thái' " +
               "FROM KeHoachDongQuy " +
               "WHERE TenKeHoach LIKE N'%" + search + "%' OR MoTa LIKE N'%" + search + "%'";

            try
            {
                // Lấy dữ liệu và cập nhật DataGridView
                DataTable dt = db.getSqlDataAdapter(query);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inputTimKiem.Focus();
                }
                else
                {
                    dtg_DSKeHoachDongQuy.DataSource = dt;
                    dtg_DSKeHoachDongQuy.Columns["Số tiền cần đóng"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThi_ComboBoxLocTrangThai()
        {
            comboBox_LocTrangThai.Items.Clear();
            comboBox_LocTrangThai.Items.Add("Tất cả");
            comboBox_LocTrangThai.Items.Add("Đang hoạt động");
            comboBox_LocTrangThai.Items.Add("Ngừng hoạt động");
            comboBox_LocTrangThai.SelectedIndex = 0;
        }

        private void HienThi_ComboBoxTrangThai()
        {
            comboBox_TrangThai.Items.Clear();
            comboBox_TrangThai.Items.Add("Đang hoạt động");
            comboBox_TrangThai.Items.Add("Ngừng hoạt động");
            comboBox_TrangThai.SelectedIndex = 0;
        }

        private void comboBox_TrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy giá trị của mục được chọn từ ComboBox
            string trangThai = comboBox_LocTrangThai.SelectedItem.ToString(); // Sử dụng SelectedItem để lấy chuỗi

            if (trangThai != null)
            {
                if (trangThai == "Tất cả")
                {
                    // Hiển thị tất cả các kế hoạch
                    HienThi_DSKeHoachDongQuy();
                }
                else
                {
                    // Hiển thị kế hoạch theo trạng thái được chọn
                    string query = "SELECT MaKeHoach AS N'Mã kế hoạch', TenKeHoach AS N'Tên kế hoạch', MoTa AS N'Mô tả', " +
                                   "NgayBatDau AS N'Ngày bắt đầu', NgayKetThuc AS N'Ngày kết thúc', " +
                                   "SoTienCanDong AS N'Số tiền cần đóng', TrangThai AS N'Trạng thái' " +
                                   "FROM KeHoachDongQuy " +
                                   "WHERE TrangThai = N'" + trangThai + "'";
                    DataTable dt = db.getSqlDataAdapter(query);
                    dtg_DSKeHoachDongQuy.DataSource = dt;
                    dtg_DSKeHoachDongQuy.Columns["Số tiền cần đóng"].DefaultCellStyle.Format = "N0";
                }
            }
        }


        private void FormDongQuy_Load(object sender, EventArgs e)
        {
            HienThi_DSKeHoachDongQuy();
            HienThi_ComboBoxLocTrangThai();
            HienThi_ComboBoxTrangThai();

        }

        private void HienThiDSThanhVienDongQuy(string makh)
        {
            string query = "select tv.MaThanhVien as N'Mã thành viên', tv.HoTen as N'Họ tên', dq.TrangThai as N'Trạng thái', dq.NgayDong as N'Ngày đóng' from KeHoachDongQuy khdq join DongQuy dq on khdq.MaKeHoach = dq.MaKeHoach join ThanhVien tv on dq.MaThanhVien = tv.MaThanhVien where khdq.MaKeHoach = '" + makh + "'";
            DataTable dt = db.getSqlDataAdapter(query);
            dtg_dsDongQuy.DataSource = dt;
            dtg_dsDongQuy.Columns[0].ReadOnly = true;
            dtg_dsDongQuy.Columns[1].ReadOnly = true;
            dtg_dsDongQuy.Columns[2].ReadOnly = true;
            dtg_dsDongQuy.Columns[3].ReadOnly = true;
            dtg_dsDongQuy.Columns["Mã thành viên"].Width = 100;  // Đặt chiều rộng 100
            dtg_dsDongQuy.Columns["Họ tên"].Width = 100;  // Đặt chiều rộng 100
            dtg_dsDongQuy.Columns["Trạng thái"].Width = 50;         // Đặt chiều rộng 200
            dtg_dsDongQuy.Columns["Ngày đóng"].Width = 120;       // Đặt chiều rộng 80
            dtg_dsDongQuy.Columns["Trạng thái"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        bool flag = false;
        private void dtg_DSKeHoachDongQuy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                inputMaKH.Enabled = false;
                inputMaKH.Text = dtg_DSKeHoachDongQuy.Rows[e.RowIndex].Cells["Mã kế hoạch"].Value.ToString();
                comboBox_LocTrangThai.SelectedIndexChanged -= comboBox_TrangThai_SelectedIndexChanged;

                if(flag == true)
                {
                    inputTenKeHoach.Text = dtg_DSKeHoachDongQuy.Rows[e.RowIndex].Cells["Tên kế hoạch"].Value.ToString();
                    inputSoTienCanDong.Text = dtg_DSKeHoachDongQuy.Rows[e.RowIndex].Cells["Số tiền cần đóng"].Value.ToString();
                    inputMoTa.Text = dtg_DSKeHoachDongQuy.Rows[e.RowIndex].Cells["Mô tả"].Value.ToString();
                    comboBox_TrangThai.Text = dtg_DSKeHoachDongQuy.Rows[e.RowIndex].Cells["Trạng thái"].Value.ToString();

                    string ngayBatDau = dtg_DSKeHoachDongQuy.Rows[e.RowIndex].Cells["Ngày bắt đầu"].Value.ToString();
                    string ngayKetThuc = dtg_DSKeHoachDongQuy.Rows[e.RowIndex].Cells["Ngày kết thúc"].Value.ToString();
                    DateTime parsedDate;
                    if (DateTime.TryParse(ngayBatDau, out parsedDate ))
                    {
                        guna2DateTimePicker1.Value = parsedDate; // Gán giá trị ngày sau khi chuyển đổi
                    }
                    DateTime parsedDate2;
                    if (DateTime.TryParse(ngayKetThuc, out parsedDate2))
                    {
                        guna2DateTimePicker2.Value = parsedDate2; // Gán giá trị ngày sau khi chuyển đổi
                    }
                }


                HienThiDSThanhVienDongQuy(inputMaKH.Text);

                string query2 = "SELECT HoTen, MaThanhVien FROM ThanhVien WHERE TrangThai = N'Đang hoạt động' AND NOT EXISTS (SELECT 1 FROM DongQuy WHERE DongQuy.MaThanhVien = ThanhVien.MaThanhVien AND DongQuy.MaKeHoach = '" + inputMaKH.Text + "')";
                DataTable dt2 = db.getSqlDataAdapter(query2);
                comboBoxChonThanhVien.DataSource = dt2;
                comboBoxChonThanhVien.ValueMember = "MaThanhVien";
                comboBoxChonThanhVien.DisplayMember = "HoTen";
                comboBoxChonThanhVien.SelectedIndex = 0;

                comboBox_LocTrangThai.SelectedIndexChanged += comboBox_TrangThai_SelectedIndexChanged; // Bật lại sự kiện
            }
        }

        private void ResetForm()
        {
            inputSoTienCanDong.Clear();
            inputTenKeHoach.Clear();
            inputMoTa.Clear();
            comboBox_TrangThai.SelectedIndex = 0;  // Chọn lại mục đầu tiên trong ComboBox
        }

        private void btn_lamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            flag = false;
            inputMaKH.Enabled = true;
            inputTenKeHoach.Enabled = true;
            inputMoTa.Enabled = true;
            inputSoTienCanDong.Enabled = true;
            comboBox_TrangThai.Enabled = true;
            guna2DateTimePicker1.Enabled = true;
            guna2DateTimePicker2.Enabled = true;
        }

        private bool ValidateInputsThemKeHoachDongQuy()
        {
            if (string.IsNullOrEmpty(inputTenKeHoach.Text))
            {
                MessageBox.Show("Tên kế hoạch không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputTenKeHoach.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(inputMoTa.Text))
            {
                MessageBox.Show("Mô tả không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputMoTa.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(inputSoTienCanDong.Text))
            {
                MessageBox.Show("Số tiền cần đóng không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputSoTienCanDong.Focus();
                return false;
            }

            if (!Regex.IsMatch(inputSoTienCanDong.Text, @"^\d+$"))
            {
                MessageBox.Show("Số tiền chỉ được chứa ký tự số!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputSoTienCanDong.Focus();
                return false;
            }


            return true;
        }

        private void btn_themKeHoachDongQuy_Click(object sender, EventArgs e)
        {
            comboBox_LocTrangThai.SelectedIndexChanged -= comboBox_TrangThai_SelectedIndexChanged; // Bật lại sự kiện

            DataTable dt = db.getSqlDataAdapter("Select max(MaKeHoach) from KeHoachDongQuy");
            var d = dt.Rows[0][0];
            int max = int.Parse(d.ToString().Substring(2));
            string maKH = "KH" + (max + 1).ToString("D3");

            if (!ValidateInputsThemKeHoachDongQuy())
            {
                return;
            }
           
            string tenKeHoach = inputTenKeHoach.Text;
            string mota = inputMoTa.Text;
            string soTien = inputSoTienCanDong.Text;
            string trangthai = comboBox_TrangThai.SelectedItem.ToString();
            string ngayBatDau = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
            string ngayKetThuc = guna2DateTimePicker2.Value.ToString("yyyy-MM-dd");

            string insert = "insert into KeHoachDongQuy (MaKeHoach, TenKeHoach, MoTa, NgayBatDau, NgayKetThuc, SoTienCanDong ,TrangThai) Values ('" + maKH + "', N'" + tenKeHoach + "', N'" + mota + "', '" + ngayBatDau + "', '" + ngayKetThuc + "', N'" + soTien + "', N'" + trangthai + "')";
            try
            {
                db.getSqlDataAdapter(insert);
                HienThi_DSKeHoachDongQuy();
                ResetForm();
                MessageBox.Show("Thêm kế hoạch đóng quỹ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            comboBox_LocTrangThai.SelectedIndexChanged += comboBox_TrangThai_SelectedIndexChanged; // Bật lại sự kiện
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            flag = true;
            btn_themKeHoachDongQuy.Enabled = false;
            inputMaKH.Enabled = true;
            inputTenKeHoach.Enabled = true;
            inputMoTa.Enabled = true;
            inputSoTienCanDong.Enabled = true;
            comboBox_TrangThai.Enabled = true;
            guna2DateTimePicker1.Enabled = true;
            guna2DateTimePicker2.Enabled = true;

            btn_luu.Enabled = true;
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            try
            {
                string update = "Update KeHoachDongQuy set TenKeHoach=N'" + inputTenKeHoach.Text + "', MoTa=N'" + inputMoTa.Text + "', SoTienCanDong='" + inputSoTienCanDong.Text + "', TrangThai=N'" + comboBox_TrangThai.Text + "', NgayBatDau=N'" + guna2DateTimePicker1.Value.ToString("yyyy-MM-dd") + "', NgayKetThuc='" + guna2DateTimePicker2.Value.ToString("yyyy-MM-dd")  + "' where MaKeHoach='" + inputMaKH.Text + "'";
                int kq = db.getNonQuery(update);
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThi_DSKeHoachDongQuy();
                btn_luu.Enabled = false;
                btn_themKeHoachDongQuy.Enabled = true;
                flag = false;
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dtg_DSKeHoachDongQuy.SelectedRows.Count > 0)
            {
                var selectedRow = dtg_DSKeHoachDongQuy.SelectedRows[0];
                if (selectedRow.Cells["Mã kế hoạch"].Value == null || string.IsNullOrWhiteSpace(selectedRow.Cells["Mã kế hoạch"].Value.ToString()))
                {
                    MessageBox.Show("Dòng này không có dữ liệu hợp lệ để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string maKeHoach = (dtg_DSKeHoachDongQuy.SelectedRows[0].Cells["Mã kế hoạch"].Value.ToString());

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa kế hoạch đóng quỹ này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    string query = "DELETE FROM KeHoachDongQuy WHERE MaKeHoach = @MaKeHoach";
                    string connectionString = @"Data Source = PHAMTHUAN\MSSQLSERVER01; Initial Catalog = QuanLyCauLacBo; User ID = sa; Password = 123";

                    try
                    {
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (var transaction = connection.BeginTransaction())
                            {
                                try
                                {
                                    using (var command = new SqlCommand(query, connection, transaction))
                                    {
                                        command.Parameters.AddWithValue("@MaKeHoach", maKeHoach);

                                        // Thực hiện xóa
                                        int rowsAffected = command.ExecuteNonQuery();

                                        if (rowsAffected > 0)
                                        {
                                            // Nếu xóa thành công, commit giao dịch
                                            transaction.Commit();
                                            MessageBox.Show("Xóa kế hoạch đóng quỹ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            // Cập nhật lại DataGridView
                                            HienThi_DSKeHoachDongQuy();
                                        }
                                        else
                                        {
                                            // Nếu không có dòng nào bị xóa, rollback giao dịch
                                            transaction.Rollback();
                                            MessageBox.Show("Không tìm thấy kế hoạch đóng quỹ để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                }
                                catch (SqlException ex)
                                {
                                    // Nếu có lỗi (chẳng hạn ràng buộc khóa ngoại), rollback giao dịch
                                    transaction.Rollback();
                                    if (ex.Number == 547) // Lỗi FK constraint (số 547 là lỗi khóa ngoại)
                                    {
                                        MessageBox.Show("Không thể xóa kế hoạch này vì có ràng buộc với bảng khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Có lỗi xảy ra khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Nếu không có dòng nào được chọn, thông báo cho người dùng
                MessageBox.Show("Vui lòng chọn một kế hoạch để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_themTVDongQuy_Click(object sender, EventArgs e)
        {
            string checkKeHoachQuery = "SELECT TrangThai FROM KeHoachDongQuy WHERE MaKeHoach = '" + inputMaKH.Text + "'";
            string insert = "INSERT INTO DongQuy (MaThanhVien, MaKeHoach) VALUES ('" + comboBoxChonThanhVien.SelectedValue.ToString() + "','" + inputMaKH.Text + "')"; 
            try
            {
                DataTable keHoachResult = db.getSqlDataAdapter(checkKeHoachQuery);
                if (keHoachResult.Rows[0]["TrangThai"].ToString() != "Đang hoạt động")
                {
                    MessageBox.Show("Kế hoạch đóng quỹ đã ngừng hoạt động. Không thể thêm thành viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataTable dt = db.getSqlDataAdapter(insert);
                HienThiDSThanhVienDongQuy(inputMaKH.Text);
                MessageBox.Show("Thêm thành viên vào kế hoạch đóng quỹ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm thành viên vào kế hoạch đóng quỹ. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_sua_dongquy_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
                btn_luu_dongquy.Enabled = true;
                dtg_dsDongQuy.Columns[1].ReadOnly = false;
                dtg_dsDongQuy.Columns[2].ReadOnly = false;
        }

        private void btn_luu_dongquy_Click(object sender, EventArgs e)
        {
            if (dtg_dsDongQuy.SelectedRows.Count > 0)
            {
                // Lấy thông tin của dòng được chọn
                DataGridViewRow selectedRow = dtg_dsDongQuy.SelectedRows[0];
                bool isChecked = false;
                if (selectedRow.Cells["Trạng thái"].Value != DBNull.Value)
                {
                    isChecked = Convert.ToBoolean(selectedRow.Cells["Trạng thái"].Value);
                }
                string hoTen = selectedRow.Cells["Họ tên"].Value.ToString();
                string matv = selectedRow.Cells["Mã thành viên"].Value.ToString();
                // Cập nhật dữ liệu cho dòng được chọn
                DateTime ngayDong = DateTime.Now;
                string ngayDongFormatted = ngayDong.ToString("yyyy-MM-dd");

                string update = string.Format(
                "UPDATE DongQuy SET TrangThai = {0}, NgayDong = '{1}' WHERE MaThanhVien = '{2}' AND MaKeHoach = '{3}'", 
                isChecked ? 1 : 0, ngayDongFormatted, matv, inputMaKH.Text);
                try
                {
                    DataTable dt = db.getSqlDataAdapter(update);
                    HienThiDSThanhVienDongQuy(inputMaKH.Text);
                    MessageBox.Show(string.Format("Cập nhật trạng thái cho thành viên {0} thành công!", hoTen),  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_luu_dongquy.Enabled = false;
                }
                catch (Exception ex)
                {
                     MessageBox.Show(string.Format("Không thể cập nhật trạng thái cho thành viên {0}. Vui lòng thử lại.", hoTen),  "Thông báo",  MessageBoxButtons.OK,  MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_xoadongquy_Click(object sender, EventArgs e)
        {
            if (dtg_dsDongQuy.SelectedRows.Count > 0)
            {
                // Lấy thông tin của dòng được chọn
                DataGridViewRow selectedRow = dtg_dsDongQuy.SelectedRows[0];
                string hoTen = selectedRow.Cells["Họ tên"].Value.ToString();
                string matv = selectedRow.Cells["Mã thành viên"].Value.ToString();

                // Kiểm tra điều kiện: NgayDong = NULL và TrangThai = 0
                string checkQuery = string.Format(
                    "SELECT 1 FROM DongQuy WHERE MaThanhVien = '{0}' AND MaKeHoach = '{1}' AND TrangThai IS NULL AND NgayDong IS NULL",
                    matv,
                    inputMaKH.Text
                );

                try
                {
                    DataTable checkResult = db.getSqlDataAdapter(checkQuery);

                    if (checkResult.Rows.Count > 0) // Điều kiện thỏa mãn
                    {
                        string deleteQuery = string.Format(
                            "DELETE FROM DongQuy WHERE MaThanhVien = '{0}' AND MaKeHoach = '{1}'", 
                            matv, 
                            inputMaKH.Text
                        );

                        db.getSqlDataAdapter(deleteQuery); // Thực hiện xóa
                        HienThiDSThanhVienDongQuy(inputMaKH.Text); // Cập nhật danh sách
                        MessageBox.Show(
                            string.Format("Xóa thành viên {0} khỏi kế hoạch đóng quỹ thành công!", hoTen), 
                            "Thông báo", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information
                        );
                    }
                    else // Điều kiện không thỏa mãn
                    {
                        MessageBox.Show(
                            string.Format("Không thể xóa thành viên {0}. Chỉ có thể xóa nếu NgayDong = NULL và TrangThai = 0.", hoTen), 
                            "Thông báo", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Warning
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        string.Format("Đã xảy ra lỗi khi thực hiện xóa thành viên {0}. Vui lòng thử lại.\nChi tiết lỗi: {1}", hoTen, ex.Message), 
                        "Thông báo", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error
                    );
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

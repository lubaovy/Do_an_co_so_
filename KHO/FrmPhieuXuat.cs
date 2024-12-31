using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHO
{
    public partial class FrmPhieuXuat : Form, IUserForm
    {
        PhieuXuatRepository pxRepo;
        UserRepository userRepo;
        KHRepository khRepo;
        DVTRepository dvtRepo;
        private Dictionary<int, string> dvtDictionary;
        HangHoaRepository hhRepo;
        PhieuXuatCTRepository pxctRepo;
        private User _currentUser;
        public FrmPhieuXuat()
        {
            InitializeComponent();
            pxRepo = new PhieuXuatRepository();
            userRepo = new UserRepository();
            khRepo = new KHRepository();
            dvtRepo = new DVTRepository();
            hhRepo = new HangHoaRepository();
            pxctRepo = new PhieuXuatCTRepository();
        }
        public void SetCurrentUser(User currentUser)
        {
            _currentUser = currentUser;
            ApplyPermissions();
        }
        private void ApplyPermissions()
        {
            if (_currentUser.Role == "Admin")
            {
                btnXoa.Enabled = true;
            }
            else if (_currentUser.Role == "Quản lý")
            {
                btnXoa.Enabled = true;
            }
            else
            {
                btnXoa.Enabled = false;
                nutXoa.Enabled = false;
            }
        }
        private void FrmPhieuXuat_Load(object sender, EventArgs e)
        {
            txtNgay.Value = DateTime.Now;
            LoadData();
            LoadDanhSachPhieuXuat();
            LoadDanhSachPhieuXuatCT();
            dvtDictionary = dvtRepo.GetDvtDictionary();
            LoadPhieuXuatSummary();
            ApplyPermissions();
        }
        private void LoadData()
        {
            cbNguoiXuat.DataSource = userRepo.GetAllUsers();
            cbNguoiXuat.DisplayMember = "Tên";
            cbNguoiXuat.ValueMember = "Id";


            cbKH.DataSource = khRepo.GetKhachHangs();
            cbKH.DisplayMember = "Ten";
            cbKH.ValueMember = "Id";
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.Add("Chờ duyệt");
            cbTrangThai.Items.Add("Hoàn thành");
            cbTrangThai.Items.Add("Hủy");

        }
        private void LoadDanhSachPhieuXuat()
        {
            dataGridView1.DataSource = pxRepo.GetPhieuXuat();
            dataGridView1.Columns["IdKhachHang"].Visible = false;
            dataGridView1.Columns["IdUser"].Visible = false;
            ClearForm();
        }
        private void LoadDanhSachPhieuXuatCT()
        {
            cbSoPhieu.DataSource = pxRepo.GetPhieuXuat();
            cbSoPhieu.DisplayMember = "Id";
            cbSoPhieu.ValueMember = "Id";
        }
        private void ClearForm()
        {
            cbNguoiXuat.SelectedIndex = -1;
            cbKH.SelectedIndex = -1;
            txtGhiChu.Clear();
            cbTrangThai.SelectedIndex = -1;
            cbSoPhieu.SelectedIndex = -1; // Chọn lại mục đầu tiên hoặc không chọn gì
            dataGridView3.Rows.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                PhieuXuatDto px = new PhieuXuatDto()
                {
                    NgayXuat = txtNgay.Value, // Ngày nhập
                    IdUser = (int)cbNguoiXuat.SelectedValue, // Id nhân viên (người nhập)
                    IdKhachHang = (int)cbKH.SelectedValue,
                    GhiChu = txtGhiChu.Text.Trim()
                };

                pxRepo.AddPhieuXuat(px); // Thêm vào CSDL
                MessageBox.Show("Thêm phiếu xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDanhSachPhieuXuat();
                LoadDanhSachPhieuXuatCT();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm phiếu xuất: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

                PhieuXuatDto px = pxRepo.GetPhieuXuatById(id);
                if (px != null)
                {
                    px.NgayXuat = txtNgay.Value; // Cập nhật ngày nhập
                    px.IdUser = (int)cbNguoiXuat.SelectedValue; // Cập nhật người nhập
                    px.IdKhachHang = (int)cbKH.SelectedValue;
                    px.GhiChu = txtGhiChu.Text.Trim();

                    pxRepo.UpdatePhieuXuat(px);
                    MessageBox.Show("Sửa phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachPhieuXuat();
                    LoadDanhSachPhieuXuatCT();
                    ClearForm();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                    pxRepo.DeletePhieuXuat(id);
                    LoadDanhSachPhieuXuat();
                    LoadDanhSachPhieuXuatCT();
                    ClearForm();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có dòng nào được chọn
            if (dataGridView1.CurrentRow != null)
            {
                // Lấy dữ liệu từ dòng được chọn
                var selectedRow = dataGridView1.CurrentRow;

                // Lấy dữ liệu từ các cột và hiển thị lên các TextBox, ComboBox
                txtNgay.Text = selectedRow.Cells["NgayXuat"].Value?.ToString();

                var selectedKHId = selectedRow.Cells["IdKhachHang"].Value;
                if (selectedKHId != null)
                {
                    cbKH.SelectedValue = Convert.ToInt32(selectedKHId);
                }

                var selectedUserId = selectedRow.Cells["IdUser"].Value;
                if (selectedUserId != null)
                {
                    cbNguoiXuat.SelectedValue = Convert.ToInt32(selectedUserId);
                }
            }
        }


        //TAB CHI TIET PHIEU XUAT
        private void nutLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị từ ComboBox, TextBox
                if (cbSoPhieu.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn số phiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idPhieuXuat = Convert.ToInt32(cbSoPhieu.SelectedValue);
                string trangThai = cbTrangThai.SelectedItem.ToString();

                // Danh sách chi tiết phiếu nhập
                List<PhieuXuatCTDto> danhSachChiTiet = new List<PhieuXuatCTDto>();

                // Lặp qua các dòng trong DataGridView
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    // Kiểm tra dòng trống (tất cả ô đều null hoặc rỗng)
                    if (row.IsNewRow || row.Cells.Cast<DataGridViewCell>().All(c => c.Value == null || string.IsNullOrWhiteSpace(c.Value.ToString())))
                    {
                        continue; // Bỏ qua dòng này
                    }
                    // Kiểm tra dòng có giá trị hợp lệ không
                    if (row.Cells["BarCode"].Value != null &&
                        row.Cells["SoLuong"].Value != null &&
                        row.Cells["Gia"].Value != null &&
                        row.Cells["IdHangHoa"].Value != null)
                    {
                        try
                        {
                            // Lấy giá trị từ các cột
                            var barCode = Convert.ToString(row.Cells["BarCode"].Value);
                            var soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                            var gia = Convert.ToDecimal(row.Cells["Gia"].Value);
                            var thanhTien = Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                            var idDVT = Convert.ToInt32(row.Cells["IdDVT"].Value);
                            var tenHH = Convert.ToString(row.Cells["Ten"].Value);
                            var idHH = Convert.ToInt32(row.Cells["IdHangHoa"].Value);

                            // Kiểm tra số lượng và giá trị
                            if (soLuong <= 0 || gia <= 0)
                            {
                                MessageBox.Show($"Dòng {row.Index + 1}: Số lượng hoặc giá trị không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }

                            // Tạo chi tiết phiếu nhập
                            PhieuXuatCTDto chiTiet = new PhieuXuatCTDto
                            {
                                IdPhieuXuat = idPhieuXuat,
                                IdHangHoa = idHH,
                                SoLuong = soLuong,
                                TongTien = thanhTien,
                                TrangThai = trangThai,
                                GiaXuat = gia,
                            };

                            danhSachChiTiet.Add(chiTiet);
                        }
                        catch (Exception innerEx)
                        {
                            MessageBox.Show($"Dòng {row.Index + 1} có lỗi: {innerEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                // Kiểm tra nếu không có chi tiết hợp lệ
                if (danhSachChiTiet.Count == 0)
                {
                    MessageBox.Show("Không có chi tiết nào để lưu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thêm toàn bộ danh sách chi tiết
                foreach (var chiTiet in danhSachChiTiet)
                {
                    pxctRepo.AddPhieuXuatCT(chiTiet);
                }

                // Hiển thị thông báo thành công
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhieuXuatSummary();
                ClearForm();
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi tổng quát
                MessageBox.Show($"Có lỗi khi lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nutXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                {
                    if (!row.IsNewRow) // Kiểm tra để không xóa dòng trống mặc định
                    {
                        dataGridView3.Rows.Remove(row);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void nutThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu cột Barcode đang được chỉnh sửa
            if (e.ColumnIndex == dataGridView3.Columns["Barcode"].Index)
            {
                string barcode = dataGridView3.Rows[e.RowIndex].Cells["Barcode"].Value?.ToString();

                // Nếu người dùng nhập dấu "."
                if (barcode == ".")
                {
                    // Hiển thị form chọn hàng hóa
                    using (var frmChonHangHoa = new FrmChonHangHoa())
                    {
                        if (frmChonHangHoa.ShowDialog() == DialogResult.OK)
                        {
                            // Lấy danh sách hàng hóa được chọn
                            var hangHoas = frmChonHangHoa.SelectedHangHoas;

                            // Ghi thông tin hàng hóa vào dòng hiện tại
                            if (hangHoas.Count > 0)
                            {
                                var hangHoa = hangHoas.First(); // Lấy hàng hóa đầu tiên
                                dataGridView3.Rows[e.RowIndex].Cells["Barcode"].Value = hangHoa.BarCode;
                                dataGridView3.Rows[e.RowIndex].Cells["Ten"].Value = hangHoa.Ten;
                                dataGridView3.Rows[e.RowIndex].Cells["IdDVT"].Value = hangHoa.IdDVT;
                                dataGridView3.Rows[e.RowIndex].Cells["Gia"].Value = hangHoa.Gia;
                                dataGridView3.Rows[e.RowIndex].Cells["ThanhTien"].Value = hangHoa.Gia;
                                dataGridView3.Rows[e.RowIndex].Cells["SoLuong"].Value = 1;
                                dataGridView3.Rows[e.RowIndex].Cells["IdHangHoa"].Value = hangHoa.Id;
                                dataGridView3.Rows[e.RowIndex].Cells["Barcode"].Tag = hangHoa.SoLuongTonKho;

                                // Nếu có nhiều hàng hóa được chọn, thêm các dòng mới
                                for (int i = 1; i < hangHoas.Count; i++)
                                {
                                    var additionalHangHoa = hangHoas[i];
                                    int rowIndex = dataGridView3.Rows.Add();
                                    dataGridView3.Rows[rowIndex].Cells["Barcode"].Value = additionalHangHoa.BarCode;
                                    dataGridView3.Rows[rowIndex].Cells["Ten"].Value = additionalHangHoa.Ten;
                                    dataGridView3.Rows[rowIndex].Cells["IdDVT"].Value = additionalHangHoa.IdDVT;
                                    dataGridView3.Rows[rowIndex].Cells["Gia"].Value = additionalHangHoa.Gia;
                                    dataGridView3.Rows[rowIndex].Cells["ThanhTien"].Value = additionalHangHoa.Gia;
                                    dataGridView3.Rows[rowIndex].Cells["SoLuong"].Value = 1;
                                    dataGridView3.Rows[rowIndex].Cells["IdHangHoa"].Value = additionalHangHoa.Id;
                                    dataGridView3.Rows[rowIndex].Cells["Barcode"].Tag = additionalHangHoa.SoLuongTonKho;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dataGridView3.Rows.Count == 0)
                return; // Không xử lý nếu không có dòng hoặc cột hợp lệ

            // Kiểm tra nếu cột đang thay đổi là "SoLuong"
            if (e.ColumnIndex == dataGridView3.Columns["SoLuong"].Index)
            {
                // Lấy giá trị số lượng người dùng nhập
                var soLuongCell = dataGridView3.Rows[e.RowIndex].Cells["SoLuong"].Value;
                var barcodeTag = dataGridView3.Rows[e.RowIndex].Cells["Barcode"].Tag; // Lấy số lượng tồn kho từ Tag

                if (int.TryParse(soLuongCell?.ToString(), out int soLuong) &&
                    int.TryParse(barcodeTag?.ToString(), out int soLuongTonKho))
                {
                    // Kiểm tra nếu số lượng cần xuất lớn hơn số lượng tồn kho
                    if (soLuong > soLuongTonKho)
                    {
                        MessageBox.Show("Số lượng cần xuất lớn hơn số lượng tồn kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView3.Rows[e.RowIndex].Cells["SoLuong"].Value = soLuongTonKho; // Reset về tối đa tồn kho
                    }
                    else
                    {
                        // Tính lại thành tiền nếu số lượng hợp lệ
                        var giaCell = dataGridView3.Rows[e.RowIndex].Cells["Gia"].Value;
                        if (decimal.TryParse(giaCell?.ToString(), out decimal gia))
                        {
                            dataGridView3.Rows[e.RowIndex].Cells["ThanhTien"].Value = soLuong * gia;
                        }
                    }
                }
            }
        }

        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((dataGridView3.Columns[e.ColumnIndex].Name == "Gia" || dataGridView3.Columns[e.ColumnIndex].Name == "ThanhTien") && e.Value != null)
            {
                e.CellStyle.Format = "N0"; // Định dạng giá theo nghìn (VD: 1,000,000)
            }
            if (dataGridView3.Columns[e.ColumnIndex].Name == "IdDVT" && e.Value != null)
            {
                int idDVT = (int)e.Value;
                if (dvtDictionary.TryGetValue(idDVT, out string dvtName))
                {
                    e.Value = dvtName; // Hiển thị tên thay vì Id
                    e.FormattingApplied = true;
                }
            }
        }


        //TAB DANH SACH
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "TongTien" && e.Value != null)
            {
                e.CellStyle.Format = "N0"; // Định dạng giá theo nghìn (VD: 1,000,000)
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView2.Rows[e.RowIndex].DataBoundItem as PhieuXuatCTDto;

                if (selectedRow != null)
                {
                    cboNguoiXuat.Text = selectedRow.TenUser;
                    cboKH.Text = selectedRow.TenKH;
                }
            }
        }
        private void LoadPhieuXuatSummary()
        {
            //DateTime startDate = dateTimePicker1.Value.Date;
            //DateTime endDate = dateTimePicker2.Value.Date;
            // Gọi hàm lấy dữ liệu tổng hợp
            var phieuXuatSummaryList = pxctRepo.GetPhieuXuatCT();

            // Đổ dữ liệu vào DataGridView
            dataGridView2.DataSource = phieuXuatSummaryList;
            dataGridView2.Columns["IdHangHoa"].Visible = false;
            dataGridView2.Columns["GiaXuat"].Visible = false;
            dataGridView2.Columns["TenKH"].Visible = false;
            dataGridView2.Columns["TenUser"].Visible = false;
            dataGridView2.Columns["Id"].Visible = false;
            dataGridView2.Columns["BarCode"].Visible = false;
            dataGridView2.Columns["TenHH"].Visible = false;
            dataGridView2.Columns["IdDVT"].Visible = false;
            dataGridView2.Columns["GhiChu"].DisplayIndex = 4;

            dateTimePicker1.Value = DateTime.Now.AddDays(-30); // Ngày bắt đầu: 30 ngày trước
            dateTimePicker2.Value = DateTime.Now;             // Ngày kết thúc: ngày hiện tại

            if (phieuXuatSummaryList.Any())
            {
                var firstPhieuXuat = phieuXuatSummaryList.FirstOrDefault();
                cboNguoiXuat.Text = firstPhieuXuat.TenUser;
                cboKH.Text = firstPhieuXuat.TenKH;
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem dòng được nhấp có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy IdPhieuNhap từ DataGridView
                int idPhieuXuat = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["IdPhieuXuat"].Value);

                // Gọi hàm hiển thị chi tiết phiếu nhập
                LoadChiTietPhieuXuat(idPhieuXuat);
                // Chuyển sang tab ChiTietPhieuNhap
                tabControl1.SelectedIndex = 1;
            }
        }
        private void LoadChiTietPhieuXuat(int idPhieuXuat)
        {
            // Lấy dữ liệu chi tiết phiếu nhập từ cơ sở dữ liệu
            var chiTietPhieuXuat = pxctRepo.GetChiTietPhieuXuatById(idPhieuXuat);

            // Gán dữ liệu vào DataGridView (hoặc các điều khiển khác)
            dataGridView3.DataSource = chiTietPhieuXuat;

            dataGridView3.AutoGenerateColumns = false; // Ngăn tự động tạo cột
            dataGridView3.Columns["Id"].Visible = false;
            dataGridView3.Columns["IdPhieuXuat"].Visible = false;
            dataGridView3.Columns["TrangThai"].Visible = false;
            dataGridView3.Columns["GhiChu"].Visible = false;
            dataGridView3.Columns["NgayXuat"].Visible = false;
            dataGridView3.Columns["TenKH"].Visible = false;
            dataGridView3.Columns["TenUser"].Visible = false;
            dataGridView3.Columns["IdHangHoa"].Visible = false;
            cbSoPhieu.DataSource = chiTietPhieuXuat;
            cbSoPhieu.DisplayMember = "IdPhieuXuat";
            cbSoPhieu.ValueMember = "IdPhieuXuat";
            cbTrangThai.DataSource = chiTietPhieuXuat;
            cbTrangThai.ValueMember = "TrangThai";
        }
    }
}

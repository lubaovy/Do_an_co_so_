using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHO
{
    public partial class FrmPhieuNhap : Form, IUserForm
    {
        private PhieuNhapRepository pnRepo;
        private NCCRepository nccRepo;
        private UserRepository userRepo;
        private PhieuNhapCTRepository pnctRepo;
        private HangHoaRepository hangHoaRepo;
        private DVTRepository dVTRepo;
        private Dictionary<int, string> dvtDictionary;
        private User _currentUser;
        public FrmPhieuNhap()
        {
            InitializeComponent();
            pnRepo = new PhieuNhapRepository();
            nccRepo = new NCCRepository();
            userRepo = new UserRepository();
            hangHoaRepo = new HangHoaRepository();
            pnctRepo = new PhieuNhapCTRepository();
            dVTRepo = new DVTRepository();
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
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmPhieuNhap_Load(object sender, EventArgs e)
        {
            // Gán ngày hiện tại
            txtNgay.Value = DateTime.Now;
            LoadData();
            // Hiển thị danh sách phiếu nhập
            LoadDanhSachPhieuNhap();
            LoadDanhSachPhieuNhapCT();
            dvtDictionary = dVTRepo.GetDvtDictionary();
            LoadPhieuNhapSummary();
            ClearForm();
        }
        private void LoadData()
        {
            cbNguoiNhap.DataSource = userRepo.GetAllUsers();
            cbNguoiNhap.DisplayMember = "Tên";
            cbNguoiNhap.ValueMember = "Id";
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.Add("Chờ duyệt");
            cbTrangThai.Items.Add("Hoàn thành");
            cbTrangThai.Items.Add("Hủy");
            cbNCC.DataSource = nccRepo.GetNCCs();
            cbNCC.DisplayMember = "Ten";
            cbNCC.ValueMember = "Id";
        }
        private void LoadDanhSachPhieuNhap()
        {
            dataGridView1.DataSource = pnRepo.GetPhieuNhap();
            dataGridView1.Columns["IdNCC"].Visible = false;
            dataGridView1.Columns["IdUser"].Visible = false;
            dataGridView1.Columns["IdDVT"].Visible = false;
            dataGridView1.Columns["TenDVT"].Visible = false;
        }
        private void LoadDanhSachPhieuNhapCT()
        {
            cbSoPhieu.DataSource = pnRepo.GetPhieuNhap();
            cbSoPhieu.DisplayMember = "Id";
            cbSoPhieu.ValueMember = "Id";
        }
        private void LoadPhieuNhapSummary()
        {
            //DateTime startDate = dateTimePicker1.Value.Date;
            //DateTime endDate = dateTimePicker2.Value.Date;
            // Gọi hàm lấy dữ liệu tổng hợp
            var phieuNhapSummaryList = pnctRepo.GetPhieuNhapCT();

            // Đổ dữ liệu vào DataGridView
            dataGridView2.DataSource = phieuNhapSummaryList;
            dataGridView2.Columns["IdHangHoa"].Visible = false;
            dataGridView2.Columns["ThanhTien"].Visible = false;
            dataGridView2.Columns["GiaNhap"].Visible = false;
            dataGridView2.Columns["TenNCC"].Visible = false;
            dataGridView2.Columns["TenUser"].Visible = false;
            dataGridView2.Columns["Id"].Visible = false;
            dataGridView2.Columns["BarCode"].Visible = false;
            dataGridView2.Columns["TenHH"].Visible = false;
            dataGridView2.Columns["IdDVT"].Visible = false;
            dataGridView2.Columns["TongSL"].DisplayIndex = 3;
            dateTimePicker1.Value = DateTime.Now.AddDays(-30); // Ngày bắt đầu: 30 ngày trước
            dateTimePicker2.Value = DateTime.Now;             // Ngày kết thúc: ngày hiện tại

            if (phieuNhapSummaryList.Any())
            {
                // Hiển thị thông tin người nhập và nhà cung cấp của phiếu đầu tiên
                var firstPhieuNhap = phieuNhapSummaryList.FirstOrDefault();
                cboNguoiNhap.Text = firstPhieuNhap.TenUser;
                cboNCC.Text = firstPhieuNhap.TenNCC;
            }
        }
        private void ClearForm()
        {
            cbNguoiNhap.SelectedIndex = -1;
            cbNCC.SelectedIndex = -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                PhieuNhapDto pn = new PhieuNhapDto()
                {
                    NgayNhap = txtNgay.Value, // Ngày nhập
                    IdUser = (int)cbNguoiNhap.SelectedValue, // Id nhân viên (người nhập)
                    IdNCC = (int)cbNCC.SelectedValue // Id nhà cung cấp
                };

                pnRepo.AddPhieuNhap(pn); // Thêm vào CSDL
                MessageBox.Show("Thêm phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDanhSachPhieuNhap(); // Cập nhật lại danh sách phiếu nhập
                LoadDanhSachPhieuNhapCT();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm phiếu nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

                PhieuNhapDto pn = pnRepo.GetPhieuNhapById(id);
                if (pn != null)
                {
                    pn.NgayNhap = txtNgay.Value; // Cập nhật ngày nhập
                    pn.IdUser = (int)cbNguoiNhap.SelectedValue; // Cập nhật người nhập
                    pn.IdNCC = (int)cbNCC.SelectedValue; // Cập nhật nhà cung cấp

                    pnRepo.UpdatePhieuNhap(pn);
                    MessageBox.Show("Sửa phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachPhieuNhap();
                    LoadDanhSachPhieuNhapCT();
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
                    pnRepo.DeletePhieuNhap(id);
                    LoadDanhSachPhieuNhap();
                    LoadDanhSachPhieuNhapCT();
                    ClearForm();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có dòng nào được chọn
            if (dataGridView1.CurrentRow != null)
            {
                // Lấy dữ liệu từ dòng được chọn
                var selectedRow = dataGridView1.CurrentRow;

                // Lấy dữ liệu từ các cột và hiển thị lên các TextBox, ComboBox
                txtNgay.Text = selectedRow.Cells["NgayNhap"].Value?.ToString();

                // Gán giá trị cho ComboBox NCC (Nhà cung cấp)
                var selectedNCCId = selectedRow.Cells["IdNCC"].Value;
                if (selectedNCCId != null)
                {
                    cbNCC.SelectedValue = Convert.ToInt32(selectedNCCId);
                }

                // Gán giá trị cho ComboBox DVT (Đơn vị tính)
                var selectedUserId = selectedRow.Cells["IdUser"].Value;
                if (selectedUserId != null)
                {
                    cbNguoiNhap.SelectedValue = Convert.ToInt32(selectedUserId);
                }
            }
        }

        //tab chitietphieunhap
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
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.Rows.Count == 0 || e.RowIndex < 0 || e.ColumnIndex < 0)
                return; // Không xử lý nếu chưa có dòng nào hoặc không có cột nào
            // Xác định cột đang thay đổi có phải là "SoLuong"
            if (e.ColumnIndex == dataGridView3.Columns["SoLuong"].Index && e.RowIndex >= 0)
            {
                // Lấy giá trị Số Lượng
                var soLuongCell = dataGridView3.Rows[e.RowIndex].Cells["SoLuong"].Value;
                var giaCell = dataGridView3.Rows[e.RowIndex].Cells["Gia"].Value;

                if (soLuongCell != null && giaCell != null)
                {
                    // Tính Thành Tiền: Số Lượng × Giá
                    decimal soLuong = Convert.ToDecimal(soLuongCell);
                    decimal gia = Convert.ToDecimal(giaCell);
                    decimal thanhTien = soLuong * gia;

                    // Cập nhật cột Thành Tiền
                    dataGridView3.Rows[e.RowIndex].Cells["ThanhTien"].Value = thanhTien;
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

                int idPhieuNhap = Convert.ToInt32(cbSoPhieu.SelectedValue);
                string trangThai = cbTrangThai.SelectedItem.ToString();
                string ghiChu = txtGhiChu.Text;

                // Danh sách chi tiết phiếu nhập
                List<PhieuNhapCTDto> danhSachChiTiet = new List<PhieuNhapCTDto>();

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
                            PhieuNhapCTDto chiTiet = new PhieuNhapCTDto
                            {
                                IdPhieuNhap = idPhieuNhap,
                                IdHangHoa = idHH,
                                SoLuong = soLuong,
                                ThanhTien = thanhTien,
                                TrangThai = trangThai,
                                GhiChu = ghiChu,
                                GiaNhap = gia,
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
                    pnctRepo.AddPhieuNhapCT(chiTiet);
                }

                // Hiển thị thông báo thành công
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhieuNhapSummary();
                ResetForm();
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi tổng quát
                MessageBox.Show($"Có lỗi khi lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ResetForm()
        {
            cbTrangThai.SelectedIndex = -1;
            txtGhiChu.Clear();

            // Làm trống ComboBox
            cbSoPhieu.SelectedIndex = -1; // Chọn lại mục đầu tiên hoặc không chọn gì

            // Làm trống DataGridView
            dataGridView3.Rows.Clear();

            // Nếu có các control khác cần làm trống thì thêm vào
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Lấy dữ liệu từ dòng được chọn
                var selectedRow = dataGridView1.CurrentRow;
            }
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
                var selectedRow = dataGridView2.Rows[e.RowIndex].DataBoundItem as PhieuNhapCTDto;

                if (selectedRow != null)
                {
                    cboNguoiNhap.Text = selectedRow.TenUser;
                    cboNCC.Text = selectedRow.TenNCC;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem dòng được nhấp có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy IdPhieuNhap từ DataGridView
                int idPhieuNhap = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["IdPhieuNhap"].Value);

                // Gọi hàm hiển thị chi tiết phiếu nhập
                LoadChiTietPhieuNhap(idPhieuNhap);
                // Chuyển sang tab ChiTietPhieuNhap
                tabControl1.SelectedIndex = 1;
            }
        }
        private void LoadChiTietPhieuNhap(int idPhieuNhap)
        {
            // Lấy dữ liệu chi tiết phiếu nhập từ cơ sở dữ liệu
            var chiTietPhieuNhap = pnctRepo.GetChiTietPhieuNhapById(idPhieuNhap);

            // Gán dữ liệu vào DataGridView (hoặc các điều khiển khác)
            dataGridView3.DataSource = chiTietPhieuNhap;

            dataGridView3.AutoGenerateColumns = false; // Ngăn tự động tạo cột
            dataGridView3.Columns["Id"].Visible = false;        
            dataGridView3.Columns["IdPhieuNhap"].Visible = false;
            dataGridView3.Columns["TrangThai"].Visible = false;
            dataGridView3.Columns["GhiChu"].Visible = false;
            dataGridView3.Columns["NgayNhap"].Visible = false;
            dataGridView3.Columns["TongTien"].Visible = false;
            dataGridView3.Columns["TenNCC"].Visible = false;
            dataGridView3.Columns["TenUser"].Visible = false;

            cbSoPhieu.DataSource = chiTietPhieuNhap;
            cbSoPhieu.DisplayMember = "IdPhieuNhap";
            cbSoPhieu.ValueMember = "IdPhieuNhap";

            cbTrangThai.DataSource = chiTietPhieuNhap;
            cbTrangThai.ValueMember = "TrangThai";
            txtGhiChu.Text = chiTietPhieuNhap.FirstOrDefault()?.GhiChu;
        }

    }
}

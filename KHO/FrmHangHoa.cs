using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace KHO
{
    public partial class FrmHangHoa : Form, IUserForm
    {
        private HangHoaRepository hangHoaRepo;
        private DVTRepository dvtRepo;
        private NCCRepository nccRepo;
        private User _currentUser;
        public FrmHangHoa()
        {
            InitializeComponent();
            hangHoaRepo = new HangHoaRepository();
            dvtRepo = new DVTRepository();
            nccRepo = new NCCRepository();
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
            }
        }
        private void FrmHangHoa_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBoxData();
            ClearForm();
        }
        private void LoadData()
        {
            dataGridView1.DataSource = hangHoaRepo.GetHangHoas(); // Lấy danh sách hàng hóa từ csdl
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["IdDVT"].Visible = false;
            dataGridView1.Columns["IdNCC"].Visible = false;
            dataGridView1.Columns["Gia"].Visible = false;
            dataGridView1.Columns["Ten"].DisplayIndex = 2;
            dataGridView1.Columns["IdDVT"].DisplayIndex = 3;
        }
        private void ClearForm()
        {
            txtTen.Clear();
            txtBarcode.Clear();
            txtGia.Clear();
            txtMoTa.Clear();
            cbDVT.SelectedIndex = -1;
            cbNCC.SelectedIndex = -1;
        }

        private void LoadComboBoxData()
        {
            cbDVT.DataSource = dvtRepo.GetDVTs(); // Lấy danh sách đơn vị tính
            cbDVT.DisplayMember = "Ten";
            cbDVT.ValueMember = "Id";

            cbNCC.DataSource = nccRepo.GetNCCs(); // Lấy danh sách nhà cung cấp
            cbNCC.DisplayMember = "Ten";
            cbNCC.ValueMember = "Id";
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //_them = true;
            //showHideControl(false);
            //_edControl(true);
            HangHoaDto hangHoa = new HangHoaDto
            {
                Ten = txtTen.Text.Trim(),
                BarCode = txtBarcode.Text.Trim(),
                Gia = decimal.TryParse(txtGia.Text, out decimal gia) ? gia : (decimal?)null,
                MoTa = txtMoTa.Text.Trim(),
                IdDVT = (int)cbDVT.SelectedValue,
                IdNCC = (int)cbNCC.SelectedValue,
                SoLuongTonKho = 0 // Số lượng mặc định khi thêm mới
            };

            hangHoaRepo.AddHangHoa(hangHoa);
            LoadData();
            ClearForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

                HangHoaDto hangHoa = hangHoaRepo.GetHangHoaById(id);
                if (hangHoa != null)
                {
                    hangHoa.Ten = txtTen.Text.Trim();
                    hangHoa.BarCode = txtBarcode.Text.Trim();
                    hangHoa.Gia = decimal.TryParse(txtGia.Text, out decimal gia) ? gia : (decimal?)null;
                    hangHoa.MoTa = txtMoTa.Text.Trim();
                    hangHoa.IdDVT = (int)cbDVT.SelectedValue;
                    hangHoa.IdNCC = (int)cbNCC.SelectedValue;

                    hangHoaRepo.UpdateHangHoa(hangHoa);
                    LoadData();
                    ClearForm();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)   
        {
            // Tùy thuộc vào yêu cầu, hàm này có thể lưu tất cả thay đổi vào cơ sở dữ liệu
            MessageBox.Show("Dữ liệu đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //_them = false;
            //showHideControl(true);
            //LoadData();
            //_edControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn xóa không?","Thông báo",MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                    hangHoaRepo.DeleteHangHoa(id);
                    LoadData();
                    ClearForm();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTim.Text.Trim();
            dataGridView1.DataSource = hangHoaRepo.SearchHangHoa(keyword);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "GiaFormatted" && e.Value != null)
            {
                e.CellStyle.Format = "N0"; // Định dạng giá theo nghìn (VD: 1,000,000)
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
                txtTen.Text = selectedRow.Cells["Ten"].Value?.ToString();
                txtGia.Text = selectedRow.Cells["Gia"].Value?.ToString();
                txtMoTa.Text = selectedRow.Cells["MoTa"].Value?.ToString();
                txtBarcode.Text = selectedRow.Cells["Barcode"].Value?.ToString();

                // Gán giá trị cho ComboBox NCC (Nhà cung cấp)
                var selectedNCCId = selectedRow.Cells["IdNCC"].Value;
                if (selectedNCCId != null)
                {
                    cbNCC.SelectedValue = Convert.ToInt32(selectedNCCId);
                }

                // Gán giá trị cho ComboBox DVT (Đơn vị tính)
                var selectedDVTId = selectedRow.Cells["IdDVT"].Value;
                if (selectedDVTId != null)
                {
                    cbDVT.SelectedValue = Convert.ToInt32(selectedDVTId);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Cấu hình giấy phép trước khi dùng EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Lấy danh sách hàng hóa từ DataGridView
            var hangHoas = (List<HangHoaDto>)dataGridView1.DataSource;

            // Chọn nơi lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                Title = "Lưu file Excel",
                FileName = "HangHoa.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Tạo file Excel
                    using (var package = new OfficeOpenXml.ExcelPackage())
                    {
                        // Tạo sheet
                        var worksheet = package.Workbook.Worksheets.Add("Hàng Hóa");

                        // Tiêu đề cột
                        worksheet.Cells[1, 1].Value = "BarCode";
                        worksheet.Cells[1, 2].Value = "Tên Hàng";
                        worksheet.Cells[1, 3].Value = "Nhà Cung Cấp";
                        worksheet.Cells[1, 4].Value = "Đơn Vị Tính";
                        worksheet.Cells[1, 5].Value = "Giá";
                        worksheet.Cells[1, 6].Value = "Mô Tả";
                        worksheet.Cells[1, 7].Value = "Số Lượng Tồn Kho";

                        // Định dạng tiêu đề
                        using (var range = worksheet.Cells[1, 1, 1, 7])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }

                        // Ghi dữ liệu
                        int row = 2;
                        foreach (var hh in hangHoas)
                        {
                            worksheet.Cells[row, 1].Value = hh.BarCode;
                            worksheet.Cells[row, 2].Value = hh.Ten;
                            worksheet.Cells[row, 3].Value = hh.TenNCC;
                            worksheet.Cells[row, 4].Value = hh.TenDVT;
                            worksheet.Cells[row, 5].Value = hh.Gia.HasValue ? hh.Gia.Value.ToString("N0") : "0";
                            worksheet.Cells[row, 6].Value = hh.MoTa;
                            worksheet.Cells[row, 7].Value = hh.SoLuongTonKho;
                            row++;
                        }

                        // Tự động điều chỉnh kích thước cột
                        worksheet.Cells.AutoFitColumns();

                        // Lưu file
                        var file = new System.IO.FileInfo(saveFileDialog.FileName);
                        package.SaveAs(file);

                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Lấy giá trị số lượng từ hàng hiện tại
            var soLuongCell = dataGridView1.Rows[e.RowIndex].Cells["SoLuongTonKho"].Value;
            if (soLuongCell != null && int.TryParse(soLuongCell.ToString(), out int soLuong))
            {
                if (soLuong < 5) // Kiểm tra nếu số lượng nhỏ hơn 5
                {
                    // Tải biểu tượng cảnh báo
                    Image warningIcon = Properties.Resources.warning;

                    // Xác định vùng cần vẽ (trong RowHeader)
                    int iconWidth = 20;
                    int iconHeight = 20;
                    Rectangle rowHeaderBounds = new Rectangle(
                        e.RowBounds.Left + 4, // Khoảng cách từ lề trái của RowHeader
                        e.RowBounds.Top + (e.RowBounds.Height - iconHeight) / 2, // Căn giữa theo chiều dọc
                        iconWidth,
                        iconHeight
                    );

                    // Vẽ biểu tượng cảnh báo
                    e.Graphics.DrawImage(warningIcon, rowHeaderBounds);
                }
            }
        }
    }
}

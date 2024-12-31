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
    public partial class FrmChonHangHoa : Form
    {
        public List<HangHoaDto> SelectedHangHoas { get; private set; }
        private HangHoaRepository hangHoaRepo;
        private DVTRepository dvtRepo;
        public FrmChonHangHoa()
        {
            InitializeComponent();
            hangHoaRepo = new HangHoaRepository();
            dvtRepo = new DVTRepository();
        }

        private void FrmChonHangHoa_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            dataGridView1.DataSource = hangHoaRepo.GetHangHoas();
            dataGridView1.Columns["Id"].Visible = false;
            //dataGridView1.Columns["SoLuongTonKho"].Visible = false;
            dataGridView1.Columns["IdDVT"].Visible = false;
            dataGridView1.Columns["IdNCC"].Visible = false;
            dataGridView1.Columns["Gia"].Visible = false;
            dataGridView1.Columns["MoTa"].Visible = false;
            dataGridView1.Columns["TenNCC"].Visible = false;
            dataGridView1.Columns["Ten"].DisplayIndex = 2;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var selectedHangHoas = new List<HangHoaDto>();

            // Duyệt qua tất cả các dòng được chọn
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                selectedHangHoas.Add(new HangHoaDto
                {
                    Id = Convert.ToInt32(row.Cells["Id"].Value),
                    Ten = row.Cells["Ten"].Value.ToString(),
                    BarCode = row.Cells["BarCode"].Value.ToString(),
                    Gia = Convert.ToDecimal(row.Cells["Gia"].Value),
                    IdDVT = Convert.ToInt32(row.Cells["IdDVT"].Value),
                    SoLuongTonKho = Convert.ToInt32(row.Cells["SoLuongTonKho"].Value)
                });
            }

            if (selectedHangHoas.Count > 0)
            {
                // Xử lý danh sách hàng hóa được chọn
                SelectedHangHoas = selectedHangHoas;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một hàng hóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

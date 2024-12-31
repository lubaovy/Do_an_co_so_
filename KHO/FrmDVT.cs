using QLKHO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHO
{
    public partial class FrmDVT : Form
    {
        private int selectedDVTId; // Lưu Id của đơn vị tính đang chọn
        public FrmDVT()
        {
            InitializeComponent();
        }

        private void FrmDVT_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            using (var repository = new DVTRepository())
            {
                //var dVTs = repository.GetDVTs();
                //dataGridView1.Rows.Clear();
                //foreach (var item in dVTs)
                //{
                //    // Giả sử bạn có các cột là: Id, Ten
                //    dataGridView1.Rows.Add(item.Id, item.Ten);
                //}
                // Chỉ lấy cột Id và Ten từ bảng DVT
                var list = repository.GetDVTs().Select(d => new { d.Id, d.Ten }).ToList();
                dataGridView1.DataSource = list;

                // Ẩn cột Id trong DataGridView để không hiển thị cho người dùng
                dataGridView1.Columns["Id"].Visible = false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DVTRepository repository = new DVTRepository();
        private void btnThem_Click(object sender, EventArgs e)
        {
            var newDVT = new DVT { Ten = txtTen.Text.Trim() };
            repository.AddDonViTinh(newDVT);
            LoadData();
            txtTen.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var dvtToUpdate = new DVT { Id = selectedDVTId, Ten = txtTen.Text.Trim() };
            repository.UpdateDonViTinh(dvtToUpdate);
            LoadData();
            txtTen.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem hàng có hợp lệ
            {
                // Lấy dòng đã chọn và gán Id cùng tên đơn vị tính vào textbox
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedDVTId = Convert.ToInt32(row.Cells["Id"].Value);
                txtTen.Text = row.Cells["Ten"].Value.ToString();
            }
        }
    }
}

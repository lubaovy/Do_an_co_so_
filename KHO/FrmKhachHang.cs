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
    public partial class FrmKhachHang : Form, IUserForm
    {
        private KHRepository _repository;
        private User _currentUser;
        public FrmKhachHang()
        {
            InitializeComponent();
            _repository = new KHRepository();
            LoadData();
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
        private void LoadData()
        {
            dataGridView1.DataSource = _repository.GetKhachHangs();
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["DiaChi"].DisplayIndex = 4;
            dataGridView1.Columns["MoreInfo"].DisplayIndex = 5;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var kh = new KhachHangDto
            {
                Ten = txtTen.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                Phone = txtDienThoai.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                MoreInfo = txtGhiChu.Text.Trim()
            };
            _repository.AddKH(kh);
            LoadData();
            ClearTextBoxes();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var kh = new KhachHangDto
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value),
                    Ten = txtTen.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    Phone = txtDienThoai.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    MoreInfo = txtGhiChu.Text.Trim()
                };
                _repository.UpdateKH(kh);
                LoadData();
                ClearTextBoxes();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                _repository.DeleteKH(id);
                LoadData();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtTen.Text = dataGridView1.CurrentRow.Cells["Ten"].Value.ToString();
                txtDiaChi.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
                txtDienThoai.Text = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
                txtEmail.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
                txtGhiChu.Text = dataGridView1.CurrentRow.Cells["MoreInfo"].Value.ToString();
            }
        }
        private void ClearTextBoxes()
        {
            txtTen.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtEmail.Clear();
            txtGhiChu.Clear();
        }

        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }
    }
}

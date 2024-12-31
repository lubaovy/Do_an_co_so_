using KHO.DTO;
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
    public partial class FrmUser : Form
    {
        UserRepository userRepo;
        public FrmUser()
        {
            InitializeComponent();
            userRepo = new UserRepository();
        }
        private void LoadData()
        {
            dataGridView1.DataSource = userRepo.GetAllUsers();
            dataGridView1.Columns["Id"].Visible = false;
            cbChucVu.Items.Clear();
            cbChucVu.Items.Add("Admin");
            cbChucVu.Items.Add("Quản lý");
            cbChucVu.Items.Add("Nhân viên");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var user = new UserDto
            {
                Tên = txtUsername.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim(),
                Role = cbChucVu.SelectedItem.ToString()
            };
            userRepo.AddUser(user);
            LoadData();
            ClearTextBoxes();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var user = new UserDto
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value),
                    Tên = txtUsername.Text.Trim(),
                    MatKhau = txtMatKhau.Text.Trim(),
                    Role = cbChucVu.SelectedItem.ToString() 
                };
                userRepo.UpdateUser(user);
                LoadData();
                ClearTextBoxes();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                userRepo.DeleteUser(id);
                LoadData();
            }
        }
        private void ClearTextBoxes()
        {
            txtMatKhau.Clear();
            txtUsername.Clear();
            cbChucVu.SelectedIndex = -1;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtUsername.Text = dataGridView1.CurrentRow.Cells["Tên"].Value.ToString();
                txtMatKhau.Text = dataGridView1.CurrentRow.Cells["MatKhau"].Value.ToString();
                cbChucVu.SelectedItem = dataGridView1.CurrentRow.Cells["Role"].Value.ToString();
            }
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            LoadData();
            ClearTextBoxes() ;
        }
    }
}

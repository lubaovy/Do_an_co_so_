using QLKHO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHO
{
    public partial class MainForm : Form
    {
        private User _currentUser;
        public MainForm(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            ApplyPermissions();
        }
        private void ApplyPermissions()
        {
            if (_currentUser.Role == "Admin")
            {
                ngườiDùngToolStripMenuItem.Enabled = true;
            }
            else if (_currentUser.Role == "Quản lý")
            {
                ngườiDùngToolStripMenuItem.Enabled= false;
            }
            else
            {
                ngườiDùngToolStripMenuItem.Enabled = false;
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void OpenForm<T>() where T : Form, new()
        {
            Form form = Application.OpenForms.OfType<T>().FirstOrDefault();
            if (form == null)
            {
                form = new T();
                if (form is IUserForm userForm)
                {
                    userForm.SetCurrentUser(_currentUser);
                }
                form.Show();
            }
            else
            {
                form.Activate();
            }
        }
        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<FrmKhachHang>();
        }

        private void ngườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<FrmUser>();
        }

        private void hàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<FrmHangHoa>();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<FrmNhaCungCap>();
        }


        private void phiếuXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<FrmPhieuXuat>();
        }

        private void tồnKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void đơnVịTínhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<FrmDVT>();
        }

        private void phieuNhapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<FrmPhieuNhap>();
        }
    }
}

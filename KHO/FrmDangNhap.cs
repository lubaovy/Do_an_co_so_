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
    public partial class FrmDangNhap : Form
    {
        public User CurrentUser { get; private set; } // Thuộc tính chứa thông tin người dùng
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi UserRepository để xác thực
            var userRepository = new UserRepository();
            var user = userRepository.AuthenticateUser(username, password);

            if (user != null)
            {
                CurrentUser = user; // Lưu thông tin người dùng
                MessageBox.Show($"Chào mừng {user.Tên}!", "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK; // Đặt kết quả đăng nhập thành công
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

        }
    }
}

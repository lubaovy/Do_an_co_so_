using QLKHO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHO
{
    public partial class frmConnect : Form
    {
        public frmConnect()
        {
            InitializeComponent();
        }

        SqlConnection GetConnection(string server, string username, string password, string database)
        {
            return new SqlConnection("Data Source=" + server + "; Initial Catalog=" + database + "; User ID=" + username + "; Password=" + password + ";");
        }

        private void frmConnect_Load(object sender, EventArgs e)
        {

        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            SqlConnection connection = GetConnection(txtServ.Text, txtUsername.Text, txtPass.Text, cbBoxData.Text);
            try
            {
                connection.Open();
                MessageBox.Show("Kết nối thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Kết nối thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string enCryptServ = Encryptor.Encrypt(txtServ.Text, "qweryuiop", true);
            string enCryptUser = Encryptor.Encrypt(txtUsername.Text, "qweryuiop", true);
            string enCryptPass = Encryptor.Encrypt(txtPass.Text, "qweryuiop", true);
            string enCryptData = Encryptor.Encrypt(cbBoxData.Text, "qweryuiop", true);
            connect cn = new connect(enCryptServ, enCryptUser, enCryptPass, enCryptData);
            cn.SaveFile();
            MessageBox.Show("Lưu file thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbBoxData_Click(object sender, EventArgs e)
        {
            cbBoxData.Items.Clear();
            try
            {
                string ketNoi = "Server=LAPTOP-REKF3LEK\\SQLEXPRESS;User Id=" + txtUsername.Text + ";Password=" + txtPass.Text + ";";

                //string ketNoi = "Server" + txtServ.Text + ";User Id=" + txtUsername.Text + ";Password=" + txtPass.Text + ";";
                SqlConnection KN = new SqlConnection(ketNoi);
                KN.Open();
                string sql = "select name from sys.databases WHERE name NOT IN ('master','tempdb','model','msdb')";
                SqlCommand cmd = new SqlCommand(sql, KN);
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbBoxData.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

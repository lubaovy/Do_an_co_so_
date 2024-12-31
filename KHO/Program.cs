using QLKHO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHO
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (File.Exists("connectdb.dba"))
            {
                string conStr = "";
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open("connectdb.dba", FileMode.Open, FileAccess.Read);
                connect cp = (connect)bf.Deserialize(fs);
                string servername = Encryptor.Decrypt(cp.servername, "qweryuiop", true);
                string username = Encryptor.Decrypt(cp.username, "qweryuiop", true);
                string pass = Encryptor.Decrypt(cp.password, "qweryuiop", true);
                string database = Encryptor.Decrypt(cp.database, "qweryuiop", true);
                conStr += "Data Source=" + servername + "; Initial Catalog=" + database + "; User ID=" + username + "; Password=" + pass + ";";
                ketnoi = conStr;
                //myFunctions._srv = servername;
                //myFunctions._us = username;
                //myFunctions._pw = pass;
                //myFunctions._db = database;
                SqlConnection kNoi = new SqlConnection(conStr);

                try
                {
                    kNoi.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể kết nối CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fs.Close();
                }
                kNoi.Close();
                fs.Close();
                // Hiển thị form đăng nhập
                var loginForm = new FrmDangNhap();
                if (loginForm.ShowDialog() == DialogResult.OK) // Nếu đăng nhập thành công
                {
                    // Truyền thông tin người dùng từ form đăng nhập vào form chính
                    Application.Run(new MainForm(loginForm.CurrentUser));
                }
                else
                {
                    // Thoát ứng dụng nếu người dùng không đăng nhập
                    Application.Exit();
                }
            }
            else
            {
                Application.Run(new frmConnect());
            }
        }
        public static string ketnoi = "";
    }
}

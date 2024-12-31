using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO
{
    public class UserRepository : IDisposable
    {
        private Entities db;

        public UserRepository()
        {
            db = Entities.CreateEntities(); // Đảm bảo `Entities` là DbContext của bạn
        }

        // Lấy tất cả người dùng
        public List<UserDto> GetAllUsers()
        {
            using (var db = Entities.CreateEntities())
            {
                return db.Users
                .Select(user => new UserDto { Id = user.Id, Tên = user.Tên, MatKhau = user.MatKhau, Role = user.Role  })
                .ToList();
            }
        }
        public User AuthenticateUser(string username, string password)
        {
            // Tìm tài khoản phù hợp
            var user = db.Users.FirstOrDefault(u => u.Tên == username && u.MatKhau == password);
            return user; // Trả về null nếu không tìm thấy
        }
        public void AddUser(UserDto user)
        {
            db.Users.Add(new User
            {
                Tên = user.Tên,
                MatKhau = user.MatKhau,
                Role = user.Role
            });
            db.SaveChanges();
        }

        public void UpdateUser(UserDto user)
        {
            var existinguser = db.Users.Find(user.Id);
            if (existinguser != null)
            {
                existinguser.Tên = user.Tên;
                existinguser.MatKhau = user.MatKhau;
                existinguser.Role = user.Role;
                db.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
        public void Dispose()
        {
            db?.Dispose();
        }
    }
}

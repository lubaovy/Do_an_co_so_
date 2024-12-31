using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Tên { get; set; }
        public string MatKhau { get; set; }
        public string Role { get; set; }
    }
}

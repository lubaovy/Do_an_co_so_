using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO.DTO
{
    public class PhieuNhapDto
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> NgayNhap { get; set; }
        public Nullable<int> IdNCC { get; set; }
        public Nullable<int> IdUser { get; set; }
        public string TenNCC { get; set; }
        public string TenUser { get; set; }
        public int IdDVT { get; set; }
        public string TenDVT { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO.DTO
{
    public class PhieuXuatDto
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> NgayXuat { get; set; }
        public Nullable<int> IdKhachHang { get; set; }
        public Nullable<int> IdUser { get; set; }
        public string GhiChu { get; set; }
        public string TenKH {  get; set; }
        public string TenUser { get; set; }
    }
}

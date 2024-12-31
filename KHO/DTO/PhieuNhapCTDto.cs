using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO.DTO
{
    public class PhieuNhapCTDto
    {
        public int Id { get; set; }
        public int IdHangHoa { get; set; }
        public int IdPhieuNhap { get; set; }
        public int SoLuong { get; set; }
        public Nullable<decimal> GiaNhap { get; set; }
        public Nullable<decimal> ThanhTien { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string BarCode { get; set; }
        public string TenHH { get; set; }
        public Nullable<System.DateTime> NgayNhap { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public string TenNCC { get; set; }
        public string TenUser { get; set; }
        public int IdDVT { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO.DTO
{
    public class PhieuXuatCTDto
    {
        public int Id { get; set; }
        public int IdHangHoa { get; set; }
        public Nullable<decimal> GiaXuat { get; set; }
        public int SoLuong { get; set; }
        public string TrangThai { get; set; }
        public int IdPhieuXuat { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public string BarCode { get; set; }
        public string TenHH { get; set; }
        public string TenKH { get; set; }
        public string TenUser { get; set; }
        public int IdDVT { get; set; }
        public Nullable<System.DateTime> NgayXuat { get; set; }
        public string GhiChu { get; set; }
    }
}

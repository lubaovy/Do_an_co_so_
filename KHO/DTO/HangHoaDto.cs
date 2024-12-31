using QLKHO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO.DTO
{
    public class HangHoaDto
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int IdDVT { get; set; }
        public int IdNCC { get; set; }
        //public string QrCode { get; set; }
        public string BarCode { get; set; }
        public Nullable<int> SoLuongTonKho { get; set; }
        public string MoTa { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public string TenNCC { get; set; }    
        public string TenDVT { get; set; }
        // Giá định dạng thành chuỗi
        public string GiaFormatted
        {
            get { return Gia.HasValue ? Gia.Value.ToString("N0") : "0"; }
        }
    }
}

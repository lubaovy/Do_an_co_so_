using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KHO
{
    public class HangHoaRepository : IDisposable
    {
        private Entities db;

        public HangHoaRepository()
        {
            db = Entities.CreateEntities();
        }
        // Thêm mới hàng hóa
        public void AddHangHoa(HangHoaDto hangHoaDto)
        {
            var hangHoa = new HangHoa
            {
                Ten = hangHoaDto.Ten,
                //BarCode = hangHoaDto.BarCode,
                Gia = hangHoaDto.Gia,
                MoTa = hangHoaDto.MoTa,
                IdDVT = hangHoaDto.IdDVT,
                IdNCC = hangHoaDto.IdNCC,
                SoLuongTonKho = hangHoaDto.SoLuongTonKho,

                // Tạo mã BarCode tự động
                BarCode = Guid.NewGuid().ToString("N").Substring(0, 10) // Ví dụ lấy 10 ký tự từ Guid
            };
            db.HangHoas.Add(hangHoa);
            db.SaveChanges();
        }

        // Cập nhật hàng hóa
        public void UpdateHangHoa(HangHoaDto hangHoaDto)
        {
            var hangHoa = db.HangHoas.Find(hangHoaDto.Id);
            if (hangHoa != null)
            {
                hangHoa.Ten = hangHoaDto.Ten;
                hangHoa.BarCode = hangHoaDto.BarCode;
                hangHoa.Gia = hangHoaDto.Gia;
                hangHoa.MoTa = hangHoaDto.MoTa;
                hangHoa.IdDVT = hangHoaDto.IdDVT;
                hangHoa.IdNCC = hangHoaDto.IdNCC;
                hangHoa.SoLuongTonKho = hangHoaDto.SoLuongTonKho;
                db.SaveChanges();
            }
        }

        // Xóa hàng hóa
        public void DeleteHangHoa(int id)
        {
            var hangHoa = db.HangHoas.Find(id);
            if (hangHoa != null)
            {
                db.HangHoas.Remove(hangHoa);
                db.SaveChanges();
            }
        }

        // Lấy danh sách hàng hóa để hiển thị
        public List<HangHoaDto> GetHangHoas()
        {
            using (var db = Entities.CreateEntities())
            {
                var hangHoas = from hh in db.HangHoas
                               join ncc in db.NCCs on hh.IdNCC equals ncc.Id
                               join dvt in db.DVTs on hh.IdDVT equals dvt.Id
                               select new HangHoaDto
                               {
                                   Id = hh.Id,
                                   Ten = hh.Ten,
                                   TenNCC = ncc.Ten,
                                   TenDVT = dvt.Ten,
                                   BarCode = hh.BarCode,
                                   MoTa = hh.MoTa,
                                   SoLuongTonKho = hh.SoLuongTonKho,
                                   Gia = hh.Gia,
                                   IdDVT = hh.IdDVT,
                                   IdNCC = hh.IdNCC
                               };
                return hangHoas.ToList();
            }
        }

        // Tìm kiếm hàng hóa theo từ khóa
        public List<HangHoaDto> SearchHangHoa(string keyword)
        {
            return db.HangHoas
                .Where(h => h.Ten.Contains(keyword) || h.BarCode.Contains(keyword))
                .Select(h => new HangHoaDto
                {
                    Id = h.Id,
                    Ten = h.Ten,
                    BarCode = h.BarCode,
                    Gia = h.Gia,
                    MoTa = h.MoTa,
                    IdDVT = h.IdDVT,
                    IdNCC = h.IdNCC,
                    SoLuongTonKho = h.SoLuongTonKho
                }).ToList();
        }
        public HangHoaDto GetHangHoaById(int id)
        {
            var hangHoa = db.HangHoas.Find(id);
            if (hangHoa != null)
            {
                return new HangHoaDto
                {
                    Id = hangHoa.Id,
                    Ten = hangHoa.Ten,
                    BarCode = hangHoa.BarCode,
                    Gia = hangHoa.Gia,
                    MoTa = hangHoa.MoTa,
                    IdDVT = hangHoa.IdDVT,
                    IdNCC = hangHoa.IdNCC,
                    SoLuongTonKho = hangHoa.SoLuongTonKho
                };
            }
            return null;
        }
        public void Dispose()
        {
            db?.Dispose();
        }
    }
}

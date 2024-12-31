using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace KHO
{
    public class PhieuXuatCTRepository : IDisposable
    {
        private Entities db;

        public PhieuXuatCTRepository()
        {
            db = Entities.CreateEntities();
        }
        public List<PhieuXuatCTDto> GetPhieuXuatCT()
        {
            using (var db = Entities.CreateEntities())
            {
                var result = (from ct in db.ChiTietPhieuXuats
                              join px in db.PhieuXuats on ct.IdPhieuXuat equals px.Id
                              join user in db.Users on px.IdUser equals user.Id
                              join kh in db.KhachHangs on px.IdKhachHang equals kh.Id
                              //where pn.NgayNhap >= startDate && pn.NgayNhap <= endDate
                              group ct by new { ct.IdPhieuXuat, px.NgayXuat, user.Tên, kh.Ten, px.GhiChu } into g
                              select new PhieuXuatCTDto
                              {
                                  IdPhieuXuat = g.Key.IdPhieuXuat,
                                  NgayXuat = g.Key.NgayXuat,
                                  SoLuong = g.Sum(x => x.SoLuong),
                                  TongTien = g.Sum(x => x.TongTien ?? 0), // Tổng tiền từ cột ChiTietPhieuNhaps
                                  GhiChu = g.Key.GhiChu,
                                  TrangThai = g.Select(x => x.TrangThai).FirstOrDefault(), // Lấy TrangThai đầu tiên
                                  TenUser = g.Key.Tên,
                                  TenKH = g.Key.Ten,
                              }).ToList();

                return result;
            }
        }
        public void AddPhieuXuatCT(PhieuXuatCTDto chiTiet)
        {
            var chiTietPhieuXuat = new ChiTietPhieuXuat
            {
                IdPhieuXuat = chiTiet.IdPhieuXuat,
                IdHangHoa = chiTiet.IdHangHoa,
                SoLuong = chiTiet.SoLuong,
                TongTien = chiTiet.TongTien,
                TrangThai = chiTiet.TrangThai,
                GiaXuat = chiTiet.GiaXuat
            };

            // Thêm chi tiết vào cơ sở dữ liệu và lưu thay đổi
            db.ChiTietPhieuXuats.Add(chiTietPhieuXuat);
            db.SaveChanges();
        }

        // Xóa phiếu nhập
        public void DeletePhieuXuatCT(int id)
        {
            var phieuXuatCT = db.ChiTietPhieuXuats.Find(id);
            if (phieuXuatCT != null)
            {
                db.ChiTietPhieuXuats.Remove(phieuXuatCT);
                db.SaveChanges();
            }
        }
        public List<PhieuXuatCTDto> GetChiTietPhieuXuatById(int idPhieuXuat)
        {
            using (var db = Entities.CreateEntities())
            {
                return (from ct in db.ChiTietPhieuXuats
                        join hh in db.HangHoas on ct.IdHangHoa equals hh.Id
                        where ct.IdPhieuXuat == idPhieuXuat
                        select new PhieuXuatCTDto
                        {
                            Id = ct.Id,
                            IdPhieuXuat = ct.IdPhieuXuat,
                            IdHangHoa = ct.IdHangHoa,
                            BarCode = hh.BarCode,
                            TenHH = hh.Ten,
                            SoLuong = ct.SoLuong,
                            GiaXuat = ct.GiaXuat,
                            TongTien = ct.TongTien,
                            TrangThai = ct.TrangThai,
                            IdDVT = hh.IdDVT
                        }).ToList();
            }
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}


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
    public class PhieuNhapCTRepository : IDisposable
    {
        private Entities db;

        public PhieuNhapCTRepository()
        {
            db = Entities.CreateEntities();
        }
        // Lấy danh sách phiếu nhập
        public List<PhieuNhapCTDto> GetPhieuNhapCT()
        {
            using (var db = Entities.CreateEntities())
            {
                var result = (from ct in db.ChiTietPhieuNhaps
                              join pn in db.PhieuNhaps on ct.IdPhieuNhap equals pn.Id
                              join user in db.Users on pn.IdUser equals user.Id
                              join ncc in db.NCCs on pn.IdNCC equals ncc.Id
                              //where pn.NgayNhap >= startDate && pn.NgayNhap <= endDate
                              group ct by new { ct.IdPhieuNhap, pn.NgayNhap, user.Tên, ncc.Ten } into g
                              select new PhieuNhapCTDto
                              {
                                  IdPhieuNhap = g.Key.IdPhieuNhap,
                                  NgayNhap = g.Key.NgayNhap,
                                  SoLuong = g.Sum(x => x.SoLuong),
                                  TongTien = g.Sum(x => x.ThanhTien ?? 0), // Tổng tiền từ cột ChiTietPhieuNhaps
                                  GhiChu = g.Select(x => x.GhiChu).FirstOrDefault(), // Lấy GhiChu đầu tiên
                                  TrangThai = g.Select(x => x.TrangThai).FirstOrDefault(), // Lấy TrangThai đầu tiên
                                  TenUser = g.Key.Tên,
                                  TenNCC = g.Key.Ten,
                              }).ToList();

                return result;
            }
        }
        // Thêm phiếu nhập
        public void AddPhieuNhapCT(PhieuNhapCTDto chiTiet)
        {
            var chiTietPhieuNhap = new ChiTietPhieuNhap
            {
                IdPhieuNhap = chiTiet.IdPhieuNhap,
                IdHangHoa = chiTiet.IdHangHoa,
                SoLuong = chiTiet.SoLuong,
                ThanhTien = chiTiet.ThanhTien,
                TrangThai = chiTiet.TrangThai,
                GhiChu = chiTiet.GhiChu,
                GiaNhap = chiTiet.GiaNhap
            };

            // Thêm chi tiết vào cơ sở dữ liệu và lưu thay đổi
            db.ChiTietPhieuNhaps.Add(chiTietPhieuNhap);
            db.SaveChanges();
        }

        // Xóa phiếu nhập
        public void DeletePhieuNhapCT(int id)
        {
            var phieuNhapCT = db.ChiTietPhieuNhaps.Find(id);
            if (phieuNhapCT != null)
            {
                db.ChiTietPhieuNhaps.Remove(phieuNhapCT);
                db.SaveChanges();
            }
        }
        public List<PhieuNhapCTDto> GetChiTietPhieuNhapById(int idPhieuNhap)
        {
            using (var db = Entities.CreateEntities())
            {
                return (from ct in db.ChiTietPhieuNhaps
                        join hh in db.HangHoas on ct.IdHangHoa equals hh.Id
                        where ct.IdPhieuNhap == idPhieuNhap
                        select new PhieuNhapCTDto
                        {
                            Id = ct.Id,
                            IdPhieuNhap = ct.IdPhieuNhap,
                            IdHangHoa = ct.IdHangHoa,
                            BarCode = hh.BarCode,
                            TenHH = hh.Ten,
                            SoLuong = ct.SoLuong,
                            GiaNhap = ct.GiaNhap,
                            ThanhTien = ct.ThanhTien,
                            TrangThai = ct.TrangThai,
                            GhiChu = ct.GhiChu,
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


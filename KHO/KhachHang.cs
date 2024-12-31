using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO
{
    public class KHRepository : IDisposable
    {
        private Entities db;
        public KHRepository()
        {
            db = Entities.CreateEntities();
        }
        public List<KhachHangDto> GetKhachHangs()
        {
            using (var db = Entities.CreateEntities())
            {
                return db.KhachHangs
                .Select(kh => new KhachHangDto { Id = kh.Id, Ten = kh.Ten, DiaChi = kh.DiaChi, Email = kh.Email, Phone = kh.Phone, MoreInfo = kh.MoreInfo })
                .ToList();
            }
        }
        public void AddKH(KhachHangDto kh)
        {
            db.KhachHangs.Add(new KhachHang
            {
                Ten = kh.Ten,
                DiaChi = kh.DiaChi,
                Phone = kh.Phone,
                Email = kh.Email,
                MoreInfo = kh.MoreInfo
            });
            db.SaveChanges();
        }

        public void UpdateKH(KhachHangDto kh)
        {
            var existingkh = db.KhachHangs.Find(kh.Id);
            if (existingkh != null)
            {
                existingkh.Ten = kh.Ten;
                existingkh.DiaChi = kh.DiaChi;
                existingkh.Phone = kh.Phone;
                existingkh.Email = kh.Email;
                existingkh.MoreInfo = kh.MoreInfo;
                db.SaveChanges();
            }
        }

        public void DeleteKH(int id)
        {
            var kh = db.KhachHangs.Find(id);
            if (kh != null)
            {
                db.KhachHangs.Remove(kh);
                db.SaveChanges();
            }
        }
        public void Dispose()
        {
            db?.Dispose();
        }
    }
}

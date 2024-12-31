using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO
{
    public class PhieuNhapRepository : IDisposable
    {
        private Entities db;

        public PhieuNhapRepository()
        {
            db = Entities.CreateEntities();
        }
        // Lấy danh sách phiếu nhập
        public List<PhieuNhapDto> GetPhieuNhap()
        {
            using (var db = Entities.CreateEntities())
            {
                var phieuNhaps = from pn in db.PhieuNhaps
                               join ncc in db.NCCs on pn.IdNCC equals ncc.Id
                               join user in db.Users on pn.IdUser equals user.Id
                               select new PhieuNhapDto
                               {
                                   Id = pn.Id,
                                   IdUser = pn.IdUser,
                                   TenNCC = ncc.Ten,
                                   IdNCC = pn.IdNCC,
                                   TenUser = user.Tên,
                                   NgayNhap = pn.NgayNhap
                               };
                return phieuNhaps.ToList();
            }
        }
        // Thêm phiếu nhập
        public void AddPhieuNhap(PhieuNhapDto phieuNhapDto)
        {
            var phieuNhap = new PhieuNhap
            {
                NgayNhap = phieuNhapDto.NgayNhap ?? DateTime.Now, // Nếu NgayNhap null thì lấy ngày hiện tại
                IdNCC = phieuNhapDto.IdNCC,
                IdUser = phieuNhapDto.IdUser,
            };
            db.PhieuNhaps.Add(phieuNhap);
            db.SaveChanges();
        }

        // Sửa phiếu nhập
        public void UpdatePhieuNhap(PhieuNhapDto phieuNhapDto)
        {
            var phieuNhap = db.PhieuNhaps.Find(phieuNhapDto.Id);

            if (phieuNhap != null)
            {
                // Cập nhật thông tin phiếu nhập từ DTO
                phieuNhap.NgayNhap = phieuNhapDto.NgayNhap ?? phieuNhap.NgayNhap; // Nếu NgayNhap null thì giữ nguyên giá trị cũ
                phieuNhap.IdNCC = phieuNhapDto.IdNCC ?? phieuNhap.IdNCC;
                phieuNhap.IdUser = phieuNhapDto.IdUser ?? phieuNhap.IdUser;

                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        // Xóa phiếu nhập
        public void DeletePhieuNhap(int id)
        {
            var phieuNhap = db.PhieuNhaps.Find(id);
            if (phieuNhap != null)
            {
                db.PhieuNhaps.Remove(phieuNhap);
                db.SaveChanges();
            }
        }
        public PhieuNhapDto GetPhieuNhapById(int id)
        {
            var phieuNhap = db.PhieuNhaps.Find(id);
            if (phieuNhap != null)
            {
                return new PhieuNhapDto
                {
                    Id = phieuNhap.Id,
                    NgayNhap = phieuNhap.NgayNhap,
                    IdNCC = phieuNhap.IdNCC,
                    IdUser = phieuNhap.IdUser,
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

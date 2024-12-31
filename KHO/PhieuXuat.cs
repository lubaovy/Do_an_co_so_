using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO
{
    public class PhieuXuatRepository : IDisposable
    {
        private Entities db;

        public PhieuXuatRepository()
        {
            db = Entities.CreateEntities();
        }
        public List<PhieuXuatDto> GetPhieuXuat()
        {
            using (var db = Entities.CreateEntities())
            {
                var phieuXuats = from px in db.PhieuXuats
                                 join kh in db.KhachHangs on px.IdKhachHang equals kh.Id
                                 join user in db.Users on px.IdUser equals user.Id
                                 select new PhieuXuatDto
                                 {
                                     Id = px.Id,
                                     IdUser = px.IdUser,
                                     IdKhachHang = px.IdKhachHang,
                                     NgayXuat = px.NgayXuat,
                                     TenKH = kh.Ten,
                                     TenUser = user.Tên,
                                     GhiChu = px.GhiChu
                                 };
                return phieuXuats.ToList();
            }
        }
        public void AddPhieuXuat(PhieuXuatDto phieuXuatDto)
        {
            var phieuXuat = new PhieuXuat
            {
                NgayXuat = phieuXuatDto.NgayXuat ?? DateTime.Now, // Nếu NgayNhap null thì lấy ngày hiện tại
                IdKhachHang = phieuXuatDto.IdKhachHang,
                IdUser = phieuXuatDto.IdUser,
                GhiChu = phieuXuatDto.GhiChu
            };
            db.PhieuXuats.Add(phieuXuat);
            db.SaveChanges();
        }
        public void UpdatePhieuXuat(PhieuXuatDto phieuXuatDto)
        {
            var phieuXuat = db.PhieuXuats.Find(phieuXuatDto.Id);

            if (phieuXuat != null)
            {
                // Cập nhật thông tin phiếu nhập từ DTO
                phieuXuat.NgayXuat = phieuXuatDto.NgayXuat ?? phieuXuat.NgayXuat; // Nếu NgayNhap null thì giữ nguyên giá trị cũ
                phieuXuat.IdKhachHang = phieuXuatDto.IdKhachHang ?? phieuXuat.IdKhachHang;
                phieuXuat.IdUser = phieuXuatDto.IdUser ?? phieuXuat.IdUser;
                phieuXuat.GhiChu = phieuXuatDto.GhiChu ?? phieuXuat.GhiChu;

                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }
        public void DeletePhieuXuat(int id)
        {
            var phieuXuat = db.PhieuXuats.Find(id);
            if (phieuXuat != null)
            {
                db.PhieuXuats.Remove(phieuXuat);
                db.SaveChanges();
            }
        }
        public PhieuXuatDto GetPhieuXuatById(int id)
        {
            var phieuXuat = db.PhieuXuats.Find(id);
            if (phieuXuat != null)
            {
                return new PhieuXuatDto
                {
                    Id = phieuXuat.Id,
                    NgayXuat = phieuXuat.NgayXuat,
                    IdKhachHang = phieuXuat.IdKhachHang,
                    IdUser = phieuXuat.IdUser,
                    GhiChu = phieuXuat?.GhiChu
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

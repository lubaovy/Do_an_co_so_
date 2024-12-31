using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO
{
    public class NCCRepository : IDisposable
    {
        private Entities db;
        public NCCRepository()
        {
            db = Entities.CreateEntities();
        }
        public List<NCCDto> GetNCCs()
        {
            using (var db = Entities.CreateEntities())
            {
                return db.NCCs
                .Select(ncc => new NCCDto { Id = ncc.Id, Ten = ncc.Ten, DiaChi = ncc.DiaChi, Email = ncc.Email, Phone = ncc.Phone, MoreInfo = ncc.MoreInfo })
                .ToList();
            }
        }
        public void AddNCC(NCCDto ncc)
        {
            db.NCCs.Add(new NCC
            {
                Ten = ncc.Ten,
                DiaChi = ncc.DiaChi,
                Phone = ncc.Phone,
                Email = ncc.Email,
                MoreInfo = ncc.MoreInfo
            });
            db.SaveChanges();
        }

        public void UpdateNCC(NCCDto ncc)
        {
            var existingNCC = db.NCCs.Find(ncc.Id);
            if (existingNCC != null)
            {
                existingNCC.Ten = ncc.Ten;
                existingNCC.DiaChi = ncc.DiaChi;
                existingNCC.Phone = ncc.Phone;
                existingNCC.Email = ncc.Email;
                existingNCC.MoreInfo = ncc.MoreInfo;
                db.SaveChanges();
            }
        }

        public void DeleteNCC(int id)
        {
            var ncc = db.NCCs.Find(id);
            if (ncc != null)
            {
                db.NCCs.Remove(ncc);
                db.SaveChanges();
            }
        }
        public void Dispose()
        {
            db?.Dispose();
        }
    }
}

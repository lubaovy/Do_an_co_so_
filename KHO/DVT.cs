using KHO.DTO;
using QLKHO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHO
{
    public class DVTRepository : IDisposable
    {
        private Entities db;
        public DVTRepository()
        {
            db = Entities.CreateEntities();
        }
        public List<DVTDto> GetDVTs()
        {
            using (var db = Entities.CreateEntities())
            {
                return db.DVTs
                .Select(dvt => new DVTDto {Id = dvt.Id, Ten = dvt.Ten }) // Chỉ chọn Id và Tên
                .ToList();
            }
        }
        public void AddDonViTinh(DVT dvt)
        {
            db.DVTs.Add(dvt);
            db.SaveChanges();
        }
        public void UpdateDonViTinh(DVT dvt)
        {
            var existingDvt = db.DVTs.Find(dvt.Id);
            if (existingDvt != null)
            {
                existingDvt.Ten = dvt.Ten;
                db.SaveChanges();
            }
        }
        // Phương thức tìm DVT theo Id
        public DVT GetDVTById(int id)
        {
            return db.DVTs.FirstOrDefault(dvt => dvt.Id == id);
        }
        public Dictionary<int, string> GetDvtDictionary()
        {
            return db.DVTs.ToDictionary(d => d.Id, d => d.Ten);
        }
        public void Dispose()
        {
            db?.Dispose();
        }
    }
}

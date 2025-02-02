using Microsoft.EntityFrameworkCore;
using MusicStoreData.Data;
using MusicStoreData.Models.CMS;
using MusicStore.Repository.IRepository;
using NPOI.SS.Formula.Functions;
 
namespace MusicStore.Repository
{
    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public ManufacturerRepository(MusicStoreContext context) : base(context)
        {
            _context = context;
        }
        public bool IsPositionInUse(Manufacturer product)
        {
            return _context.Manufacturer.Any(m => m.Position == product.Position && m.Id != product.Id);

        } 
        public Manufacturer GetWithManufacturer(int id)
        {
            return _context.Manufacturer.FirstOrDefault(p => p.Id == id);
        }
        public bool ManufacturerExists(int id)
        {
            return _context.Manufacturer.Any(e => e.Id == id);
        }
        public void Update(Manufacturer obj)
        {
            var objFromDb = _context.Manufacturer.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Descriptions = obj.Descriptions;
                objFromDb.Position = obj.Position;
                objFromDb.ManufacturerWeb = obj.ManufacturerWeb;
                objFromDb.ManufacturerEmail = obj.ManufacturerEmail;
                objFromDb.ManufactureAddress = obj.ManufactureAddress;
                objFromDb.ContactNumber = obj.ContactNumber;
                objFromDb.CountryOfOrigin = obj.CountryOfOrigin;
                if (obj.LogoUrl != null)
                {
                    objFromDb.LogoUrl = obj.LogoUrl;
                }
            }
        }
    }
}

   


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class SupplierDealRepository : BaseRepository
    {
        public delegate void SupplierDealHandler(object sender, SupplierDealEventArgs e);
        public event SupplierDealHandler? SupplierDealChanged;
        public Entity.Supplierdeal? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Suppliersdeals.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Supplierdeal>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Suppliersdeals.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Supplierdeal? Create(Entity.Supplierdeal? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Suppliersdeals.Add(Source);
            ApiContext.SaveChanges();
            SupplierDealChanged?.Invoke(this, new SupplierDealEventArgs(Source));
            return Source;
        }
        public Entity.Supplierdeal? Update(Entity.Supplierdeal? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.SupplierId = Source.SupplierId;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            SupplierDealChanged?.Invoke(this, new SupplierDealEventArgs(record));
            return record;
        }
        public Entity.Supplierdeal? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            SupplierDealChanged?.Invoke(this, new SupplierDealEventArgs(record));
            return record;
        }
    }
    public class SupplierDealEventArgs : EventArgs
    {
        public Entity.Supplierdeal Supplierdeal {  get; set; }
        public SupplierDealEventArgs(Entity.Supplierdeal supplierdeal)
        {
            Supplierdeal = supplierdeal;
        }
    }
}

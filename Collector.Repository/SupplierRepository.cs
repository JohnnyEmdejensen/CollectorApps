using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class SupplierRepository : BaseRepository
    {
        public delegate void SupplierHandler(object sender, SupplierEventArgs e);
        public event SupplierHandler? SupplierChanged;
        public Entity.Supplier? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Suppliers.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Supplier>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Suppliers.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Supplier? Create(Entity.Supplier? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Suppliers.Add(Source);
            ApiContext.SaveChanges();
            SupplierChanged?.Invoke(this, new SupplierEventArgs(Source));
            return Source;
        }
        public Entity.Supplier? Update(Entity.Supplier? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            SupplierChanged?.Invoke(this, new SupplierEventArgs(record));
            return record;
        }
        public Entity.Supplier? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            SupplierChanged?.Invoke(this, new SupplierEventArgs(record));
            return record;
        }
    }
    public class SupplierEventArgs : EventArgs
    {
        public Entity.Supplier Supplier { get; set; }
        public SupplierEventArgs(Entity.Supplier supplier)
        {
            Supplier = supplier;    
        }
    }
}

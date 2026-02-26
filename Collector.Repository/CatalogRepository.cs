using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class CatalogRepository : BaseRepository
    {
        public delegate void CatalogHandler(object sender, CatalogEventArgs e);
        public event CatalogHandler? CatalogChanged;

        public Entity.Catalog? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Catalogs.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Catalog>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Catalogs.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Catalog? Create(Entity.Catalog? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Catalogs.Add(Source);
            ApiContext.SaveChanges();
            CatalogChanged?.Invoke(this, new CatalogEventArgs(Source));
            return Source;
        }
        public Entity.Catalog? Update(Entity.Catalog? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            CatalogChanged?.Invoke(this, new CatalogEventArgs(record));
            return record;
        }
        public Entity.Catalog? Delete(long Id)
        {
            if (ApiContext == null) return null;
            var record = Get(Id);
            if (record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            CatalogChanged?.Invoke(this, new CatalogEventArgs(record));
            return record;
        }
    }
    public class CatalogEventArgs : EventArgs
    {
        public Entity.Catalog Catalog { get; set; }
        public CatalogEventArgs(Entity.Catalog catalog)
        {
            Catalog = catalog;
        }
    }
}

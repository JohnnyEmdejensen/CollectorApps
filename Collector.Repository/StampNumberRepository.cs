using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class StampNumberRepository : BaseRepository
    {
        public Entity.StampNumber? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Stampnumbers.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.StampNumber>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Stampnumbers.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.StampNumber? Create(Entity.StampNumber? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Stampnumbers.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.StampNumber? Update(Entity.StampNumber? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.CatalogId = Source.CatalogId;
            record.Number = Source.Number;
            record.Prefix = Source.Prefix;
            record.StampId = Source.StampId;
            record.Suffix = Source.Suffix;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.StampNumber? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}

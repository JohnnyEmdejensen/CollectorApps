using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class ContinentRepository : BaseRepository
    {
        public delegate void ContinentHandler(object sender, ContinentEventArgs e);
        public event ContinentHandler? ContinentChanged;

        public Entity.Continent? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Continents.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Continent>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Continents.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Continent? Create(Entity.Continent? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Continents.Add(Source);
            ApiContext.SaveChanges();
            ContinentChanged?.Invoke(this, new ContinentEventArgs(Source));
            return Source;
        }
        public Entity.Continent? Update(Entity.Continent? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;    
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            ContinentChanged?.Invoke(this, new ContinentEventArgs(record));
            return record;
        }
        public Entity.Continent? Delete(long Id)
        {
            if (ApiContext == null) return null;
            var record = Get(Id);
            if (record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            ContinentChanged?.Invoke(this, new ContinentEventArgs(record));
            return record;
        }
    }
    public class ContinentEventArgs : EventArgs
    {
        private Entity.Continent _continent;
        public ContinentEventArgs(Entity.Continent Continent)
        {
            _continent = Continent;
        }
    }
}

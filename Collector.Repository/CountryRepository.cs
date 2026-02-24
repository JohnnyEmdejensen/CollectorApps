using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class CountryRepository : BaseRepository
    {

        public delegate void COuntryHandler(object sender, CountryEventArgs e);
        public event COuntryHandler? CountryChanged;

        public Entity.Country? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Countries.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Country>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Countries.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Country? Create(Entity.Country? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Countries.Add(Source);
            ApiContext.SaveChanges();
            CountryChanged?.Invoke(this, new CountryEventArgs(Source));
            return Source;
        }
        public Entity.Country? Update(Entity.Country? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Name = Source.Name;
            record.Iso = Source.Iso;
            record.Number = Source.Number;
            record.ContinentId = Source.ContinentId;
            ApiContext.SaveChanges();
            CountryChanged?.Invoke(this, new CountryEventArgs(record));
            return record;
        }

        public Entity.Country? Delete(long Id)
        {
            if (ApiContext == null) return null;
            var record = Get(Id);
            if (record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            CountryChanged?.Invoke(this, new CountryEventArgs(record));
            return record;
        }
    }

    public class CountryEventArgs
    {
        public Entity.Country Country { get; set; }

        public CountryEventArgs(Entity.Country country)
        {
            Country = country;
        }
    }
}

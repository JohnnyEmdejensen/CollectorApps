using Collector.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class PublishedcountryRepository : BaseRepository
    {
        public delegate void PublishedCountryHandler(object sender, PublishedCountryEventArgs e);
        public event PublishedCountryHandler? PublishedCountryChanged;

        public Entity.PublishedCountry? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.PublishedCountries.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.PublishedCountry>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.PublishedCountries.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.PublishedCountry? Create(Entity.PublishedCountry? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.PublishedCountries.Add(Source);
            ApiContext.SaveChanges();
            PublishedCountryChanged?.Invoke(this, new PublishedCountryEventArgs(Source));
            return Source;
        }
        public Entity.PublishedCountry? Update(Entity.PublishedCountry? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Name = Source.Name;
            record.ContryId = Source.ContryId;
            ApiContext.SaveChanges();
            PublishedCountryChanged?.Invoke(this, new PublishedCountryEventArgs(record));
            return record;
        }
        public Entity.PublishedCountry? Delete(long Id)
        {
            if (ApiContext == null) return null;
            var record = Get(Id);
            if (record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            PublishedCountryChanged?.Invoke(this, new PublishedCountryEventArgs(record));
            return record;
        }
    }
    public class PublishedCountryEventArgs : EventArgs
    {
        public Entity.PublishedCountry Publishedcountry { get; set; }
        public PublishedCountryEventArgs(Entity.PublishedCountry publishedcountry)
        {
            Publishedcountry = publishedcountry;
        }
    }
}

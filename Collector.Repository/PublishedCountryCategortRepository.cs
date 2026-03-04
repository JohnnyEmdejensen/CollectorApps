using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class PublishedCountryCategortRepository : BaseRepository
    {
        public delegate void PublishedCountryCategortHandler(object sender, PublishedCountryCategoryEventArgs e);
        public event PublishedCountryCategortHandler? PublishedCountryCategoryChanged;
        public Entity.PublishedCountryCategoryLink? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.PublishedCountryCategoryLinks.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.PublishedCountryCategoryLink>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.PublishedCountryCategoryLinks.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.PublishedCountryCategoryLink? Create(Entity.PublishedCountryCategoryLink? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.PublishedCountryCategoryLinks.Add(Source);
            ApiContext.SaveChanges();
            PublishedCountryCategoryChanged?.Invoke(this, new PublishedCountryCategoryEventArgs(Source));
            return Source;
        }
        public Entity.PublishedCountryCategoryLink? Update(Entity.PublishedCountryCategoryLink? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.PublishedCountryId = Source.PublishedCountryId;
            record.CategoryId = Source.CategoryId;
            ApiContext.SaveChanges();
            PublishedCountryCategoryChanged?.Invoke(this, new PublishedCountryCategoryEventArgs(record));
            return record;
        }
        public Entity.PublishedCountryCategoryLink? Delete(long Id)
        {
            if (ApiContext == null) return null;
            var record = Get(Id);
            if (record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            PublishedCountryCategoryChanged?.Invoke(this, new PublishedCountryCategoryEventArgs(record));
            return record;
        }
    }
    public class PublishedCountryCategoryEventArgs
    {
        public Entity.PublishedCountryCategoryLink PublishedCountryCategoryLink { get; set; }
        public PublishedCountryCategoryEventArgs(Entity.PublishedCountryCategoryLink publishedCountryCategoryLink)
        {
            PublishedCountryCategoryLink = publishedCountryCategoryLink;
        }
    }
}

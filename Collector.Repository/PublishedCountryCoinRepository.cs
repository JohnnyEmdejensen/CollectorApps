using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class PublishedCountryCoinRepository : BaseRepository
    {
        public delegate void PublishedCountryCoinHandler(object sender, PublishedCountryCoinLinkEventArgs e);
        public event PublishedCountryCoinHandler? PublishedCountryCoinLinkChanged;
        public Entity.PublishedCountryCoinLink? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.PublishedCountryCoinsLinks.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.PublishedCountryCoinLink>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.PublishedCountryCoinsLinks.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.PublishedCountryCoinLink? Create(Entity.PublishedCountryCoinLink? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.PublishedCountryCoinsLinks.Add(Source);
            ApiContext.SaveChanges();
            PublishedCountryCoinLinkChanged?.Invoke(this, new PublishedCountryCoinLinkEventArgs(Source));
            return Source;
        }
        public Entity.PublishedCountryCoinLink? Update(Entity.PublishedCountryCoinLink? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.PublishedCountryId = Source.PublishedCountryId;
            record.CoinId = Source.CoinId;
            ApiContext.SaveChanges();
            PublishedCountryCoinLinkChanged?.Invoke(this, new PublishedCountryCoinLinkEventArgs(record));
            return record;
        }
        public Entity.PublishedCountryCoinLink? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            PublishedCountryCoinLinkChanged?.Invoke(this, new PublishedCountryCoinLinkEventArgs(record));
            return record;
        }
    }
    public class PublishedCountryCoinLinkEventArgs : EventArgs
    {
        public Entity.PublishedCountryCoinLink PublishedCountryCoinLink { get; set; }
        public PublishedCountryCoinLinkEventArgs(Entity.PublishedCountryCoinLink link)
        {
            PublishedCountryCoinLink = link;
        }
    }
}

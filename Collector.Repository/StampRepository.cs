using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class StampRepository : BaseRepository
    {
        public delegate void StampHandler(object sender, StampEventArgs e);
        public event StampHandler? StampChanged;

        public Entity.Stamp? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Stamps.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Stamp>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Stamps.Where(x => !x.IsDeleted).ToList();
        }
        public List<Entity.Stamp>? GetByPublishedCountry(long PublishedCountryId)
        {
            if (ApiContext == null) return null;
            return ApiContext.Stamps.Where(x => x.PublishedCountryId == PublishedCountryId && !x.IsDeleted).ToList();
        }
        public Entity.Stamp? Create(Entity.Stamp? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Stamps.Add(Source);
            ApiContext.SaveChanges();
            StampChanged?.Invoke(this, new StampEventArgs(Source));
            return Source;
        }
        public Entity.Stamp? Update(Entity.Stamp? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.CatalogId = Source.CatalogId;
            record.CategoryId = Source.CategoryId;
            record.CoinId = Source.CoinId;
            record.ColorId = Source.ColorId;
            record.Description = Source.Description;
            record.Design = Source.Design;
            record.Designer = Source.Designer;
            record.Facevalue = Source.Facevalue;
            record.Height = Source.Height;
            record.Ingraver = Source.Ingraver;
            record.Issued = Source.Issued;
            record.IssuedAmount = Source.IssuedAmount;
            record.Motive = Source.Motive;
            record.Number = Source.Number;
            record.PerforationHorId = Source.PerforationHorId;
            record.PerforationVerId = Source.PerforationVerId;
            record.Prefix = Source.Prefix;
            record.PublishedCountryId = Source.PublishedCountryId;
            record.Suffix = Source.Suffix;
            record.TopicId = Source.TopicId;
            record.WatermarkId = Source.WatermarkId;
            record.Width = Source.Width;
            record.TypeId = Source.TypeId;
            record.ClassId = Source.ClassId;
            ApiContext.SaveChanges();
            StampChanged?.Invoke(this, new StampEventArgs(record));
            return record;
        }
        public Entity.Stamp? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            StampChanged?.Invoke(this, new StampEventArgs(record));
            return record;
        }
    }
    public class StampEventArgs : EventArgs
    {
        public Entity.Stamp Stamp { get; set; }

        public StampEventArgs(Entity.Stamp stamp)
        {
            Stamp = stamp;
        }
    }
}

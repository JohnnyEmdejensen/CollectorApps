using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class StampPriceRepository : BaseRepository
    {
        public delegate void StampPriceHandler(object sender, StampPriceEventArgs e);
        public event StampPriceHandler? OnStampPriceChanged;
        public Entity.StampPrice? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.StampPrices.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.StampPrice>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.StampPrices.Where(x => !x.IsDeleted).ToList();
        }
        public List<Entity.StampPrice>? GetByStamp(long StampId)
        {
            if (ApiContext == null) return null;
            return ApiContext.StampPrices.Where(x => x.StampId == StampId && !x.IsDeleted).ToList();
        }
        public Entity.StampPrice? Create(Entity.StampPrice? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.StampPrices.Add(Source);
            ApiContext.SaveChanges();
            OnStampPriceChanged?.Invoke(this, new StampPriceEventArgs(Source));
            return Source;
        }
        public Entity.StampPrice? Update(Entity.StampPrice? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.StampId = Source.StampId;
            record.ConditionId = Source.ConditionId;
            record.Price = Source.Price;
            ApiContext.SaveChanges();
            OnStampPriceChanged?.Invoke(this, new StampPriceEventArgs(record));
            return record;
        }
        public Entity.StampPrice? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            OnStampPriceChanged?.Invoke(this, new StampPriceEventArgs(record));
            return record;
        }
    }
    public class StampPriceEventArgs : EventArgs
    {
        public Entity.StampPrice StampPrice { get; set; }
        public StampPriceEventArgs(Entity.StampPrice stampPrice)
        {
            StampPrice = stampPrice;
        }   
    }
}

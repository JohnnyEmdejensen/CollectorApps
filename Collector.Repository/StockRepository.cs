using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class StockRepository : BaseRepository
    {
        public delegate void StockHandler(object sender, StockEventArgs e);
        public event StockHandler? StockChanged;
        public bool SupressEvents { get; set; } = false;

        public Entity.Stock? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Stocks.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Stock>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Stocks.Where(x => !x.IsDeleted).ToList();
        }
        public List<Entity.Stock>? GetByStamp(long StampId)
        {
            if (ApiContext == null) return null;
            return ApiContext.Stocks.Where(x => x.StampId == StampId && !x.IsDeleted).ToList();
        }
        public Entity.Stock? Create(Entity.Stock? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Stocks.Add(Source);
            ApiContext.SaveChanges();
            if (!SupressEvents)
                StockChanged?.Invoke(this, new StockEventArgs(Source));
            return Source;
        }
        public Entity.Stock? Update(Entity.Stock? Source)
        {            
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.LocationId = Source.LocationId;
            record.Quantity = Source.Quantity;
            record.StampId = Source.StampId;
            record.SupplierDealId = Source.SupplierDealId;
            record.ConditionId = Source.ConditionId;
            ApiContext.SaveChanges();
            if (!SupressEvents)
                StockChanged?.Invoke(this, new StockEventArgs(record));
            return record;
        }
        public Entity.Stock? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            if (!SupressEvents)
                StockChanged?.Invoke(this, new StockEventArgs(record));
            return record;
        }
    }
    public class StockEventArgs : EventArgs
    {
        public Entity.Stock Stock { get; set; }
        public StockEventArgs(Entity.Stock stock)
        {
            Stock = stock;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class CoinRepository : BaseRepository
    {
        public delegate void CoinHandler(object sender, CoinEventArgs e);
        public event CoinHandler? CoinChanged;

        public Entity.Coin? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Coins.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Coin>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Coins.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Coin? Create(Entity.Coin? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Coins.Add(Source);
            ApiContext.SaveChanges();
            CoinChanged?.Invoke(this, new CoinEventArgs(Source));
            return Source;
        }
        public Entity.Coin? Update(Entity.Coin? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            CoinChanged?.Invoke(this, new CoinEventArgs(record));
            return record;
        }
        public Entity.Coin? Delete(long Id)
        {
            if (ApiContext == null) return null;
            var record = Get(Id);
            if (record == null) { return null; }
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            CoinChanged?.Invoke(this, new CoinEventArgs(record));
            return record;
        }
    }
    public class CoinEventArgs : EventArgs
    {
        public Entity.Coin Coin { get; set; }
        public CoinEventArgs(Entity.Coin coin)
        {
            Coin = coin;
        }
    }
}

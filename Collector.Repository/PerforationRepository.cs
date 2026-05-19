using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class PerforationRepository : BaseRepository
    {
        public delegate void PerforationHandler (object sender, PerforationEventArgs e);
        public event PerforationHandler? PerforationChanged;
        public Entity.Perforation? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Perforations.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Perforation>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Perforations.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Perforation? Create(Entity.Perforation? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Perforations.Add(Source);
            ApiContext.SaveChanges();
            PerforationChanged?.Invoke(this, new PerforationEventArgs(Source));
            return Source;
        }
        public Entity.Perforation? Update(Entity.Perforation? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            PerforationChanged?.Invoke(this, new PerforationEventArgs(record));
            return record;
        }
        public Entity.Perforation? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            PerforationChanged?.Invoke(this, new PerforationEventArgs(record));
            return record;
        }
    }
    public class PerforationEventArgs: EventArgs
    {
        public Entity.Perforation Perforation { get; private set; }
        public PerforationEventArgs(Entity.Perforation perforation)
        {
            Perforation = perforation;
        }
    }
}

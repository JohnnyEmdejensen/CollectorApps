using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class LocationRepository : BaseRepository
    {
        public delegate void LocationHandler(object sender, LocationEventArgs e);
        public event LocationHandler? LocationChanged;

        public Entity.Location? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Locations.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Location>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Locations.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Location? Create(Entity.Location? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Locations.Add(Source);
            ApiContext.SaveChanges();
            LocationChanged?.Invoke(this, new LocationEventArgs(Source));
            return Source;
        }
        public Entity.Location? Update(Entity.Location? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if(record == null) return null;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            LocationChanged?.Invoke(this, new LocationEventArgs(record));
            return record;
        }
        public Entity.Location? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            LocationChanged?.Invoke(this, new LocationEventArgs(record));
            return record;
        }
    }
    public class LocationEventArgs : EventArgs
    {
        public Entity.Location Location { get; set; }
        public LocationEventArgs(Entity.Location location)
        {
            Location = location;
        }
    }
}

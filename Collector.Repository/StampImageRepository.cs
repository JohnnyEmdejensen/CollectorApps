using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class StampImageRepository : BaseRepository
    {
        public delegate void StampImageHandler(object sender, StampImageEventArgs e);
        public event StampImageHandler? StampImageChanged;
        public Entity.Stampimage? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Stampimages.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public Entity.Stampimage? GetByStampId(long StampId)
        {
            if (ApiContext == null) return null;
            return ApiContext.Stampimages.FirstOrDefault(x => x.StampId == StampId && !x.IsDeleted);
        }
        public List<Entity.Stampimage>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Stampimages.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Stampimage? Create(Entity.Stampimage? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Stampimages.Add(Source);
            ApiContext.SaveChanges();
            StampImageChanged?.Invoke(this, new StampImageEventArgs(Source));
            return Source;
        }
        public Entity.Stampimage? Update(Entity.Stampimage? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Hunting = Source.Hunting;
            record.Original = Source.Original;
            record.Thumpnail = Source.Thumpnail;
            ApiContext.SaveChanges();
            StampImageChanged?.Invoke(this, new StampImageEventArgs(record));
            return record;
        }
        public Entity.Stampimage? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            StampImageChanged?.Invoke(this, new StampImageEventArgs(record));
            return record;
        }
    }
    public class StampImageEventArgs : EventArgs
    {
        public Entity.Stampimage Stampimage { get; set; }
        public StampImageEventArgs(Entity.Stampimage stampImage)
        {
            Stampimage = stampImage;
        }
    }
}

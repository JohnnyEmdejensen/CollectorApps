using Collector.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class WatermarkRepository : BaseRepository
    {
        public delegate void WatermarkHandler(object sender, WatermarkEventArgs e);
        public event WatermarkHandler? WatermarkChanged;
        public Entity.Watermark? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Watermarkings.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Watermark>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Watermarkings.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Watermark? Create(Entity.Watermark? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Watermarkings.Add(Source);
            ApiContext.SaveChanges();
            WatermarkChanged?.Invoke(this, new WatermarkEventArgs(Source));
            return Source;
        }
        public Entity.Watermark? Update(Entity.Watermark? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            WatermarkChanged?.Invoke(this, new WatermarkEventArgs(record));
            return record;
        }
        public Entity.Watermark? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            WatermarkChanged?.Invoke(this, new WatermarkEventArgs(record));
            return record;
        }
    }
    public class WatermarkEventArgs : EventArgs
    {
        public Entity.Watermark Watermark { get; set; }

        public WatermarkEventArgs(Entity.Watermark watermark)
        {
            Watermark = watermark;
        }
    }
}

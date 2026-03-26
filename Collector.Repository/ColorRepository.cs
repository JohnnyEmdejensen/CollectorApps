using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class ColorRepository : BaseRepository
    {
        public delegate void ColorHandler(object sender, ColorEventArgs e);
        public event ColorHandler? ColorChanged;

        public Entity.Color? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Colors.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Color>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Colors.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Color? Create(Entity.Color? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Colors.Add(Source);
            ApiContext.SaveChanges();
            ColorChanged?.Invoke(this, new ColorEventArgs(Source));
            return Source;
        }
        public Entity.Color? Update(Entity.Color? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            ColorChanged?.Invoke(this, new ColorEventArgs(record));
            return record;
        }
        public Entity.Color? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            ColorChanged?.Invoke(this, new ColorEventArgs(record));
            return record;
        }
    }

    public class ColorEventArgs : EventArgs
    {
        public Entity.Color Color { get; set; }

        public ColorEventArgs(Entity.Color color)
        {
            Color = color;
        }
    }
}

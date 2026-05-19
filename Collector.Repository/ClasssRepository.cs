using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class ClasssRepository : BaseRepository
    {
        public delegate void ClasssHandler(object sender, ClasssEventArgs e);
        public event ClasssHandler? ClasssChanged;
        public Entity.Classs? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Classes.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Classs>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Classes.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Classs? Create(Entity.Classs? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Classes.Add(Source);
            ApiContext.SaveChanges();
            ClasssChanged?.Invoke(this, new ClasssEventArgs(Source));
            return Source;
        }
        public Entity.Classs? Update(Entity.Classs? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            ClasssChanged?.Invoke(this, new ClasssEventArgs(record));
            return record;
        }
        public Entity.Classs? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            ClasssChanged?.Invoke(this, new ClasssEventArgs(record));
            return record;
        }
    }
    public class ClasssEventArgs : EventArgs
    {
        public Entity.Classs Classs { get; set; }
        public ClasssEventArgs(Entity.Classs classs)
        {
            Classs = classs;
        }
    }
}

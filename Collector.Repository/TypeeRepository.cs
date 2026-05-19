using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class TypeeRepository : BaseRepository
    {
        public delegate void TypeeHandler(object sender, TypeeEventArgs e);
        public event TypeeHandler? TypeeChanged;
        public Entity.Typee? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Typees.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Typee>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Typees.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Typee? Create(Entity.Typee? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Typees.Add(Source);
            ApiContext.SaveChanges();
            TypeeChanged?.Invoke(this, new TypeeEventArgs(Source));
            return Source;
        }
        public Entity.Typee? Update(Entity.Typee? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            TypeeChanged?.Invoke(this, new TypeeEventArgs(record));
            return record;
        }
        public Entity.Typee? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            TypeeChanged?.Invoke(this, new TypeeEventArgs(record));
            return record;
        }
    }
    public class TypeeEventArgs : EventArgs
    {
        public Entity.Typee Typee { get; set; }
        public TypeeEventArgs(Entity.Typee typee)
        {
            Typee = typee;
        }
    }
}

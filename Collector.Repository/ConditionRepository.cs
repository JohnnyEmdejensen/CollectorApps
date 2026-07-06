using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class ConditionRepository : BaseRepository
    {
        public delegate void ConditionHandler(object sender, ConditionEventArgs e);
        public event ConditionHandler? ConditionChanged;
        public Entity.Condition? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Conditions.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Condition>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Conditions.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Condition? Create(Entity.Condition? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Conditions.Add(Source);
            ApiContext.SaveChanges();
            ConditionChanged?.Invoke(this, new ConditionEventArgs(Source));
            return Source;
        }
        public Entity.Condition? Update(Entity.Condition? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            record.Sort = Source.Sort;
            ApiContext.SaveChanges();
            ConditionChanged?.Invoke(this, new ConditionEventArgs(record));
            return record;
        }
        public Entity.Condition? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            ConditionChanged?.Invoke(this, new ConditionEventArgs(record));
            return record;
        }
    }
    public class ConditionEventArgs : EventArgs
    {
        public Entity.Condition Condition { get; set; }
        public ConditionEventArgs(Entity.Condition condition)
        {
            Condition = condition;
        }
    }
}

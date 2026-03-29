using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class TopicRepository : BaseRepository
    {
        public delegate void TopicHandler(object sender, TopicEventArgs e);
        public event TopicHandler? TopicChanged;
        public Entity.Topic? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Topics.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Topic>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Topics.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Topic? Create(Entity.Topic? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Topics.Add(Source);
            ApiContext.SaveChanges();
            TopicChanged?.Invoke (this, new TopicEventArgs(Source));
            return Source;
        }
        public Entity.Topic? Update(Entity.Topic? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            ApiContext.SaveChanges();
            TopicChanged?.Invoke(this, new TopicEventArgs(record));
            return record;
        }
        public Entity.Topic? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            TopicChanged?.Invoke(this, new TopicEventArgs(record));
            return record;
        }
    }
    public class TopicEventArgs : EventArgs
    {
        public Entity.Topic Topic { get; set; }

        public TopicEventArgs(Entity.Topic topic)
        {
            Topic = topic;
        }
    }

}

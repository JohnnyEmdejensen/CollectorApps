using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class CategoryRepository : BaseRepository
    {
        public delegate void CategoryHandler(object sender, CategoryEventArgs e);
        public event CategoryHandler? CategoryChanged;

        public Entity.Category? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.Categories.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
        }
        public List<Entity.Category>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.Categories.Where(x => !x.IsDeleted).ToList();
        }
        public Entity.Category? Create(Entity.Category? Source)
        {
            if (ApiContext == null || Source == null) return null;
            ApiContext.Categories.Add(Source);
            ApiContext.SaveChanges();
            CategoryChanged?.Invoke(this, new CategoryEventArgs(Source));
            return Source;
        }
        public Entity.Category? Update(Entity.Category? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Value = Source.Value;
            record.UKValue = Source.UKValue;
            ApiContext.SaveChanges();
            CategoryChanged?.Invoke(this, new CategoryEventArgs(record));
            return record;
        }
        public Entity.Category? Delete(long Id)
        {
            if (ApiContext == null) return null;
            var record = Get(Id);
            if (record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            CategoryChanged?.Invoke(this, new CategoryEventArgs(record));
            return record;
        }
    }
    public class CategoryEventArgs : EventArgs
    {
        public Entity.Category Category { get; set; }
        public CategoryEventArgs(Entity.Category category)
        {
            Category = category;
        }
    }
}

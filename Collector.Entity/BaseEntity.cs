using System.ComponentModel.DataAnnotations;

namespace Collector.Entity
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsDeleted { get; set; }
    }
}

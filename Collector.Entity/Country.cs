using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class Country : BaseEntity
    {
        [MaxLength(128)]
        public string Name { get; set; } = "";
        [MaxLength(8)]
        public string? Iso { get; set; }
        public int? Number { get; set; }
        public long? ContinentId { get; set; }
    }
}

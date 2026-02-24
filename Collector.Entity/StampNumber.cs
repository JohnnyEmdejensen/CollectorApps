using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class StampNumber : BaseEntity
    {
        public long StampId { get; set; }
        public long CatalogId { get; set; }
        [MaxLength(8)]
        public string? Prefix { get; set; }
        [MaxLength(8)]
        public string Number { get; set; } = "";
        [MaxLength(8)]
        public string? Suffix { get; set; }
    }
}

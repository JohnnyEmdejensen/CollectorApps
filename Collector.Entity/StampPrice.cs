using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class StampPrice : BaseEntity
    {
        [Required]
        public long StampId { get; set; }
        [Required]
        public long ConditionId { get; set; }
        [Required]
        public double Price { get; set; }
    }
}

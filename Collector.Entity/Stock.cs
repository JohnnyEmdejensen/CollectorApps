using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class Stock : BaseEntity
    {
        public long StampId { get; set; }
        public int Quantity { get; set; }
        public long ConditionId { get; set; }
        public long LocationId { get; set; }
        public long SupplierDealId { get; set; }

        [NotMapped]
        public bool IsDirty { get; set; }
    }
}

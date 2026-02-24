using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class StampValue : BaseEntity
    {
        public long StampId { get; set; }
        public long CatalogId { get; set; }
        public double? PriceLetter { get; set; }
        public double? PriceFDC { get; set; }
        public double? PriceBlock { get; set; }
        public double? PriceMint { get; set; }
        public double? PriceUnused { get; set; }
        public double? PriceUsed { get; set; }
    }
}

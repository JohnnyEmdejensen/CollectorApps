using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class Supplierdeal : BaseIdValue
    {
        public long SupplierId { get; set; }
        public double Price { get; set; }
    }
}

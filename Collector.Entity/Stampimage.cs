using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class Stampimage : BaseEntity
    {
        public long StampId { get; set; }
        public byte[]? Thumpnail { get; set; }
        public byte[]? Original { get; set; }
        public byte[]? Hunting { get; set; }
    }
}

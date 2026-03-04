using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class PublishedCountryCategoryLink : BaseEntity
    {
        public long PublishedCountryId { get; set; }
        public long CategoryId { get; set; }
    }
}

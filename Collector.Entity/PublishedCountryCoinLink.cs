using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class PublishedCountryCoinLink : BaseEntity
    {
        public long PublishedCountryId { get; set; }
        public long CoinId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.ViewModel
{
    public class PublishedCountryCoinView
    {
        public long Id { get; set; }
        public long PublishedCountryId { get; set; }
        public string PublishedCountry { get; set; } = "";
        public long CoinId { get; set; }
        public string Coin { get; set; } = "";

    }
}

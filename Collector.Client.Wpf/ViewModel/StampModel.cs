using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.ViewModel
{
    public class StampModel
    {
        public long Id { get; set; }
        public string? PublishingCountry { get; set; }
        public string? Catalog { get; set; }
        public string? FormattedIssued { get; set; }
        public string? Number { get; set; }
        public double? Facevalue { get; set; }
        public string? Coin { get; set; }
        public string? Design { get; set; }
        public string? Color { get; set; }
        public string? Category { get; set; }
        public string? SortField { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }

        public long? ContinentId { get; set; }
        public long? CountryId { get; set; }
        public long? PublishedCountryId { get; set; }

    }
}

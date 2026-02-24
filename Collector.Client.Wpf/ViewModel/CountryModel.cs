using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.ViewModel
{
    public class CountryModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public string? Iso { get; set; }
        public int? Number { get; set; }
        public string Continent { get; set; } = "";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.ViewModel
{
    public class PublishedCountryModel
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public string Name { get; set; } = "";
        public string CountryName { get; set; } = "";

    }
}

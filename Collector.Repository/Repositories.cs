using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Repository
{
    public class Repositories
    {
        public Repositories()        
        { 
            ContinentRepository = new ContinentRepository();
            CountryRepository = new CountryRepository();
            CatalogRepository = new CatalogRepository();
            CategoryRepository = new CategoryRepository();
            PublishedcountryRepository = new PublishedcountryRepository();
            PublishedCountryCategortRepository = new PublishedCountryCategortRepository();
            CoinRepository = new CoinRepository();
            PublishedCountryCoinRepository = new PublishedCountryCoinRepository();
            ColorRepository = new ColorRepository();
        }

        public ColorRepository ColorRepository { get; set; }
        public PublishedCountryCoinRepository PublishedCountryCoinRepository { get; set; }
        public CoinRepository CoinRepository { get; set; }
        public PublishedCountryCategortRepository PublishedCountryCategortRepository { get; set; }
        public PublishedcountryRepository PublishedcountryRepository { get; set; }
        public CategoryRepository CategoryRepository { get; set; }
        public CatalogRepository CatalogRepository { get; set; }
        public CountryRepository CountryRepository { get; set; }
        public ContinentRepository ContinentRepository { get; set; }
    }
}

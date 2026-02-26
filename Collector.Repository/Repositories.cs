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
        }

        public CategoryRepository CategoryRepository { get; set; }
        public CatalogRepository CatalogRepository { get; set; }
        public CountryRepository CountryRepository { get; set; }
        public ContinentRepository ContinentRepository { get; set; }
    }
}

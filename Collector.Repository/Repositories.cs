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
        }

        public CountryRepository CountryRepository { get; set; }
        public ContinentRepository ContinentRepository { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Collector.Client.Wpf.Internals
{
    public class DataShareClass
    {
        public List<Entity.Catalog>? CatalogList { get; set; }
        public List<Entity.Continent>? ContinentList { get; set; }
        public List<Entity.Country>? CountryList { get; set; }

        public DataShareClass() 
        { 

        }

        public void Initialize()
        {
            GetContinents();
            GetCountries();
            GetCatalogs();
        }

        public void GetCatalogs()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            CatalogList = App.AppShare.Repositories.CatalogRepository.GetAll();
            if (CatalogList != null) 
                CatalogList = CatalogList.OrderBy(x => x.Value).ToList();
            App.EventHelper.RaiseCatalogListChangedEvent();
            Mouse.OverrideCursor = c;
        }
        public void GetCountries()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            CountryList = App.AppShare.Repositories.CountryRepository.GetAll();
            if (CountryList != null)
                CountryList = CountryList.OrderBy(x => x.Name).ToList();
            App.EventHelper.RaiseCountryListChangedEvent();
            Mouse.OverrideCursor = c;
        }
        public void GetContinents()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            ContinentList = App.AppShare.Repositories.ContinentRepository.GetAll();
            if (ContinentList != null)
                ContinentList = ContinentList.OrderBy(x => x.Value).ToList();   
            App.EventHelper.RaiseContinentListChangedEvent();
            Mouse.OverrideCursor = c;
        }
    }
}

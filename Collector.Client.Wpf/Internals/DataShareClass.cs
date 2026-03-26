using Collector.Entity;
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
        public List<Internals.WorkerItem> WorkerItems { get; set; }
        public List<Entity.Catalog>? CatalogList { get; set; }
        public List<Entity.Continent>? ContinentList { get; set; }
        public List<Entity.Country>? CountryList { get; set; }
        public List<Entity.Category>? CategoryList { get; set; }
        public List<Entity.PublishedCountry>? PublishedCountryList { get; set; }
        public List<Entity.PublishedCountryCategoryLink>? PublishedCountryCategoryList { get; set; }
        public List<Entity.PublishedCountryCoinLink>? PublishedCountryCoinLinks { get; set; }
        public List<Entity.Coin>? CoinList { get; set; }
        public List<Entity.Color>? ColorList { get; set; }
        public DataShareClass() 
        { 
            WorkerItems = new List<Internals.WorkerItem>();
        }

        public void Initialize()
        {
            GetContinents();
            GetCountries();
            GetCatalogs();
            GetCategories();
            GetPublishedCountries();
            GetPublishedCountryCategoryLinks();
            GetCoins();
            GetPublishedCountryCoinLinks();
            GetColors();
        }

        public void GetColors()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            ColorList = App.AppShare.Repositories.ColorRepository.GetAll();
            if (ColorList != null)
                ColorList = ColorList.OrderBy(x => x.Value).ToList();
            App.EventHelper.RaiseColorListChangedEvent();
            Mouse.OverrideCursor = c;
        }
        public void GetPublishedCountryCoinLinks()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            PublishedCountryCoinLinks = App.AppShare.Repositories.PublishedCountryCoinRepository.GetAll();
            App.EventHelper.RaisePublishedCountryCoinLinksChangedEvent();
            Mouse.OverrideCursor = c;
        }
        public void GetCoins()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            CoinList = App.AppShare.Repositories.CoinRepository.GetAll();
            if (CoinList != null)
                CoinList = CoinList.OrderBy(x => x.Value).ToList();
            App.EventHelper.RaiseCoinListChangedEvent();
            Mouse.OverrideCursor = c;
        }
        public void GetPublishedCountryCategoryLinks()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            PublishedCountryCategoryList = App.AppShare.Repositories.PublishedCountryCategortRepository.GetAll();
            App.EventHelper.RaisePublishedCountryCategoryListChangedEvent();
            Mouse.OverrideCursor = c;
        }

        public void GetPublishedCountries()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            PublishedCountryList = App.AppShare.Repositories.PublishedcountryRepository.GetAll();
            if (PublishedCountryList != null)
                PublishedCountryList = PublishedCountryList.OrderBy(x => x.Name).ToList();
            App.EventHelper.RaisePublishedCountryListChangedEvent();
            Mouse.OverrideCursor = c;
        }
        public void GetCategories()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            CategoryList = App.AppShare.Repositories.CategoryRepository.GetAll();
            if (CategoryList != null)
                CategoryList = CategoryList.OrderBy(x => x.Value).ToList();
            App.EventHelper.RaiseCategoryListChangedEvent();
            Mouse.OverrideCursor = c;
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

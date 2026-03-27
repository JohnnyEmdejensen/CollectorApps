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
            using (WorkerHandler worker = new WorkerHandler("Initialize"))
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
        }

        public void GetColors()
        {
            using (WorkerHandler worker = new WorkerHandler("GetColors"))
            {
                ColorList = App.AppShare.Repositories.ColorRepository.GetAll();
                if (ColorList != null)
                    ColorList = ColorList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaiseColorListChangedEvent();
            }
        }
        public void GetPublishedCountryCoinLinks()
        {
            using (WorkerHandler worker = new WorkerHandler("GetPublishedCountryCoinLinks"))
            {
                PublishedCountryCoinLinks = App.AppShare.Repositories.PublishedCountryCoinRepository.GetAll();
                App.EventHelper.RaisePublishedCountryCoinLinksChangedEvent();
            }
        }
        public void GetCoins()
        {
            using (WorkerHandler worker = new WorkerHandler("GetCoins"))
            {
                CoinList = App.AppShare.Repositories.CoinRepository.GetAll();
                if (CoinList != null)
                    CoinList = CoinList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaiseCoinListChangedEvent();
            }
        }
        public void GetPublishedCountryCategoryLinks()
        {
            using (WorkerHandler worker = new WorkerHandler("GetPublishedCountryCategoryLinks"))
            {
                PublishedCountryCategoryList = App.AppShare.Repositories.PublishedCountryCategortRepository.GetAll();
                App.EventHelper.RaisePublishedCountryCategoryListChangedEvent();
            }
        }

        public void GetPublishedCountries()
        {
            using (WorkerHandler worker = new WorkerHandler("GetPublishedCountries"))
            {
                PublishedCountryList = App.AppShare.Repositories.PublishedcountryRepository.GetAll();
                if (PublishedCountryList != null)
                    PublishedCountryList = PublishedCountryList.OrderBy(x => x.Name).ToList();
                App.EventHelper.RaisePublishedCountryListChangedEvent();
            }
        }
        public void GetCategories()
        {
            using (WorkerHandler worker = new WorkerHandler("GetCategories"))
            {
                CategoryList = App.AppShare.Repositories.CategoryRepository.GetAll();
                if (CategoryList != null)
                    CategoryList = CategoryList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaiseCategoryListChangedEvent();
            }
        }
        public void GetCatalogs()
        {
            using (WorkerHandler worker = new WorkerHandler("GetCatalogs"))
            {
                CatalogList = App.AppShare.Repositories.CatalogRepository.GetAll();
                if (CatalogList != null)
                    CatalogList = CatalogList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaiseCatalogListChangedEvent();
            }
        }
        public void GetCountries()
        {
            using (WorkerHandler worker = new WorkerHandler("GetCountries"))
            {
                CountryList = App.AppShare.Repositories.CountryRepository.GetAll();
                if (CountryList != null)
                    CountryList = CountryList.OrderBy(x => x.Name).ToList();
                App.EventHelper.RaiseCountryListChangedEvent();
            }
        }
        public void GetContinents()
        {
            using (WorkerHandler worker = new WorkerHandler("GetContinents"))
            {
                ContinentList = App.AppShare.Repositories.ContinentRepository.GetAll();
                if (ContinentList != null)
                    ContinentList = ContinentList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaiseContinentListChangedEvent();
            }
        }
    }
}

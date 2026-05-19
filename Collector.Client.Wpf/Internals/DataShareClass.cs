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
        public List<Entity.Stamp>? StampList { get; set; }
        public List<ViewModel.StampModel>? StampModelList { get; set; }
        public List<Entity.Watermark>? WatermarkList { get; set; }
        public List<Entity.Topic>? TopicList { get; set; }
        public List<Entity.Condition>? ConditionList { get; set; }
        public List<Entity.Location>? LocationList { get; set; }
        public List<Entity.Supplier>? SupplierList { get; set; }
        public List<Entity.Supplierdeal>? SupplierDealList { get; set; }
        public List<Entity.Stock>? StockList { get; set; }
        public List<Entity.StampNumber>? StampNumberList { get; set; }
        public List<Entity.Perforation>? PerforationList { get; set; }
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
                GetWatermarks();
                GetTopics();
                GetConditions();
                GetPerforations();
                GetLocations();
                GetSuppliers();
                GetSupplierDeals();
                GetStamps();
                GetStampNumbers();
            }
        }

        public void GetSupplierDeals()
        {
            using (new WorkerHandler("GetSupplierDeals"))
            {
                SupplierDealList = App.AppShare.Repositories.SupplierDealRepository.GetAll();
                if (SupplierDealList != null)
                    SupplierDealList = SupplierDealList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaiseSupplierDealListChangedEvent();
            }
        }
        public void GetSuppliers()
        {
            using (new WorkerHandler("GetSuppliers"))
            {
                SupplierList = App.AppShare.Repositories.SupplierRepository.GetAll();
                if (SupplierList != null) 
                    SupplierList = SupplierList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaiseSupplierListChangedEvent();
            }
        }
        public void GetLocations()
        {
            using (new WorkerHandler("GetLocations"))
            {
                LocationList = App.AppShare.Repositories.LocationRepository.GetAll();
                if (LocationList != null)
                    LocationList = LocationList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaiseLocationListChangedEvent();
            }
        }

        public void GetPerforations()
        {
            using (new WorkerHandler("GetPerforations"))
            {
                PerforationList = App.AppShare.Repositories.PerforationRepository.GetAll();
                if (PerforationList != null)
                    PerforationList = PerforationList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaisePerforationListChanged();
            }
        }
        public void GetConditions()
        {
            ConditionList = App.AppShare.Repositories.ConditionRepository.GetAll();
            if (ConditionList != null)
                ConditionList = ConditionList.OrderBy(x => x.Value).ToList();
            App.EventHelper.RaiseConditionListChangedEvent();
        }

        public void GetStampNumbers()
        {
            using (new WorkerHandler("GetStampNumbers"))
            {
                StampNumberList = App.AppShare.Repositories.StampNumberRepository.GetAll();                
            }
        }
        public void GetStamps()
        {
            using (WorkerHandler worker = new WorkerHandler("GetStamps"))
            {
                string spacer3 = "  ";
                if (App.AppShare.SelectedPublishedCountry == null)
                    StampList = App.AppShare.Repositories.StampRepository.GetAll();
                else
                    StampList = App.AppShare.Repositories.StampRepository.GetByPublishedCountry(App.AppShare.SelectedPublishedCountry.Id);
                if (StampList != null)
                {
                    StampModelList = new List<ViewModel.StampModel>();
                    foreach (var stamp in StampList)
                    {
                        var suffixValue = stamp.Suffix ?? "";
                        stamp.SortField = string.Format("{0:00000}", Convert.ToInt32( stamp.Number)) + spacer3.Substring(0, 3 - suffixValue.Length-1);
                        stamp.FormattetIssued = App.ToolHelper.FormatIssued(stamp.Issued);
                        ViewModel.StampModel sm = new ViewModel.StampModel();
                        sm.Id = stamp.Id;
                        sm.Catalog = App.ToolHelper.GetCatalogName(stamp.CatalogId);
                        sm.Category = App.ToolHelper.GetCategoryName(stamp.CategoryId);
                        sm.Coin = App.ToolHelper.GetCoinName(stamp.CoinId);
                        sm.Color = App.ToolHelper.GetColorName(stamp.ColorId);
                        sm.Design = stamp.Design;
                        sm.Facevalue = stamp.Facevalue;
                        sm.FormattedIssued = stamp.FormattetIssued;
                        sm.Height = stamp.Height;
                        sm.Number = stamp.Number;
                        sm.PublishingCountry = App.ToolHelper.GetPublishedCountryName(stamp.PublishedCountryId);
                        sm.SortField = stamp.SortField;
                        sm.Width = stamp.Width;

                        sm.PublishedCountryId = stamp.PublishedCountryId;
                        if (stamp.PublishedCountryId.HasValue)
                            sm.CountryId = App.ToolHelper.GetCountryByPublishedId(stamp.PublishedCountryId.Value);
                        if (sm.CountryId.HasValue)
                            sm.ContinentId = App.ToolHelper.GetContinentIdFromCountry(sm.CountryId.Value);
                        StampModelList.Add(sm);
                    }
                    StampList = StampList.OrderBy(x => x.SortField).ToList();
                    StampModelList = StampModelList.OrderBy(x => x.SortField).ToList();
                    App.EventHelper.RaiseStampModelListChangedEvent();
                }
            }
        }

        public void GetTopics()
        {
            using (new WorkerHandler("GetTopics"))
            {
                TopicList = App.AppShare.Repositories.TopicRepository.GetAll();
                if (TopicList != null)
                    TopicList = TopicList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaiseTopicListChangedEvent();
            }
        }
        public void GetWatermarks()
        {
            using (WorkerHandler worker = new WorkerHandler("GetWatermarks"))
            {
                WatermarkList = App.AppShare.Repositories.WatermarkRepository.GetAll();
                if (WatermarkList != null)
                    WatermarkList = WatermarkList.OrderBy(x => x.Value).ToList();
                App.EventHelper.RaiseWatermarkListChangedEvent();

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

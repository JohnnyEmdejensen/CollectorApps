using Collector.Entity;
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
            StampRepository = new StampRepository();
            WatermarkRepository = new WatermarkRepository();
            TopicRepository = new TopicRepository();
            ConditionRepository = new ConditionRepository();
            LocationRepository = new LocationRepository();
            SupplierRepository = new SupplierRepository();
            SupplierDealRepository = new SupplierDealRepository();
            StockRepository = new StockRepository();
            StampImageRepository = new StampImageRepository();
            StampNumberRepository = new StampNumberRepository();
            PerforationRepository = new PerforationRepository();
            ClasssRepository = new ClasssRepository();
            TypeeRepository = new TypeeRepository();
            StampPriceRepository = new StampPriceRepository();
        }

        public StampPriceRepository StampPriceRepository { get; set; }
        public ClasssRepository ClasssRepository { get; set; }
        public TypeeRepository TypeeRepository { get; set; }
        public PerforationRepository PerforationRepository { get; set; }
        public StampNumberRepository StampNumberRepository { get; set; }
        public StampImageRepository StampImageRepository { get; set; }
        public StockRepository StockRepository { get; set; }
        public SupplierDealRepository SupplierDealRepository { get; set; }
        public SupplierRepository SupplierRepository { get; set; }
        public LocationRepository LocationRepository { get; set; }
        public ConditionRepository ConditionRepository { get; set; }
        public TopicRepository TopicRepository { get; set; }
        public WatermarkRepository WatermarkRepository { get; set; }
        public StampRepository StampRepository { get; set; }
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

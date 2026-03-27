using Collector.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Collector.Client.Wpf.Internals
{
    public class AppShareCLass
    {
        public Repository.Repositories Repositories { get; set; }
        public Entity.PublishedCountry? SelectedPublishedCountry { get; set; }
        public Entity.Country? SelectedCountry { get; set; }

        public AppShareCLass() 
        { 
            Repositories = new Repository.Repositories();
            Repositories.ContinentRepository.ContinentChanged += ContinentRepository_ContinentChanged;
            Repositories.CountryRepository.CountryChanged += CountryRepository_CountryChanged;
            Repositories.CatalogRepository.CatalogChanged += CatalogRepository_CatalogChanged;
            Repositories.CategoryRepository.CategoryChanged += CategoryRepository_CategoryChanged;
            Repositories.PublishedcountryRepository.PublishedCountryChanged += PublishedcountryRepository_PublishedCountryChanged;
            Repositories.PublishedCountryCategortRepository.PublishedCountryCategoryChanged += PublishedCountryCategortRepository_PublishedCountryCategoryChanged;
            Repositories.CoinRepository.CoinChanged += CoinRepository_CoinChanged;
            Repositories.PublishedCountryCoinRepository.PublishedCountryCoinLinkChanged += PublishedCountryCoinRepository_PublishedCountryCoinLinkChanged;
            Repositories.ColorRepository.ColorChanged += ColorRepository_ColorChanged;
        }

        private void ColorRepository_ColorChanged(object sender, ColorEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler(""))
            {
                App.DataShare.GetColors();
            }
        }

        private void PublishedCountryCoinRepository_PublishedCountryCoinLinkChanged(object sender, PublishedCountryCoinLinkEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler(""))
            {
                App.DataShare.GetPublishedCountryCoinLinks();
            }
        }

        private void CoinRepository_CoinChanged(object sender, CoinEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler(""))
            {
                App.DataShare.GetCoins();
            }
        }

        private void PublishedCountryCategortRepository_PublishedCountryCategoryChanged(object sender, PublishedCountryCategoryEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler(""))
            {
                App.DataShare.GetPublishedCountryCategoryLinks();
            }
        }

        private void PublishedcountryRepository_PublishedCountryChanged(object sender, PublishedCountryEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler(""))
            {
                App.DataShare.GetPublishedCountries();
            }
        }

        private void CategoryRepository_CategoryChanged(object sender, Repository.CategoryEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler(""))
            {
                App.DataShare.GetCategories();
            }
        }

        private void CatalogRepository_CatalogChanged(object sender, Repository.CatalogEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler(""))
            {
                App.DataShare.GetCatalogs();
            }
        }

        private void CountryRepository_CountryChanged(object sender, Repository.CountryEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler(""))
            {
                App.DataShare.GetCountries();
            }
        }

        private void ContinentRepository_ContinentChanged(object sender, Repository.ContinentEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler(""))
            {
                App.DataShare.GetContinents();
            }
        }
    }
}

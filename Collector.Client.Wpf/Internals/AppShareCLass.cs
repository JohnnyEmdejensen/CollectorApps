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

        public AppShareCLass() 
        { 
            Repositories = new Repository.Repositories();
            Repositories.ContinentRepository.ContinentChanged += ContinentRepository_ContinentChanged;
            Repositories.CountryRepository.CountryChanged += CountryRepository_CountryChanged;
            Repositories.CatalogRepository.CatalogChanged += CatalogRepository_CatalogChanged;
            Repositories.CategoryRepository.CategoryChanged += CategoryRepository_CategoryChanged;
        }

        private void CategoryRepository_CategoryChanged(object sender, Repository.CategoryEventArgs e)
        {
            App.DataShare.GetCategories();
        }

        private void CatalogRepository_CatalogChanged(object sender, Repository.CatalogEventArgs e)
        {
            App.DataShare.GetCatalogs();
        }

        private void CountryRepository_CountryChanged(object sender, Repository.CountryEventArgs e)
        {
            App.DataShare.GetCountries();
        }

        private void ContinentRepository_ContinentChanged(object sender, Repository.ContinentEventArgs e)
        {
            App.DataShare.GetContinents();
        }
    }
}

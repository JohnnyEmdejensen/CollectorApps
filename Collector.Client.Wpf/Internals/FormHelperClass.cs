using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.Internals
{
    public class FormHelperClass
    {
        public void ShowPublishedCountries(Entity.PublishedCountry? PublishedCountry)
        {
            Dialogs.PublishedCountryDialog frm = new Dialogs.PublishedCountryDialog
            {
                DataContext = PublishedCountry,
            };
            frm.Show();
        }
        public void ShowCategories(Entity.Category? Category)
        {
            Dialogs.CategoryDialog frm = new Dialogs.CategoryDialog
            {
                DataContext = Category,
            };
            frm.Show();
        }
        public void ShowCatalogs(Entity.Catalog? Catalog)
        {
            Dialogs.CatalogDialog frm = new Dialogs.CatalogDialog
            {
                DataContext = Catalog,
            };
            frm.Show();
        }
        public void ShowCountries(Entity.Country? Country)
        {
            Dialogs.CountryDialog frm = new Dialogs.CountryDialog
            {
                DataContext = Country,
            };
            frm.Show();
        }
    }
}

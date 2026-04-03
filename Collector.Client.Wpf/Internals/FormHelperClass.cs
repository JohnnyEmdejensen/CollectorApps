using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.Internals
{
    public class FormHelperClass
    {
        public void ShowSupplierdealsDialog(Entity.Supplierdeal? Supplierdeal)
        {
            Dialogs.SupplierDealDialog frm = new Dialogs.SupplierDealDialog()
            {
                DataContext = Supplierdeal,
            };
            frm.Show();
        }
        public void ShowSuppliersDialog(Entity.Supplier? Supplier)
        {
            Dialogs.SupplierDialog frm = new Dialogs.SupplierDialog()
            {
                DataContext = Supplier,
            };
            frm.Show();
        }
        public void ShowLocationsDialog(Entity.Location? Location)
        {
            Dialogs.LocationDialog frm = new Dialogs.LocationDialog()
            {
                DataContext = Location,
            };
            frm.Show();
        }
        public void ShowConditions(Entity.Condition? Condition)
        {
            Dialogs.ConditionDialog frm = new Dialogs.ConditionDialog()
            {
                DataContext = Condition,
            };
            frm.Show();
        }
        public void ShowTopicsDialog(Entity.Topic? Topic)
        {
            Dialogs.TopicDialog frm = new Dialogs.TopicDialog()
            {
                DataContext = Topic,
            };
            frm.Show();
        }
        public void ShowWatermarkDialog(Entity.Watermark? Watermark)
        {
            using (WorkerHandler worker = new WorkerHandler("ShowWatermarkDialog"))
            {
                Dialogs.WatermarkDialog frm = new Dialogs.WatermarkDialog()
                {
                    DataContext = Watermark,
                };
                frm.Show();
            }
        }
        public void ShowColorsDialog(Entity.Color? Color)
        {
            using (WorkerHandler worker = new WorkerHandler("ShowColorsDialog"))
            {
                Dialogs.ColorDialog frm = new Dialogs.ColorDialog()
                {
                    DataContext = Color,
                };
                frm.Show();
            }
        }
        public void ShowPublishingCountryCoinDialog(Entity.PublishedCountryCoinLink? Link)
        {
            using (WorkerHandler worker = new WorkerHandler("ShowPublishingCountryCoinDialog"))
            {
                Dialogs.PublishingCountryCoinDialog frm = new Dialogs.PublishingCountryCoinDialog()
                {
                    DataContext = Link,
                };
                frm.Show();
            }
        }
        public void ShowCoinunits(Entity.Coin? Coin)
        {
            using (WorkerHandler worker = new WorkerHandler("ShowCoinunits"))
            {
                Dialogs.CoinunitDialog frm = new Dialogs.CoinunitDialog
                {
                    DataContext = Coin,
                };
                frm.Show();
            }
        }
        public void ShowStampDialog(Entity.Stamp? Stamp)
        {
            using (WorkerHandler worker = new WorkerHandler("ShowStampDialog"))
            {
                Dialogs.StampDialog frm = new Dialogs.StampDialog
                {
                    DataContext = Stamp,
                };
                frm.Show();
            }
        }
        public void ShowPublishedCountryCategories(Entity.PublishedCountryCategoryLink? PublishedCountryCategory)
        {
            using (WorkerHandler worker = new WorkerHandler("ShowPublishedCountryCategories"))
            {
                Dialogs.PublishedcountryCategoryDialog frm = new Dialogs.PublishedcountryCategoryDialog
                {
                    DataContext = PublishedCountryCategory,
                };
                frm.Show();
            }
        }
        public void ShowPublishedCountries(Entity.PublishedCountry? PublishedCountry)
        {
            using (WorkerHandler worker = new WorkerHandler("ShowPublishedCountries"))
            {
                Dialogs.PublishedCountryDialog frm = new Dialogs.PublishedCountryDialog
                {
                    DataContext = PublishedCountry,
                };
                frm.Show();
            }
        }
        public void ShowCategories(Entity.Category? Category)
        {
            using (WorkerHandler worker = new WorkerHandler("ShowCategories"))
            {
                Dialogs.CategoryDialog frm = new Dialogs.CategoryDialog
                {
                    DataContext = Category,
                };
                frm.Show();
            }
        }
        public void ShowCatalogs(Entity.Catalog? Catalog)
        {
            using (WorkerHandler worker = new WorkerHandler("ShowCatalogs"))
            {
                Dialogs.CatalogDialog frm = new Dialogs.CatalogDialog
                {
                    DataContext = Catalog,
                };
                frm.Show();
            }
        }
        public void ShowCountries(Entity.Country? Country)
        {
            using (WorkerHandler worker = new WorkerHandler("ShowCountries"))
            {
                Dialogs.CountryDialog frm = new Dialogs.CountryDialog
                {
                    DataContext = Country,
                };
                frm.Show();
            }
        }
    }
}

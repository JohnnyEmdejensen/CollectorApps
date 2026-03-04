using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.Internals
{
    public class EventHelperClass
    {
        public delegate void DefaultHandler (object sender, EventArgs e);
        public event DefaultHandler? TranslationSourceChanged;
        public event DefaultHandler? CountryListChanged;
        public event DefaultHandler? ContinentListChanged;
        public event DefaultHandler? CatalogListChanged;
        public event DefaultHandler? CategoryListChanged;
        public event DefaultHandler? PublishedCountryListChanged;
        public event DefaultHandler? PublishedCountryCategoryListChanged;

        public void RaisePublishedCountryCategoryListChangedEvent()
        {
            PublishedCountryCategoryListChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaisePublishedCountryListChangedEvent()
        {
            PublishedCountryListChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCategoryListChangedEvent()
        {
            CategoryListChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCatalogListChangedEvent()
        {
            CatalogListChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCountryListChangedEvent()
        {
            CountryListChanged?.Invoke (this, EventArgs.Empty);
        }
        public void RaiseContinentListChangedEvent()
        {
            ContinentListChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseTranslationSourceChangedEvent()
        {
            TranslationSourceChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}

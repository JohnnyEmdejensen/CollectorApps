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
        public event DefaultHandler? CoinListChanged;
        public event DefaultHandler? PublishedCountryCoinLinksChanged;
        public event DefaultHandler? ColorListChanged;
        public event DefaultHandler? StampModelListChanged;
        public event DefaultHandler? WatermarkListChanged;
        public event DefaultHandler? TopicListChanged;

        public void RaiseTopicListChangedEvent()
        {
            TopicListChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseWatermarkListChangedEvent()
        {
            WatermarkListChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseStampModelListChangedEvent()
        {
            StampModelListChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseColorListChangedEvent()
        {
            using (WorkerHandler worker = new WorkerHandler("RaiseColorListChangedEvent"))
            {
                ColorListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public void RaisePublishedCountryCoinLinksChangedEvent()
        {
            using (WorkerHandler worker = new WorkerHandler("RaisePublishedCountryCoinLinksChangedEvent"))
            {
                PublishedCountryCoinLinksChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public void RaiseCoinListChangedEvent()
        {
            using (WorkerHandler worker = new WorkerHandler("RaiseCoinListChangedEvent"))
            {
                CoinListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public void RaisePublishedCountryCategoryListChangedEvent()
        {
            using (WorkerHandler worker = new WorkerHandler("RaisePublishedCountryCategoryListChangedEvent"))
            {
                PublishedCountryCategoryListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public void RaisePublishedCountryListChangedEvent()
        {
            using (WorkerHandler worker = new WorkerHandler("RaisePublishedCountryListChangedEvent"))
            {
                PublishedCountryListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public void RaiseCategoryListChangedEvent()
        {
            using (WorkerHandler worker = new WorkerHandler("RaiseCategoryListChangedEvent"))
            {
                CategoryListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public void RaiseCatalogListChangedEvent()
        {
            using (WorkerHandler worker = new WorkerHandler("RaiseCatalogListChangedEvent"))
            {
                CatalogListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public void RaiseCountryListChangedEvent()
        {
            using (WorkerHandler worker = new WorkerHandler("RaiseCountryListChangedEvent"))
            {
                CountryListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public void RaiseContinentListChangedEvent()
        {
            using (WorkerHandler worker = new WorkerHandler("RaiseContinentListChangedEvent"))
            {
                ContinentListChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public void RaiseTranslationSourceChangedEvent()
        {
            using (WorkerHandler worker = new WorkerHandler("RaiseTranslationSourceChangedEvent"))
            {
                TranslationSourceChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}

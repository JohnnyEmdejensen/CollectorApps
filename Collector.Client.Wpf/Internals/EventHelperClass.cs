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

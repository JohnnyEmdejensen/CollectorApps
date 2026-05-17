using Collector.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.Internals
{
    public class FilterContainer
    {
        public delegate void ContinentHandler(object sender, ContinentEventArgs e);
        public delegate void CountryHandler(object sender, CountryEventArgs e);
        public delegate void PublishedCountryHandler(object sender, XPublishedCountryEventArgs e );
        public event ContinentHandler? SelectedContinentChanged;
        public event CountryHandler? SelectedCountryChanged;
        public event PublishedCountryHandler? SelectedPublishedCountryChanged;

        private Continent? selectedContinent;
        private PublishedCountry? selectedPublishedCountry;
        private Country? selectedCountry;

        public FilterContainer() 
        { 

        }

        public Entity.Continent? SelectedContinent 
        { 
            get => selectedContinent;
            set
            {
                if (value != selectedContinent)
                {
                    selectedContinent = value;
                    SelectedContinentChanged?.Invoke(this, new ContinentEventArgs(value));
                }
            }
        }
        public Entity.Country? SelectedCountry 
        { 
            get => selectedCountry;
            set
            {
                if (selectedCountry != value)
                {
                    selectedCountry = value;
                    SelectedCountryChanged?.Invoke(this, new CountryEventArgs(value));
                }
            }
        }
        public Entity.PublishedCountry? SelectedPublishedCountry 
        { 
            get => selectedPublishedCountry;
            set
            {
                if (selectedPublishedCountry != value)
                {
                    selectedPublishedCountry = value;
                    SelectedPublishedCountryChanged?.Invoke(this, new XPublishedCountryEventArgs(value));
                }
            }
        }


        public void SuspendEvents()
        {

        }
    }
    public class ContinentEventArgs : EventArgs
    {
        public ContinentEventArgs(Entity.Continent? continent) { Continent = continent; }
        public Entity.Continent? Continent { get; set; }
    }
    public class CountryEventArgs : EventArgs
    {
        public CountryEventArgs(Entity.Country? country) { Country = country; }
        public Entity.Country? Country { get; set; }
    }
    public class XPublishedCountryEventArgs : EventArgs
    {
        public XPublishedCountryEventArgs(Entity.PublishedCountry? publishedCountry) { PublishedCountry = publishedCountry; } 
        Entity.PublishedCountry? PublishedCountry { get; set; }
    }
}

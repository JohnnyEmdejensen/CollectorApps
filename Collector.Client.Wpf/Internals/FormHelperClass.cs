using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.Internals
{
    public class FormHelperClass
    {
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

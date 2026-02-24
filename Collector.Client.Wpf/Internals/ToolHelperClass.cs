using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.Internals
{
    public class ToolHelperClass
    {
        public string GetContinentName(long? Id)
        {
            string result = "";
            if (App.DataShare.ContinentList != null)
            {
                var continent = App.DataShare.ContinentList.FirstOrDefault(x => x.Id == Id);
                if (continent != null)
                {
                    result = continent.Value;
                }
            }
            return result;
        }
    }
}

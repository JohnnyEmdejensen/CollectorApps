using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class Category : BaseIdValue
    {
        [MaxLength(128)]
        public string UKValue { get; set; } = "";
        public bool AutoGenerate { get; set; }
    }
}

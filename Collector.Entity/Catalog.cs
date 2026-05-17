using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class Catalog : BaseIdValue
    {
        [MaxLength(1024)]
        public string? CatalogUrl { get; set; }
    }
}

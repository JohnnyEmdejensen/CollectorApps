using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class PublishedCountry : BaseEntity
    {
        [MaxLength(128)]
        public string Name { get; set; } = "";
        public long ContryId { get; set; }
        [MaxLength(128)]
        public string? CatalogCountryName { get; set; }
    }
}

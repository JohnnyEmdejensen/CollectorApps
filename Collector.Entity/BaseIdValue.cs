using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class BaseIdValue : BaseEntity
    {
        [MaxLength(128)]
        public string Value { get; set; } = "";
    }
}

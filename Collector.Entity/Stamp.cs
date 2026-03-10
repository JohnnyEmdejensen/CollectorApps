using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Entity
{
    public class Stamp : BaseEntity
    {
        [MaxLength(8)]
        public string? Prefix { get; set; }
        [MaxLength(8)]
        public string Number { get; set; } = "";
        [MaxLength(8)]
        public string? Suffix { get; set; }
        public int Issued { get; set; }
        [NotMapped]
        public string FormattetIssued { get; set; } = "";
        [MaxLength(1024)]
        public string Design { get; set; } = "";
        public double? Facevalue { get; set; }
        public long? CoinId { get; set; }
        public long? PublishedCountryId { get; set; }
        public long CategoryId { get; set; }
        public long CatalogId { get; set; }
        public long? ColorId { get; set; }
        public long? TopicId { get; set; }
        public long? PerforationHorId {  get; set; }
        public long? PerforationVerId {  get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public long? WatermarkId { get; set; }
        [MaxLength(4096)]
        public string? Description { get; set; }
        [MaxLength(1024)]
        public string? Motive { get; set; }
        public long? IssuedAmount { get; set; }
        [MaxLength(128)]
        public string? Designer { get; set; }
        [MaxLength(128)]
        public string? Ingraver { get; set; }

    }
}

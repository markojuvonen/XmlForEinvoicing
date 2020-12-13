using System;
using System.Collections.Generic;

#nullable disable

namespace XmlForEinvoicing.Models
{
    public partial class Tilaus
    {
        public int TilausId { get; set; }
        public int? AsiakasId { get; set; }
        public DateTime? TilausPvm { get; set; }
        public DateTime? ToimitusPvm { get; set; }
        public decimal? Tilaussumma { get; set; }
    }
}

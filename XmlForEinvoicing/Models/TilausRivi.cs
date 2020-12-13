using System;
using System.Collections.Generic;

#nullable disable

namespace XmlForEinvoicing.Models
{
    public partial class TilausRivi
    {
        public int TilausId { get; set; }
        public int Rivinro { get; set; }
        public int TuoteId { get; set; }
        public int? TilausLkm { get; set; }
        public decimal? Ahinta { get; set; }
        public decimal? Alennus { get; set; }
    }
}

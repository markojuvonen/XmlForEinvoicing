using System;
using System.Collections.Generic;

#nullable disable

namespace XmlForEinvoicing.Models
{
    public partial class Tuote
    {
        public int TuoteId { get; set; }
        public string Nimi { get; set; }
        public string Tyyppi { get; set; }
        public string Tuoteryhmä { get; set; }
        public decimal? Hinta { get; set; }
    }
}

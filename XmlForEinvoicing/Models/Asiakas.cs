using System;
using System.Collections.Generic;

#nullable disable

namespace XmlForEinvoicing.Models
{
    public partial class Asiakas
    {
        public int AsiakasId { get; set; }
        public string Sukunimi { get; set; }
        public string Etunimi { get; set; }
        public string Osoite { get; set; }
        public string Postinumero { get; set; }
        public string Postitoimipaikka { get; set; }
        public DateTime? Syntymäaika { get; set; }
        public int? Luottoraja { get; set; }
    }
}

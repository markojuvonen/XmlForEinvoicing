using System;
using System.Collections.Generic;
using System.Text;

namespace XmlForEinvoicingConsole
{
    class XmlSummary
    {
        //Properties needed for summary
        public string Type { get; set; }
        public string Rate { get; set; }
        public string AccordingSign { get; set; }
        public string AccordingAmount { get; set; }
        public string Description { get; set; }
        public string VATRateTotalSign { get; set; }
        public string VATRateTotal { get; set; }

        public XmlSummary(string type, string rate, string accordingSign, string accordingAmount, string description, string vatRateTotalSign, string vatRateTotal)
        {
            Type = type;
            Rate = rate;
            AccordingSign = accordingSign;
            AccordingAmount = accordingAmount;
            Description = description;
            VATRateTotalSign = vatRateTotalSign;
            VATRateTotal = vatRateTotal;
        }
        public XmlSummary()
        {

        }
    }
}

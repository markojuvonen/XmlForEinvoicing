using System;
using System.Collections.Generic;
using System.Text;

namespace XmlForEinvoicingConsole
{
    class XmlRow
    {
        //Properties needed for each child row 
        public string Id { get; set; }
        public string Number { get; set; }

        public string Articlename { get; set; }

        public string QuantitySign { get; set; }
        public string QuantityUnit { get; set; }
        public string QuantityCharged { get; set; }

        public string PricePerUnitExcludeSign { get; set; }
        public string PricePerUnitExcludeAmount { get; set; }

        public string RowTotalIncludeSign { get; set; }
        public string RowTotalIncludeAmount { get; set; }
        public string RowTotalExcludeSign { get; set; }
        public string RowTotalExcludeAmount { get; set; }

        public string RowAmountExcludeSign { get; set; }
        public string RowAmountExcludeAmount { get; set; }

        public string VATRate { get; set; }
        public string VATSign { get; set; }
        public string VATAmount { get; set; }
        public string FreeText { get; set; }

        public XmlRow(
            string id,
            string number,
            string articleName,
            string quantitySign,
            string quantityUnit,
            string quantityCharged,
            string pricePerUnitExcludeSign,
            string pricePerUnitExcludeAmount,
            string rowTotalIncludeSign,
            string rowTotalIncludeAmount,
            string rowTotalExcludeSign,
            string rowTotalExcludeAmount,
            string rowAmountExcludeSign,
            string rowAmountExcludeAmount,
            string vatRate,
            string vatSign,
            string vatAmount,
            string freeText
            )
        {
            Id = id;
            Number = number;
            Articlename = articleName;
            QuantitySign = quantitySign;
            QuantityUnit = quantityUnit;
            QuantityCharged = quantityCharged;
            PricePerUnitExcludeSign = pricePerUnitExcludeSign;
            PricePerUnitExcludeAmount = pricePerUnitExcludeAmount;
            RowTotalIncludeSign = rowTotalIncludeSign;
            RowTotalIncludeAmount = rowTotalIncludeAmount;
            RowTotalExcludeSign = rowTotalExcludeSign;
            RowTotalExcludeAmount = rowTotalExcludeAmount;
            RowAmountExcludeSign = rowAmountExcludeSign;
            RowAmountExcludeAmount = rowAmountExcludeAmount;
            VATRate = vatRate;
            VATSign = vatSign;
            VATAmount = vatAmount;
            FreeText = freeText;
        }
        public XmlRow()
        {

        }
    }
}

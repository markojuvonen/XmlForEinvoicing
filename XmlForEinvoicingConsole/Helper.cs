using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace XmlForEinvoicingConsole
{
    class Helper
    {
        //Get the last day of month for the master query
        public static DateTime LastDayOfMonth()
        {
            DateTime d = DateTime.Now;
            var firstDay = new DateTime(d.Year, d.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);
            return lastDay;
        }

        //Round spot price to two decimals
        public static string RoundThePrice(string price)
        {
            decimal p = decimal.Parse(price);
            p = Math.Round(p, 2);
            price = p.ToString().Trim();
            return price;
        }

        //Calculate the Finnish reference number
        public static string CalculateReferenceNumber(string refNumberStem)
        {
            string n;
            int sum = 0;
            int multiplier = 7;
            int number = 0;
            for (int i = refNumberStem.Length - 1; i >= 0; i--)
            {
                n = refNumberStem.Substring(i, 1);
                number = int.Parse(n);
                sum += number * multiplier;

                switch (multiplier)
                {
                    case 7:
                        multiplier = 3;
                        break;
                    case 3:
                        multiplier = 1;
                        break;
                    case 1:
                        multiplier = 7;
                        break;
                }
            }
            int checkSum = (((sum / 10) + 1) * 10) - sum;
            return refNumberStem + checkSum.ToString();
        }

        //Check length of org ID and doc number. If org ID is longer than 3 digits and doc num longer than 7 chars, the first number of document number needs to be removed, in order to make sure that the ref number will not be longer than 20 char
        public static string CheckMaxLength(string orgID, string docNumber)
        {
            if (orgID.Length > 3 && docNumber.Length > 7)
            {
                docNumber = docNumber.Substring(1);
            }
            return docNumber;
        }

        //Make sure day in attachment filename date has two characters (01 instead of 1 etc.)
        public static string CheckDay(string day)
        {
            if (day.Length < 2)
            {
                day = day.Insert(0, "0");
            }
            return day;
        }

        //Convert month from number format to letters
        public static string CheckMonth(string month)
        {
            if (month == "01")
            {
                month = "JAN";
            }
            else if (month == "02")
            {
                month = "FEB";
            }
            else if (month == "03")
            {
                month = "MAR";
            }
            else if (month == "04")
            {
                month = "APR";
            }
            else if (month == "05")
            {
                month = "MAY";
            }
            else if (month == "06")
            {
                month = "JUN";
            }
            else if (month == "07")
            {
                month = "JUL";
            }
            else if (month == "08")
            {
                month = "AUG";
            }
            else if (month == "09")
            {
                month = "SEP";
            }
            else if (month == "10")
            {
                month = "OCT";
            }
            else if (month == "11")
            {
                month = "NOV";
            }
            else month = "DEC";

            return month;
        }

        //Check type for attachment filename, add credit invoices
        public static string CheckType(string type)
        {
            if (type.ToUpper() == "ENVA")
            {
                type = "INVOICE_EMISSIONS";
            }
            else if (type.ToUpper() == "POWER")
            {
                type = "POWER_CFD";
            }
            else type = "";

            return type;
        }

        //Format trade date
        public static string FormatTradeDate(string tradeDate)
        {
            tradeDate = tradeDate.Substring(0, 10);
            if (tradeDate.Contains('.'))
            {
                tradeDate = tradeDate.Replace('.', '/');
            }
            if (tradeDate.Contains(' '))
            {
                tradeDate = tradeDate.Split(' ')[0];
            }
            return tradeDate.Trim();
        }

        //Format Buy/Sell
        public static string FormatBuySell(string buySell)
        {
            if (buySell == "B")
            {
                buySell = "BUY";
            }
            else buySell = "SELL";
            return buySell;
        }

        //Check VAT Type
        public static string CheckVATType(string vatRate)
        {
            string vatType = "";
            if (vatRate != "")
            {
                if (vatRate == "24")
                {
                    vatType = "S";
                }
                else if (vatRate == "0")
                {
                    vatType = "E";
                }
            }
            return vatType;
        }

        //Check VAT description, if 0 description is needed, otherwise empty
        public static string CheckVATDescription(string vatRate)
        {
            string description = "";
            if (Convert.ToInt32(vatRate) == 0)
            {
                description = "Value added tax exempted according to EU directive 2006/112/EC 135 Article (F) Point.";
            }
            else description = "";
            return description;
        }

        //Trade price info to free text tag of each row
        public static string TradePrice(string rowFreeText)
        {
            decimal price = decimal.Parse(rowFreeText);
            price = Math.Round(price, 2);
            rowFreeText = $"Trade price / MWh: {price.ToString("0.00").Trim()}";
            return rowFreeText;
        }

        //Fetch the PDF file for the invoice and copy it to same folder with the XML
        public static void FindPDF(string imageFile)
        {
            string folder = imageFile.Replace(".pdf", "");
            string copyPath = $@"C:\Work\{imageFile}";
            string pastePath = $@"C:\Work\EInvoices\{folder}\{imageFile}";
            if (File.Exists(copyPath))
            {
                File.Copy(copyPath, pastePath);
            }
        }

        //Compress the folder containing the XML and PDF into a ZIP 
        public static void ZipTheFolder(string fileName)
        {
            string folder = fileName;
            string sourcePath = Path.GetFullPath($@"C:\Work\EInvoices\{folder}");
            string zipPath = Path.GetFullPath($@"C:Work\EInvoices\{folder}.zip");
            ZipFile.CreateFromDirectory(sourcePath, zipPath);
            //ZipFile.CreateFromDirectory(sourcePath, zipPath);
            File.Move(zipPath, sourcePath + $@"\{folder}.zip\");
        }

        //Move the folder to Archive
        public static void ArchiveFolder(string fileName)
        {
            string folder = fileName;
            string sourcePath = $@"C:\Work\EInvoices\{folder}";
            string destPath = $@"C:\Work\EInvoices\Archive\{folder}";
            if (!Directory.Exists(destPath))
            {
                Directory.Move(sourcePath, destPath);
            }
        }
    }
}

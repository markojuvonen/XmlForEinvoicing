using System;
using System.Collections.Generic;
using System.Text;
using XmlForEinvoicing.Models;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace XmlForEinvoicingConsole
{
    class Xml
    {
        public static MyyntiDBContext db = new MyyntiDBContext();

        List<XmlRow> rowList = new List<XmlRow>();
      
        public void CreateXML()
        {
            //DateTime lastOfMonth = Helper.LastDayOfMonth();
            DateTime lastOfMonth = new DateTime(2014, 12, 31);
            var masterData = db.Asiakas.Join(db.Tilaus, a => a.AsiakasId, t => t.AsiakasId, (a, t) => new { a, t }).Where(b => b.t.ToimitusPvm == lastOfMonth).Distinct().ToList(); 

            foreach (var row in masterData)
            {
                XmlMaster x = new XmlMaster();
                XmlSummary s = new XmlSummary();
                string customerID = row.a.AsiakasId.ToString().Trim();
                string orderID = row.t.TilausId.ToString().Trim();
                string orderDate = row.t.TilausPvm.ToString().Trim();
                string deliveryDate = row.t.ToimitusPvm.ToString().Trim();
                string lastName = row.a.Sukunimi.ToString().Trim();
                string firstName = row.a.Etunimi.ToString().Trim();
                string address = row.a.Osoite.ToString().Trim();
                string postalCode = row.a.Postinumero.ToString().Trim();
                string postOffice = row.a.Postitoimipaikka.ToString().Trim();
                string totalPrice = row.t.Tilaussumma.ToString().Trim();
                string partyID = $"01234567890123456B"; //Bogus as these are not real customers, therefore they don't have party identification id's for e-invoicing
                decimal vat = decimal.Parse(totalPrice) * 0.24m;
                string vatAmount = vat.ToString().Trim();
                decimal exVAT = decimal.Parse(totalPrice) - vat;
                string excludeVAT = exVAT.ToString().Trim();

                string tfCode = "TF01";
                string receiver = "";
                string content = "";
                string sender = "00370010910602900A";
                string intermediator = "";
                string fbRequest = "1";
                string cfCode = "CF01";
                string netServiceId = "00370010910602900A";
                string blockTransType = "00";
                string blockAction = "00";
                string blockFormat = "TEAPPSXML";
                string formatVersion = "3.0";
                string characterSet = "UTF-8";
                string processCode = "00";
                string contractNumber = "";
                string freeText = "Description of product: ";
                string payeeCustomerName = "FUBAR OY";
                string payeeStreetAddress1 = "PO BOX 000";
                string payeeStreetAddress2 = "";
                string payeePostalCode = "00000";
                string payeePostOffice = "";
                string payeeCountry = "FINLAND";
                string payeeCountryCode = "";
                string payeeVATNumber = "";
                string payeeOrganizationNumber = "";
                string payeePartyIdentificationId = "01234567890123456A";
                string payeeContactPerson = "Customer Service";
                string payeeContactTelephoneNumber = "";
                string payeeContactTelefaxNumber = "";
                string payeeEmailAddress = "customerservice@fubar.com";
                string payeeBankName = "";
                string payeeBankAccountNumber = "";
                string payeeSwiftCode = "";
                string payeeIBAN = "";
                string payeeReference = "";
                string payeeNetServiceId = "";
                string payeeIPIReference = "";
                string receiverCustomerName = $"{lastName}, {firstName}";
                string receiverCustomerId = "";
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string invoiceId = orderID;
                string blockId = $"TEAPPSXML{invoiceId}";
                string termsOfPayment = $"14 PÄIVÄÄ NETTO";
                string batchId = orderID;
                string interestRate = "14";
                string currencyCode = "EUR";
                string receiverStreetAddress1 = address;
                string receiverStreetAddress2 = "";
                string receiverCountry = "FINLAND";
                string receiverCountryCode = "FI";
                string receiverVATNumber = "";
                string receiverPartyIdentificationId = partyID;
                string receiverContactPerson = "";
                string receiverContactTelephoneNumber = "";
                string receiverContactTelefaxNumber = "";
                string receiverContactEmailAddress = "";
                string receiverEmailAddress = "";
                string receiverNetServiceId = "";
                string summaryRowsExcludedSign = "+";
                string summaryRowsExcludedAmount = excludeVAT;
                string summaryInvoicesIncludedSign = "+";
                string summaryInvoicesIncludedAmount = totalPrice;
                string summaryInvoicesExcludedSign = "+";
                string summaryInvoicesExcludedAmount = excludeVAT;
                string summaryVATTotalSign = "+";
                string summaryVATTotalAmount = vatAmount;
                freeText += $"Order {orderID}, Delivery Date: {deliveryDate}";
                string vatRate = "24";
                //Build reference number stem
                string refNumberStem = customerID+orderID;
                //Calculate reference number
                string payeeFIReference = Helper.CalculateReferenceNumber(refNumberStem);

                //Billdate, month and year
                string[] billD = deliveryDate.Split('/');
                string billDay = billD[0];
                string billMonth = billD[1];
                string billCentury = "20";
                string billDecadeYear = billD[2].Substring(2, 2).Trim();

                //Duedate, month and year
                DateTime dueDate = row.t.ToimitusPvm.Value.AddDays(14);
                string due = dueDate.ToString().Trim();
                string[] dueD = due.Split('/');
                string dueDay = dueD[0];
                string dueMonth = dueD[1];
                string dueCentury = "20";
                string dueDecadeYear = dueD[2].Substring(2, 2).Trim();

                //Invoice Type, invoice 00 or credit 01
                string invoiceType;
                if (decimal.Parse(summaryInvoicesIncludedAmount) < 0)
                {
                    invoiceType = "01";
                }
                else invoiceType = "00";

                //Subject, invoice or credit
                string subject;
                if (decimal.Parse(summaryInvoicesIncludedAmount) < 0)
                {
                    subject = "Credit";
                }
                else subject = "Invoice";

                var childData = db.Asiakas.Join(db.Tilaus, a => a.AsiakasId, t => t.AsiakasId, (a, t) => new { a, t }).Join(db.TilausRivi, r => r.t.TilausId, tr => tr.TilausId, (r, tr) => new { r, tr }).Join(db.Tuote, u => u.tr.TuoteId, tu => tu.TuoteId, (u, tu) => new { u, tu }).Where(x => x.u.r.t.TilausId.ToString().Trim() == orderID).Distinct().ToList();

                foreach (var cRow in childData)
                {
                    XmlRow childRow = new XmlRow();
                    string rowID = cRow.u.tr.Rivinro.ToString().Trim();
                    string productID = cRow.tu.TuoteId.ToString().Trim();
                    string productType = cRow.tu.Tyyppi.ToString().Trim();
                    string productCategory = cRow.tu.Tuoteryhmä.ToString().Trim();
                    string productName = cRow.tu.Nimi.ToString().Trim();
                    string quantity = cRow.u.tr.TilausLkm.ToString().Trim();
                    string price = cRow.tu.Hinta.ToString().Trim();
                    string discount = cRow.u.tr.Alennus.ToString().Trim();
                    decimal? discounted = cRow.tu.Hinta - cRow.u.tr.Alennus;
                    string rowPrice = discounted.ToString().Trim();
                    //string rowPrice = cRow.u.tr.Ahinta.ToString().Trim();
                    string articleName = $"{productID}, {productType}, {productCategory}, {productName}";
                    string rowFreeText = $"Discount: {discount}, Row Price: {rowPrice}";
                    decimal cVat = decimal.Parse(price) * 0.24m;
                    string cVatAmount = cVat.ToString().Trim();
                    decimal cExVAT = decimal.Parse(price) - cVat;
                    string cExcludeVAT = cExVAT.ToString().Trim();

                    childRow.Id = rowID;
                    childRow.Number = rowID;
                    childRow.QuantityCharged = "";
                    childRow.PricePerUnitExcludeAmount = price;
                    childRow.PricePerUnitExcludeSign = "+";
                    childRow.QuantitySign = "+";
                    childRow.QuantityUnit = quantity;
                    childRow.RowAmountExcludeAmount = cExcludeVAT;
                    childRow.RowAmountExcludeSign = "+";
                    childRow.RowTotalExcludeAmount = cExcludeVAT;
                    childRow.RowTotalExcludeSign = "+";
                    childRow.RowTotalIncludeAmount = rowPrice;
                    childRow.RowTotalIncludeSign = "+";
                    childRow.VATRate = "24";
                    childRow.VATSign = "+";
                    childRow.VATAmount = cVatAmount;
                    childRow.Articlename = articleName;
                    childRow.FreeText = rowFreeText;
                    rowList.Add(childRow);
                }

                string imageFile = $"{orderID}_{lastName}_{timeStamp}.pdf";
                
                //Check if PDF file for the invoice exists and if not, set imageFile to "" because it must be empty in XML if invoice doesn't have PDF attachment
                string pdfPath = $@"c:\work\{imageFile}";
                if (!File.Exists(pdfPath))
                {
                    imageFile = "";
                }

                //Place each index of a row to the right property of Xml object x
                x.TFCode = tfCode;
                x.TFTimeStamp = timeStamp;
                x.BatchId = batchId;
                x.Receiver = receiver;
                x.Content = content;
                x.Sender = sender;
                x.Intermediator = intermediator;
                x.FBRequest = fbRequest;
                x.CFCode = cfCode;
                x.NetServiceId = netServiceId;
                x.BlockId = blockId;//TEAPPSXML+InvoiceID
                x.CFTimeStamp = timeStamp;
                x.BlockTransType = blockTransType;
                x.BlockAction = blockAction;
                x.BlockFormat = blockFormat;
                x.FormatVersion = formatVersion;
                x.CharacterSet = characterSet;
                x.ImageFile = imageFile;
                x.InvoiceId = invoiceId;
                x.ProcessCode = processCode;
                x.InvoiceType = invoiceType;
                x.Subject = subject;
                x.InvoiceDay = billDay;
                x.InvoiceMonth = billMonth;
                x.InvoiceCentury = billCentury;
                x.InvoiceDecadeAndYear = billDecadeYear;
                x.DueDay = dueDay;
                x.DueMonth = dueMonth;
                x.DueCentury = dueCentury;
                x.DueDecadeAndYear = dueDecadeYear;
                x.TermsOfPayment = termsOfPayment;
                x.InterestRate = interestRate;
                x.CurrencyCode = currencyCode;
                x.OrderNumber = orderID;//counterparty Users -> ADDR1
                x.ContractNumber = contractNumber;
                x.FreeText = freeText;
                x.PayeeCustomerName = payeeCustomerName;
                x.PayeeStreetAddress1 = payeeStreetAddress1;
                x.PayeeStreetAddress2 = payeeStreetAddress2;
                x.PayeePostalCode = payeePostalCode; //missing
                x.PayeePostOffice = payeePostOffice;
                x.PayeeCountry = payeeCountry;
                x.PayeeCountryCode = payeeCountryCode;
                x.PayeeVATNumber = payeeVATNumber;
                x.PayeeOrganizationNumber = payeeOrganizationNumber;
                x.PayeePartyIdentificationId = payeePartyIdentificationId;
                x.PayeeContactPerson = payeeContactPerson;
                x.PayeeContactTelephoneNumber = payeeContactTelephoneNumber;
                x.PayeeContactTelefaxNumber = payeeContactTelefaxNumber;
                x.PayeeEmailAddress = payeeEmailAddress;
                x.PayeeBankName = payeeBankName;
                x.PayeeBankAccountNumber = payeeBankAccountNumber; //missing?
                x.PayeeSwiftCode = payeeSwiftCode;
                x.PayeeIBAN = payeeIBAN;
                x.PayeeReference = payeeReference; //missing
                x.PayeeNetServiceId = payeeNetServiceId; //missing?
                x.PayeeFIReference = payeeFIReference;
                x.PayeeIPIReference = payeeIPIReference;
                x.ReceiverCustomerName = receiverCustomerName;
                x.ReceiverCustomerId = receiverCustomerId;
                x.ReceiverStreetAddress1 = receiverStreetAddress1;
                x.ReceiverStreetAddress2 = receiverStreetAddress2;
                x.ReceiverPostalCode = postalCode;
                x.ReceiverPostOffice = postOffice;
                x.ReceiverCountry = receiverCountry;
                x.ReceiverCountryCode = receiverCountryCode;
                x.ReceiverVATNumber = receiverVATNumber;
                x.ReceiverPartyIdentificationId = receiverPartyIdentificationId;
                x.ReceiverContactPerson = receiverContactPerson;
                x.ReceiverContactTelephoneNumber = receiverContactTelephoneNumber;
                x.ReceiverContactTelefaxNumber = receiverContactTelefaxNumber;
                x.ReceiverContactEmailAddress = receiverContactEmailAddress;
                x.ReceiverEmailAddress = receiverEmailAddress;
                x.ReceiverNetServiceId = receiverNetServiceId;
                x.SummaryRowsExcludedSign = summaryRowsExcludedSign;
                x.SummaryRowsExcludedAmount = summaryRowsExcludedAmount;
                x.SummaryInvoicesIncludedSign = summaryInvoicesIncludedSign;
                x.SummaryInvoicesIncludedAmount = summaryInvoicesIncludedAmount;
                x.SummaryInvoicesExcludedSign = summaryInvoicesExcludedSign;
                x.SummaryInvoicesExcludedAmount = summaryInvoicesExcludedAmount;
                x.SummaryVATTotalSign = summaryVATTotalSign;
                x.SummaryVATTotalAmount = summaryVATTotalAmount;

                //Check VAT type based on VAT rate
                string vatType = Helper.CheckVATType(vatRate);

                //Check if VAT description is needed based on VAT rate
                string description = Helper.CheckVATDescription(vatRate);

                //Summary
                s.Type = vatType;
                s.Description = description;
                s.AccordingSign = "+";
                s.AccordingAmount = x.SummaryVATTotalAmount;
                s.Rate = vatRate;
                s.VATRateTotal = vatRate;
                s.VATRateTotalSign = "+";

                XElement Rows;
                XElement Summary;
                //Create the xml and add each variable of a row to right part of XML
                XDocument invoiceXml = new XDocument(new XDeclaration("1.0", "ISO-8859-1", ""),
                new XElement("INVOICE_CENTER",
                new XElement("TRANSPORT_FRAME",
                new XElement("TF_CODE", x.TFCode),
                new XElement("TIMESTAMP", x.TFTimeStamp),
                new XElement("BATCH_ID", x.BatchId),
                new XElement("CONTENT_RECEIVER",
                new XElement("RECEIVER_REF", x.Receiver),
                new XElement("CONTENT_REF", x.Content)),
                new XElement("SENDER", x.Sender),
                new XElement("INTERMEDIATOR", x.Intermediator),
                new XElement("FB_REQUEST", x.FBRequest)),
                new XElement("CONTENT_FRAME",
                new XElement("CF_CODE", x.CFCode),
                new XElement("NET_SERVICE_ID", x.NetServiceId),
                new XElement("BLOCK_ID", x.BlockId),
                new XElement("TIMESTAMP", x.CFTimeStamp),
                new XElement("BLOCK_RULES",
                new XElement("TRANSACTION_TYPE", x.BlockTransType),
                new XElement("BLOCK_ACTION", x.BlockAction),
                new XElement("BLOCK_FORMAT", x.BlockFormat),
                new XElement("FORMAT_VERSION", x.FormatVersion),
                new XElement("CHARACTER_SET", x.CharacterSet)),
                new XElement("INVOICES",
                new XElement("INVOICE",
                new XElement("CONTROL",
                new XElement("IMAGE_CONTROL",
                new XAttribute("TYPE", "INVOICE_IMAGE"),
                new XElement("IMAGE_FILE", x.ImageFile))),
                new XElement("HEADER",
                new XElement("INVOICE_ID", x.InvoiceId),
                new XElement("PROCESS_CODE", x.ProcessCode),
                new XElement("INVOICE_TYPE", x.InvoiceType),
                new XElement("SUBJECT", x.Subject),
                new XElement("INVOICE_DATE",
                new XElement("DATE",
                new XElement("DAY", x.InvoiceDay),
                new XElement("MONTH", x.InvoiceMonth),
                new XElement("CENTURY", x.InvoiceCentury),
                new XElement("DECADE_AND_YEAR", x.InvoiceDecadeAndYear))),
                new XElement("DUE_DATE",
                new XElement("DATE",
                new XElement("DAY", x.DueDay),
                new XElement("MONTH", x.DueMonth),
                new XElement("CENTURY", x.DueCentury),
                new XElement("DECADE_AND_YEAR", x.DueDecadeAndYear))),
                new XElement("TERMS_OF_PAYMENT", x.TermsOfPayment),
                new XElement("PAYMENT_OVERDUE_FINE",
                new XElement("INTEREST_RATE", x.InterestRate)),
                new XElement("CURRENCY",
                new XElement("CODE", x.CurrencyCode)),
                new XElement("ORDER_INFORMATION",
                new XElement("ORDER_NUMBER", x.OrderNumber)),
                new XElement("CONTRACT_INFORMATION",
                new XElement("CONTRACT_NUMBER", x.ContractNumber)),
                new XElement("FREE_TEXT", x.FreeText)),
                new XElement("PAYEE",
                new XElement("CUSTOMER_INFORMATION",
                new XElement("CUSTOMER_NAME", x.PayeeCustomerName),
                new XElement("ADDRESS",
                new XElement("STREET_ADDRESS1", x.PayeeStreetAddress1),
                new XElement("STREET_ADDRESS2", x.PayeeStreetAddress2),
                new XElement("POSTAL_CODE", x.PayeePostalCode),
                new XElement("POST_OFFICE", x.PayeePostOffice),
                new XElement("COUNTRY", x.PayeeCountry),
                new XElement("COUNTRY_CODE", x.PayeeCountryCode)),
                new XElement("VAT_NUMBER", x.PayeeVATNumber),
                new XElement("ORGANIZATION_NUMBER", x.PayeeOrganizationNumber),
                new XElement("PARTY_IDENTIFICATION_ID", x.PayeePartyIdentificationId),
                new XElement("CONTACT_INFORMATION",
                new XElement("CONTACT_PERSON", x.PayeeContactPerson),
                new XElement("TELEPHONE_NUMBER", x.PayeeContactTelephoneNumber),
                new XElement("TELEFAX_NUMBER", x.PayeeContactTelefaxNumber)),
                new XElement("E-MAIL_ADDRESS", x.PayeeEmailAddress)),
                new XElement("BANKS",
                new XElement("BANK_NAME", x.PayeeBankName),
                new XElement("BANK_ACCOUNT_NUMBER", x.PayeeBankAccountNumber),
                new XElement("SWIFT_CODE", x.PayeeSwiftCode),
                new XElement("IBAN_ACCOUNT_NUMBER", x.PayeeIBAN)),
                new XElement("NET_SERVICE_ID", x.PayeeNetServiceId),
                new XElement("PAYEE_REFERENCE", x.PayeeReference),
                new XElement("DETAILS_OF_PAYMENT",
                new XElement("FI_PAYMENT_REFERENCE", x.PayeeFIReference),
                new XElement("IPI_REFERENCE", x.PayeeIPIReference))),
                new XElement("RECEIVER",
                new XElement("CUSTOMER_INFORMATION",
                new XElement("CUSTOMER_NAME", x.ReceiverCustomerName),
                new XElement("CUSTOMER_ID", ""),
                new XElement("ADDRESS",
                new XElement("STREET_ADDRESS1", x.ReceiverStreetAddress1),
                new XElement("STREET_ADDRESS2", x.ReceiverStreetAddress2),
                new XElement("POSTAL_CODE", x.ReceiverPostalCode),
                new XElement("POST_OFFICE", x.ReceiverPostOffice),
                new XElement("COUNTRY", x.ReceiverCountry),
                new XElement("COUNTRY_CODE", x.ReceiverCountryCode)),
                new XElement("VAT_NUMBER", x.ReceiverVATNumber),
                new XElement("PARTY_IDENTIFICATION_ID", x.ReceiverPartyIdentificationId),
                new XElement("CONTACT_INFORMATION",
                new XElement("CONTACT_PERSON", x.ReceiverContactPerson),
                new XElement("TELEPHONE_NUMBER", x.ReceiverContactTelephoneNumber),
                new XElement("TELEFAX_NUMBER", x.ReceiverContactTelefaxNumber),
                new XElement("E-MAIL_ADDRESS", x.ReceiverContactEmailAddress)),
                new XElement("E-MAIL_ADDRESS", x.ReceiverEmailAddress))),              
                Rows = new XElement("ROWS"),
                Summary = new XElement("SUMMARY"))))));
                for (int i = 0; i < rowList.Count; i++)
                {
                    Rows.Add(new XElement("ROW",
                    new XAttribute("ROW_ID", rowList[i].Id),
                    new XElement("ROW_NUMBER", rowList[i].Number),
                    new XElement("ARTICLE",
                    new XElement("ARTICLE_NAME", rowList[i].Articlename)),

                    new XElement("QUANTITY",
                    new XElement("CHARGED",
                    new XAttribute("Q_UNIT", rowList[i].QuantityUnit),
                    new XAttribute("SIGN", rowList[i].QuantitySign), rowList[i].QuantityCharged)),

                    new XElement("PRICE_PER_UNIT",
                    new XElement("AMOUNT",
                    new XAttribute("SIGN", rowList[i].PricePerUnitExcludeSign),
                    new XAttribute("VAT", "EXCLUDED"), rowList[i].PricePerUnitExcludeAmount)),

                    new XElement("ROW_TOTAL",
                    new XElement("AMOUNT",
                    new XAttribute("SIGN", rowList[i].RowTotalIncludeSign),
                    new XAttribute("VAT", "INCLUDED"),
                    new XAttribute("AMOUNT", rowList[i].RowTotalIncludeAmount)),
                    new XElement("AMOUNT",
                    new XAttribute("SIGN", rowList[i].RowTotalExcludeSign),
                    new XAttribute("VAT", "EXCLUDED"),
                    new XAttribute("AMOUNT", rowList[i].RowTotalExcludeAmount))),

                    new XElement("ROW_AMOUNT",
                    new XElement("AMOUNT",
                    new XAttribute("SIGN", rowList[i].RowAmountExcludeSign),
                    new XAttribute("VAT", "EXCLUDED"), rowList[i].RowAmountExcludeAmount)),

                    new XElement("VAT",
                    new XElement("RATE", rowList[i].VATRate),
                    new XElement("AMOUNT",
                    new XAttribute("SIGN", rowList[i].VATSign), rowList[i].VATAmount)),

                    new XElement("FREE_TEXT", rowList[i].FreeText)));
                }
                //Clear rowList so the existing rows for this invoice don't appear on the next invoice
                rowList.Clear();
                Summary.Add(new XElement("ROWS_TOTAL",
                new XElement("AMOUNT",
                new XAttribute("VAT", "EXCLUDED"),
                new XAttribute("SIGN", x.SummaryRowsExcludedSign), x.SummaryRowsExcludedAmount)),
                new XElement("INVOICE_TOTAL",
                new XElement("AMOUNT",
                new XAttribute("SIGN", x.SummaryInvoicesIncludedSign),
                new XAttribute("VAT", "INCLUDED"), x.SummaryInvoicesIncludedAmount),
                new XElement("AMOUNT",
                new XAttribute("SIGN", x.SummaryInvoicesExcludedSign),
                new XAttribute("VAT", "EXCLUDED"), x.SummaryInvoicesExcludedAmount)),
                new XElement("VAT_SUMMARY",
                new XAttribute("VAT_TYPE", s.Type),
                new XElement("RATE", s.Rate),
                new XElement("ACCORDING",
                new XElement("AMOUNT",
                new XAttribute("SIGN", s.AccordingSign), s.AccordingAmount)),
                new XElement("VAT_RATE_TOTAL",
                new XElement("AMOUNT",
                new XAttribute("SIGN", s.VATRateTotalSign), s.VATRateTotal)),
                new XElement("VAT_DESCRIPTION", s.Description)),
                new XElement("VAT_TOTAL",
                new XElement("AMOUNT",
                new XAttribute("SIGN", x.SummaryVATTotalSign), x.SummaryVATTotalAmount)));

                //Name for the created xml file
                string fileName = $"{orderID}_{lastName}_{timeStamp}";

                //Check filename for spaces
                if (fileName.Contains(' '))
                {
                    fileName = fileName.Replace(' ', '_');
                }

                //Folder name based on image file to save the XML into
                string folderName = fileName;

                //Archive path
                string archive = $@"C:Work\EInvoices\Archive\{folderName}";

                if (!Directory.Exists(archive))
                {
                    //Create a folder for the created XML file as both the XML and the PDF have to be in the same folder which is then archived and sent to SFTP server
                    string directory = @"C:\Work\EInvoices";
                    Directory.CreateDirectory($@"{directory}\{folderName}");

                    //Path for the created XML file
                    string xmlPath = $@"{directory}\{folderName}\{fileName}_{timeStamp}.xml";

                    //Save the XML file
                    invoiceXml.Save(xmlPath);

                    //Fetch the PDF file for the invoice and copy it to same folder with the XML
                    Helper.FindPDF(imageFile);

                    //Compress the folder containing the XML and PDF into a ZIP
                    Helper.ZipTheFolder(fileName);

                    //Get host, port, username and password for sending the zip to SFTP server
                    string host = SFTP.SFTPHost();
                    int port = SFTP.SFTPPort();
                    string username = SFTP.SFTPUsername();
                    string password = SFTP.SFTPPassword();

                    //Path for the file to be uploaded
                    string uploadPath = $@"{directory}\{folderName}\{folderName}.zip";

                    //Send the ZIP to server
                    //SFTP.UploadToSFTP(host, port, username, password, uploadPath);

                    //Move the folder to Archive
                    Helper.ArchiveFolder(fileName);
                }
            }
        }
    }   
}

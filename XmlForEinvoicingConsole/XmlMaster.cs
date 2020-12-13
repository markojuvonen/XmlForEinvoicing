using System;
using System.Collections.Generic;
using System.Text;

namespace XmlForEinvoicingConsole
{
    public class XmlMaster
    {
        //Properties needed for the xml file
        public string TFCode { get; set; }

        public string TFTimeStamp { get; set; }
        public string BatchId { get; set; }
        public string Receiver { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Intermediator { get; set; }
        public string FBRequest { get; set; }

        public string CFCode { get; set; }
        public string NetServiceId { get; set; }
        public string BlockId { get; set; }
        public string CFTimeStamp { get; set; }
        public string BlockTransType { get; set; }
        public string BlockAction { get; set; }
        public string BlockFormat { get; set; }
        public string FormatVersion { get; set; }
        public string CharacterSet { get; set; }

        public string ImageFile { get; set; }

        public string InvoiceId { get; set; }
        public string ProcessCode { get; set; }
        public string InvoiceType { get; set; }
        public string Subject { get; set; }

        public string InvoiceDay { get; set; }
        public string InvoiceMonth { get; set; }
        public string InvoiceCentury { get; set; }
        public string InvoiceDecadeAndYear { get; set; }

        public string DueDay { get; set; }
        public string DueMonth { get; set; }
        public string DueCentury { get; set; }
        public string DueDecadeAndYear { get; set; }

        public string TermsOfPayment { get; set; }
        public string InterestRate { get; set; }
        public string CurrencyCode { get; set; }
        public string OrderNumber { get; set; }
        public string ContractNumber { get; set; }
        public string FreeText { get; set; }

        public string PayeeCustomerName { get; set; }
        public string PayeeStreetAddress1 { get; set; }
        public string PayeeStreetAddress2 { get; set; }
        public string PayeePostalCode { get; set; }
        public string PayeePostOffice { get; set; }
        public string PayeeCountry { get; set; }
        public string PayeeCountryCode { get; set; }

        public string PayeeVATNumber { get; set; }
        public string PayeeOrganizationNumber { get; set; }
        public string PayeePartyIdentificationId { get; set; }

        public string PayeeContactPerson { get; set; }
        public string PayeeContactTelephoneNumber { get; set; }
        public string PayeeContactTelefaxNumber { get; set; }
        public string PayeeEmailAddress { get; set; }

        public string PayeeBankName { get; set; }
        public string PayeeBankAccountNumber { get; set; }
        public string PayeeSwiftCode { get; set; }
        public string PayeeIBAN { get; set; }

        public string PayeeReference { get; set; }
        public string PayeeNetServiceId { get; set; }
        public string PayeeFIReference { get; set; }
        public string PayeeIPIReference { get; set; }

        public string ReceiverCustomerName { get; set; }
        public string ReceiverCustomerId { get; set; }
        public string ReceiverStreetAddress1 { get; set; }
        public string ReceiverStreetAddress2 { get; set; }
        public string ReceiverPostalCode { get; set; }
        public string ReceiverPostOffice { get; set; }
        public string ReceiverCountry { get; set; }
        public string ReceiverCountryCode { get; set; }

        public string ReceiverVATNumber { get; set; }
        public string ReceiverPartyIdentificationId { get; set; }

        public string ReceiverContactPerson { get; set; }
        public string ReceiverContactTelephoneNumber { get; set; }
        public string ReceiverContactTelefaxNumber { get; set; }
        public string ReceiverContactEmailAddress { get; set; }
        public string ReceiverEmailAddress { get; set; }
        public string ReceiverNetServiceId { get; set; }

        public string SummaryRowsExcludedSign { get; set; }
        public string SummaryRowsExcludedAmount { get; set; }

        public string SummaryInvoicesIncludedSign { get; set; }
        public string SummaryInvoicesIncludedAmount { get; set; }

        public string SummaryInvoicesExcludedSign { get; set; }
        public string SummaryInvoicesExcludedAmount { get; set; }

        public string SummaryVATTotalSign { get; set; }
        public string SummaryVATTotalAmount { get; set; }

        //A constructor which takes the same amount of strings as parameters as there are properties
        public XmlMaster(
            string tfCode,
            string tfTimestamp,
            string batchID,
            string receiver,
            string content,
            string sender,
            string intermediator,
            string fbRequest,
            string cfCode,
            string netServiceId,
            string blockId,
            string cfTimeStamp,
            string blockTransType,
            string blockAction,
            string blockFormat,
            string formatVersion,
            string charSet,
            string imageFile,
            string invoiceID,
            string processCode,
            string invoiceType,
            string subject,
            string invoiceDay,
            string invoiceMonth,
            string invoiceCentury,
            string invoiceDecadeAndYear,
            string dueDay,
            string dueMonth,
            string dueCentury,
            string dueDecadeAndYear,
            string termsOfPayment,
            string interestRate,
            string currencyCode,
            string orderNumber,
            string contractNumber,
            string freeText,
            string payeeCustomerName,
            string payeeStreetAddress1,
            string payeeStreetAddress2,
            string payeePostalCode,
            string payeePostOffice,
            string payeeCountry,
            string payeeCountryCode,
            string payeeVATNumber,
            string payeeOrganizationNumber,
            string payeePartyIdentificationId,
            string payeeContactPerson,
            string payeeContactTelephoneNumber,
            string payeeContactTelefaxNumber,
            string payeeEmailAddress,
            string payeeBankName,
            string payeeBankAccountNumber,
            string payeeSwiftCode,
            string payeeIBAN,
            string payeeReference,
            string payeeNetServiceId,
            string payeeFIReference,
            string payeeIPIReference,
            string receiverCustomerName,
            string receiverCustomerId,
            string receiverStreetAddress1,
            string receiverStreetAddress2,
            string receiverPostalCode,
            string receiverPostOffice,
            string receiverCountry,
            string receiverCountryCode,
            string receiverVATNumber,
            string receiverPartyIdentificationId,
            string receiverContactPerson,
            string receiverContactTelephoneNumber,
            string receiverContactTelefaxNumber,
            string receiverContactEmailAddress,
            string receiverEmailAddress,
            string receiverNetServiceId,
            string summaryRowsExcludedSign,
            string summaryRowsExcludedAmount,
            string summaryInvoicesIncludedSign,
            string summaryInvoicesIncludedAmount,
            string summaryInvoicesExcludedSign,
            string summaryInvoicesExcludedAmount,
            string summaryVATTotalSign,
            string summaryVATTotalAmount
            )
        {
            //Each parameter is mapped to corresponding property
            TFCode = tfCode;
            TFTimeStamp = tfTimestamp;
            BatchId = batchID;
            Receiver = receiver;
            Content = content;
            Sender = sender;
            Intermediator = intermediator;
            FBRequest = fbRequest;
            CFCode = cfCode;
            NetServiceId = netServiceId;
            BlockId = blockId;
            CFTimeStamp = cfTimeStamp;
            BlockTransType = blockTransType;
            BlockAction = blockAction;
            BlockFormat = blockFormat;
            FormatVersion = formatVersion;
            CharacterSet = charSet;
            ImageFile = imageFile;
            InvoiceId = invoiceID;
            ProcessCode = processCode;
            InvoiceType = invoiceType;
            Subject = subject;
            InvoiceDay = invoiceDay;
            InvoiceMonth = invoiceMonth;
            InvoiceCentury = invoiceCentury;
            InvoiceDecadeAndYear = invoiceDecadeAndYear;
            DueDay = dueDay;
            DueMonth = dueMonth;
            DueCentury = dueCentury;
            DueDecadeAndYear = dueDecadeAndYear;
            TermsOfPayment = termsOfPayment;
            InterestRate = interestRate;
            CurrencyCode = currencyCode;
            OrderNumber = orderNumber;
            ContractNumber = contractNumber;
            FreeText = freeText;
            PayeeCustomerName = payeeCustomerName;
            PayeeStreetAddress1 = payeeStreetAddress1;
            PayeeStreetAddress2 = payeeStreetAddress2;
            PayeePostalCode = payeePostalCode;
            PayeePostOffice = payeePostOffice;
            PayeeCountry = payeeCountry;
            PayeeCountryCode = payeeCountryCode;
            PayeeVATNumber = payeeVATNumber;
            PayeeOrganizationNumber = payeeOrganizationNumber;
            PayeePartyIdentificationId = payeePartyIdentificationId;
            PayeeContactPerson = payeeContactPerson;
            PayeeContactTelephoneNumber = payeeContactTelephoneNumber;
            PayeeContactTelefaxNumber = payeeContactTelefaxNumber;
            PayeeEmailAddress = payeeEmailAddress;
            PayeeBankName = payeeBankName;
            PayeeBankAccountNumber = payeeBankAccountNumber;
            PayeeSwiftCode = payeeSwiftCode;
            PayeeIBAN = payeeIBAN;
            PayeeReference = payeeReference;
            PayeeNetServiceId = payeeNetServiceId;
            PayeeFIReference = payeeFIReference;
            PayeeIPIReference = payeeIPIReference;
            ReceiverCustomerName = receiverCustomerName;
            ReceiverCustomerId = receiverCustomerId;
            ReceiverStreetAddress1 = receiverStreetAddress1;
            ReceiverStreetAddress2 = receiverStreetAddress2;
            ReceiverPostalCode = receiverPostalCode;
            ReceiverPostOffice = receiverPostOffice;
            ReceiverCountry = receiverCountry;
            ReceiverCountryCode = receiverCountryCode;
            ReceiverVATNumber = receiverVATNumber;
            ReceiverPartyIdentificationId = receiverPartyIdentificationId;
            ReceiverContactPerson = receiverContactPerson;
            ReceiverContactTelephoneNumber = receiverContactTelephoneNumber;
            ReceiverContactTelefaxNumber = receiverContactTelefaxNumber;
            ReceiverContactEmailAddress = receiverContactEmailAddress;
            ReceiverEmailAddress = receiverEmailAddress;
            ReceiverNetServiceId = receiverNetServiceId;
            SummaryRowsExcludedSign = summaryRowsExcludedSign;
            SummaryRowsExcludedAmount = summaryRowsExcludedAmount;
            SummaryInvoicesIncludedSign = summaryInvoicesIncludedSign;
            SummaryInvoicesIncludedAmount = summaryInvoicesIncludedAmount;
            SummaryInvoicesExcludedSign = summaryInvoicesExcludedSign;
            SummaryInvoicesExcludedAmount = summaryInvoicesExcludedAmount;
            SummaryVATTotalSign = summaryVATTotalSign;
            SummaryVATTotalAmount = summaryVATTotalAmount;
        }

        //An empty constructor
        public XmlMaster()
        {

        }
    }
}

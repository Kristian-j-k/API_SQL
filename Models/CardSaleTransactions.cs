using System.ComponentModel.DataAnnotations;


namespace API_SQL.Models
{
    public class CardSaleTransactions 
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double InvoiceUnitPrice { get; set; }
        public double Quantity { get; set; }
        public double ServerSubTotal { get; set; }
        public string ServerTimestamp { get; set; }
        public double ServerUnitPrice { get; set; }
        public string SiteName { get; set; }
        public double SiteNo { get; set; }
        public string TerminalTimestamp { get; set; }
        public double Latitude  { get; set; }
        public double Longitude { get; set; }
        public string BiTimestamp { get; set; }
        public int CompanyTraceNo { get; set; }

        public CardSaleTransactions(int id, string productName, double invoiceUnitPrice, double quantity, double serverSubTotal, string serverTimestamp, double serverUnitPrice, string siteName, double siteNo, string terminalTimestamp, double latitude, double longitude, string biTimestamp, int companyTraceNo)
        {
            Id = id;
            ProductName = productName;
            InvoiceUnitPrice = invoiceUnitPrice;
            Quantity = quantity;
            ServerSubTotal = serverSubTotal;
            ServerTimestamp = serverTimestamp;
            ServerUnitPrice = serverUnitPrice;
            SiteName = siteName;
            SiteNo = siteNo;
            TerminalTimestamp = terminalTimestamp;
            Latitude = latitude;
            Longitude = longitude;
            BiTimestamp = biTimestamp;
            CompanyTraceNo = companyTraceNo;
        }

        public CardSaleTransactions()
        {
        }




    }
}

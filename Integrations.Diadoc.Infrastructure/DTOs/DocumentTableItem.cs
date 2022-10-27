using VatRate =  Diadoc.Api.DataXml.Utd820.Hyphens.TaxRateWithTwentyPercentAndTaxedByAgent;

namespace Integrations.Diadoc.Infrastructure.DTOs
{
    public class DocumentTableItem
    {
        public string? TypeName { get; set; }
        public string? TypeNamePrint { get; set; }
        public string? Measure { get; set; }
        public int Quantity { get; set; }
        public VatRate VatRate { get; set; }
        public decimal Tariff { get; set; }
        public decimal Nds { get; set; }
        public decimal Total { get; set; }
        public int IntValue { get; set; }
        public int IntValue1 { get; set; }
        public int StId { get; set; }
        public int StOwnerId { get; set; }
        public int HasInvoice { get; set; } 
        
        public string? Product { get; set; }
    }
}
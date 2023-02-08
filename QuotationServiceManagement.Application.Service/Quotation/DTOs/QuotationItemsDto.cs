namespace QuotationServiceManagement.Application.Service.Quotation.DTOs
{
    public record QuotationItemsDto
    {
        public string Name { get; init; }

        public string Specification { get; init; }

        public string TechnologicalStandard { get; init; }

        public double UnitPrice { get; init; }

        public int Quatity { get; init; }

        public string Remark { get; init; }

        public double TotalPrice { get; set; }
    }

    public record QuotationEnterpriseDescription
    {
        public static readonly string Title = "";

        public static readonly string Address = "";
    }
}
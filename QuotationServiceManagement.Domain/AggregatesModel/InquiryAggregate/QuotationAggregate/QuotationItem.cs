namespace QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate
{
    public class QuotationItem : Entity
    {
        public string Name { get; set; }

        public string Specification { get; set; }

        public string TechnologicalStandard { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice => UnitPrice * Quantity;

        public string Remark { get; set; }

        private QuotationItem()
        { }

        public QuotationItem(string name, string specification, string technologicalStandard, double unitPrice, int quantity, string remark)
            : this()
        {
            Name = name;
            Specification = specification;
            TechnologicalStandard = technologicalStandard;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Remark = remark;
        }
    }
}
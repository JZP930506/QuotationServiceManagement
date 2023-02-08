using QuotationServiceManagement.Domain.Interface;

namespace QuotationServiceManagement.Domain.AggregatesModel.ContractAggregate
{
    public class Contract : Entity, IHasGcid, IAggregateRoot
    {
        public string Gcid { get; }

        public string ContractNo { get; private set; }

        public DateTime CreateTime { get; private set; }

        public DateTime DeliveryTime { get; private set; }

        public int QuotationId { get; private set; }

        public EnterpriseParty FirstParty { get; set; }

        public string Description { get; set; }

        public double UnitPrice { get; set; }

        public int TotalData { get; set; }

        private readonly List<ContractItem> _contractItems;

        public IReadOnlyCollection<ContractItem> ContractItems => _contractItems;

        public Contract()
        {
            Gcid = Guid.NewGuid().ToString();
            _contractItems = new List<ContractItem>();
        }

        public void InitContract(DateTime creatTime, int quotationId, string description, int totalData)
        {
            ContractNo = $"{creatTime:yyyyMMddHHmmss}";
            QuotationId = quotationId;
            CreateTime = creatTime;
            Description = description;
            TotalData = totalData;
        }

        public void SetDeliveryTime(TimeSpan delayTime)
        {
            var deliveryTime = CreateTime.Add(delayTime);
            if (deliveryTime <= CreateTime)
            {
                throw new ArgumentException($"{nameof(DeliveryTime)} Must Be Great Than {nameof(CreateTime)}.");
            }
        }

        public void AddContractItem(string name, string specification, string technologicalStandard, double unitPrice, int quantity, string remark)
        {
            var addItem = _contractItems.Find(t => t.Name == name);

            if (addItem is null)
            {
                _contractItems.Add(new ContractItem(name, specification, technologicalStandard, unitPrice, quantity, remark));
            }
        }
    }
}
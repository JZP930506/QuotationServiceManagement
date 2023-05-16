using QuotationServiceManagement.Domain.Event;
using QuotationServiceManagement.Domain.Interface;
using QuotationServiceManagement.Domain.Share.Enums;
using QuotationServiceManagement.Domain.Share.Expections;

namespace QuotationServiceManagement.Domain.AggregatesModel.InquiryAggregate.QuotationAggregate
{
    public class Quotation : Entity, IHasGcid, IAggregateRoot
    {
        public string Gcid { get; private set; }

        public int InquiryPartyId { get; private set; }

        public DateTime QuotationDateTime { get; private set; }

        public DateTime CreateTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public QuotationStatus QuotationStatus { get; set; }

        public int QuotationCount { get; set; }

        public string Description { get; set; }

        public int TotalData { get; set; }

        private readonly List<QuotationItem> _quotationItems;

        public IReadOnlyCollection<QuotationItem> QuotationItems => _quotationItems;

        public Quotation()
        {
            Gcid = Guid.NewGuid().ToString();
            _quotationItems = new List<QuotationItem>();
        }

        public Quotation SetInquiryParty(int inquiryPartyId)
        {
            InquiryPartyId = inquiryPartyId;
            return this;
        }

        public void InitQuotation(DateTime quotationDateTime, string description, int totalData)
        {
            Description = description;
            TotalData = totalData;
            QuotationDateTime = quotationDateTime;
            QuotationCount = 1;
            QuotationStatus = QuotationStatus.Create;
            CreateTime = DateTime.Now;
            EndTime = CreateTime.AddMonths(3);
        }

        public void ChangeQuotation(DateTime quotationDateTime, DateTime endTime)
        {
            if (endTime < CreateTime)
                throw new BusinessDomainException($"{nameof(endTime)} must be great than {nameof(CreateTime)}");
            QuotationDateTime = quotationDateTime;
            QuotationCount++;
            EndTime = endTime;
            AddDomainEvent(new QuotationChangeDomainEvent(Id, InquiryPartyId));
        }

        public void FinishQuotation(int quotationId, int inquiryPartyId, DateTime submitTime)
        {
            QuotationStatus = QuotationStatus.Finished;

            AddDomainEvent(new QuotationFinishDomainEvent(quotationId, inquiryPartyId,QuotationCount, submitTime));
        }

        public void ChangeQuotationItem(string name, string specification, string technologicalStandard, double unitPrice, int quatity, string remark)
        {
            RemoveQuotationItem(name);
            AddQuotationItem(name, specification, technologicalStandard, unitPrice, quatity, remark);
        }

        private void RemoveQuotationItem(string name)
        {
            var removeItem = _quotationItems.Find(t => t.Name == name);

            if (removeItem is not null)
            {
                _quotationItems.Remove(removeItem);
            }
        }

        public void AddQuotationItem(string name, string specification, string technologicalStandard, double unitPrice, int quatity, string remark)
        {
            var addItem = _quotationItems.Find(t => t.Name == name);

            if (addItem is null)
            {
                _quotationItems.Add(new QuotationItem(name, specification, technologicalStandard, unitPrice, quatity, remark));
            }
        }
    }
}
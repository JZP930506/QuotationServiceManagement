using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace QuotationServiceManagement.Domain.Share.Models
{
    public abstract class Entity
    {
        protected List<INotification> _domainEvents;

        protected Entity()
        {
            _domainEvents = new List<INotification>();
        }

        protected Entity(int id) : this()
        {
            Id = id;
        }

        public int Id { get; protected set; }

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        protected void AddDomainEvent(INotification eventItem)
        {
            if (eventItem == null)
                return;

            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            if (eventItem == null)
                return;

            _domainEvents.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity)obj;

            return item.Id == Id;
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ 31;
        }
    }
}
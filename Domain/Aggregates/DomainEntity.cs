using Domain.Events;

namespace Domain.Aggregates {
    public abstract class DomainEntity : Entity, IDomainEventEmitter {

        private List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        public DomainEntity () { }

        public void AddDomainEvent (IDomainEvent domainEvent) {
            lock (_domainEvents) {
                _domainEvents.Add(domainEvent);
            }
        }

        public void AddUniqueDomainEvent (IDomainEvent domainEvent) {
            lock (_domainEvents) {
                _domainEvents.RemoveAll(x => {
                    return x.GetType() == domainEvent.GetType();
                });
                _domainEvents.Add(domainEvent);
            }
        }

        public void RemoveAllDomainEvents () {
            lock (_domainEvents) {
                _domainEvents.Clear();
            }
        }

        public void RemoveDomainEvent (IDomainEvent domainEvent) {
            lock (_domainEvents) {
                _domainEvents.Remove(domainEvent);
            }
        }

    }
}

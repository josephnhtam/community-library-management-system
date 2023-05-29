namespace Domain.Events {
    public interface IDomainEventEmitter {
        void AddDomainEvent (IDomainEvent domainEvent);
        void AddUniqueDomainEvent (IDomainEvent domainEvent);
        void RemoveDomainEvent (IDomainEvent domainEvent);
        void RemoveAllDomainEvents ();
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }
}

using Domain.Events;

namespace CLMS.Domain.Aggregates.PatronAggregate.Events {
    public class BookCopyDonatedDomainEvent : IDomainEvent {
        public BookDonation Donation { get; private set; }

        public BookCopyDonatedDomainEvent (BookDonation donation) {
            Donation = donation;
        }
    }
}

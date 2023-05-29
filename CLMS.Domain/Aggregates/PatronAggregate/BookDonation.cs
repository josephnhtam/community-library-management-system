using CLMS.Domain.Aggregates.PatronAggregate.Events;
using Domain.Aggregates;

namespace CLMS.Domain.Aggregates.PatronAggregate {
    public class BookDonation : DomainEntity {

        public int Version { get; private set; } = default!;
        public Guid Id { get; private set; } = default!;
        public Patron Patron { get; private set; } = default!;
        public Guid BookId { get; private set; } = default!;
        public Guid BookCopyId { get; private set; } = default!;
        public DateTimeOffset Date { get; private set; } = default!;

        private BookDonation () { }

        internal BookDonation (Patron patron, Guid bookId, Guid bookCopyId, DateTimeOffset date) {
            Id = Guid.NewGuid();
            Patron = patron;
            BookId = bookId;
            BookCopyId = bookCopyId;
            Date = date;

            AddDomainEvent(new BookCopyDonatedDomainEvent(this));
        }

        public override object GetId () {
            return Id;
        }

    }
}

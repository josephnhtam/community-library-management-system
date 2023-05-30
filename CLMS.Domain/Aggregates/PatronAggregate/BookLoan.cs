using CLMS.Domain.Aggregates.PatronAggregate.Events;
using Domain.Aggregates;

namespace CLMS.Domain.Aggregates.PatronAggregate {
    public class BookLoan : DomainEntity {

        public int Version { get; private set; } = default!;
        public Guid Id { get; private set; } = default!;
        public Patron Patron { get; private set; } = default!;
        public Guid BookCopyId { get; private set; } = default!;
        public DateTimeOffset BorrowDate { get; private set; } = default!;
        public DateTimeOffset DueDate { get; private set; } = default!;
        public DateTimeOffset? ReturnDate { get; private set; } = null;

        private BookLoan () { }

        internal BookLoan (Patron patron, Guid bookCopyId, DateTimeOffset borrowDate, DateTimeOffset dueDate) {
            Id = Guid.NewGuid();
            Patron = patron;
            BookCopyId = bookCopyId;
            BorrowDate = borrowDate;
            DueDate = dueDate;

            AddDomainEvent(new BookCopyBorrowedDomainEvent(this));
        }

        internal void SetReturnDate (DateTimeOffset returnDate) {
            ReturnDate = returnDate;
            Version++;

            AddDomainEvent(new BookCopyReturnedDomainEvent(this));
        }

        public override object GetId () {
            return Id;
        }

    }
}

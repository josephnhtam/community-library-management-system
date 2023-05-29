using Domain.Events;

namespace CLMS.Domain.Aggregates.PatronAggregate.Events {
    public class BookCopyBorrowedDomainEvent : IDomainEvent {
        public BookLoan Loan { get; private set; }

        public BookCopyBorrowedDomainEvent (BookLoan loan) {
            Loan = loan;
        }
    }
}

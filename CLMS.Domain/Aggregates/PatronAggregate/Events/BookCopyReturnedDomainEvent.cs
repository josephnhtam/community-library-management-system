using Domain.Events;

namespace CLMS.Domain.Aggregates.PatronAggregate.Events {
    public class BookCopyReturnedDomainEvent : IDomainEvent {
        public BookLoan Loan { get; private set; }

        public BookCopyReturnedDomainEvent (BookLoan loan) {
            Loan = loan;
        }
    }
}

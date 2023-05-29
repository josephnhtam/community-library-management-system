using Application.Contracts;
using CLMS.Domain.Aggregates.PatronAggregate;

namespace CLMS.Application.Commands.Patrons {
    public class ReturnBookCommand : ICommand<BookLoan> {
        public Guid PatronId { get; init; }
        public Guid BookLoanId { get; init; }
        public DateTimeOffset Date { get; init; }

        public ReturnBookCommand (Guid patronId, Guid bookLoanId, DateTimeOffset date) {
            PatronId = patronId;
            BookLoanId = bookLoanId;
            Date = date;
        }
    }
}

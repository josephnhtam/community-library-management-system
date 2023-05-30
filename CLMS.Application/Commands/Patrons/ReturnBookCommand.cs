using Application.Contracts;
using CLMS.Domain.Aggregates.PatronAggregate;

namespace CLMS.Application.Commands.Patrons {
    public class ReturnBookCommand : ICommand<BookLoan> {
        public Guid PatronId { get; init; } = default!;
        public Guid BookLoanId { get; init; } = default!;
        public DateTimeOffset Date { get; init; } = default!;
    }
}

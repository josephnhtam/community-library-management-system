using Application.Contracts;
using CLMS.Domain.Aggregates.PatronAggregate;

namespace CLMS.Application.Commands.Patrons {
    public class DonateBookCommand : ICommand<BookDonation> {
        public Guid PatronId { get; init; } = default!;
        public Guid BookId { get; init; } = default!;
        public DateTimeOffset Date { get; init; } = default!;
    }
}

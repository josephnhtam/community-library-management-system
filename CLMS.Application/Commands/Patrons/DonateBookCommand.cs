using Application.Contracts;
using CLMS.Domain.Aggregates.PatronAggregate;

namespace CLMS.Application.Commands.Patrons {
    public class DonateBookCommand : ICommand<BookDonation> {
        public Guid PatronId { get; init; }
        public Guid BookId { get; init; }
        public DateTimeOffset Date { get; init; }

        public DonateBookCommand (Guid patronId, Guid bookId, DateTimeOffset date) {
            PatronId = patronId;
            BookId = bookId;
            Date = date;
        }
    }
}

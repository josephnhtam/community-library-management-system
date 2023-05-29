using Application.Contracts;
using CLMS.Domain.Aggregates.PatronAggregate;

namespace CLMS.Application.Commands.Patrons {
    public class BorrowBookCommand : ICommand<BookLoan> {
        public Guid PatronId { get; init; }
        public Guid BookCopyId { get; init; }
        public DateTimeOffset Date { get; init; }
        public DateTimeOffset DueDate { get; init; }

        public BorrowBookCommand (Guid patronId, Guid bookCopyId, DateTimeOffset date, DateTimeOffset dueDate) {
            PatronId = patronId;
            BookCopyId = bookCopyId;
            Date = date;
            DueDate = dueDate;
        }
    }
}

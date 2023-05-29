using Application.Contracts;
using CLMS.Domain.Aggregates.BookAggregate;

namespace CLMS.Application.Queris.Books {
    public class GetBookQuery : IQuery<Book?> {
        public Guid BookId { get; init; }

        public GetBookQuery (Guid bookId) {
            BookId = bookId;
        }
    }
}

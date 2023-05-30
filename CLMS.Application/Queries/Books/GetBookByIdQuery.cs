using Application.Contracts;
using CLMS.Domain.Aggregates.BookAggregate;

namespace CLMS.Application.Queries.Books {
    public class GetBookByIdQuery : IQuery<Book?> {
        public Guid BookId { get; init; } = default!;
    }
}

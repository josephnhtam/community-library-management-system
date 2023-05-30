using Application.Contracts;
using CLMS.Domain.Aggregates.BookAggregate;

namespace CLMS.Application.Queries.Books {
    public class GetBookByCopyIdQuery : IQuery<Book?> {
        public Guid BookCopyId { get; init; } = default!;
    }
}

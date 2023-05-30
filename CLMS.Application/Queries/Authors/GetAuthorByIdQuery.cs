using Application.Contracts;
using CLMS.Domain.Aggregates.AuthorAggregate;

namespace CLMS.Application.Queries.Authors {
    public class GetAuthorByIdQuery : IQuery<Author?> {
        public Guid AuthorId { get; init; } = default!;
    }
}

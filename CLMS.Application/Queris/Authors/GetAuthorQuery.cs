using Application.Contracts;
using CLMS.Domain.Aggregates.AuthorAggregate;

namespace CLMS.Application.Queris.Authors {
    public class GetAuthorQuery : IQuery<Author?> {
        public Guid AuthorId { get; init; }

        public GetAuthorQuery (Guid authorId) {
            AuthorId = authorId;
        }
    }
}

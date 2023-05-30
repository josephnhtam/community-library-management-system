using Application.Contracts;
using CLMS.Domain.Aggregates.PatronAggregate;

namespace CLMS.Application.Queries.Patrons {
    public class GetPatronByIdQuery : IQuery<Patron?> {
        public Guid PatronId { get; init; } = default!;
    }
}

using Application.Contracts;
using CLMS.Domain.Aggregates.AuthorAggregate;

namespace CLMS.Application.Commands.Authors {
    public class AddAuthorCommand : ICommand<Author> {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
    }
}

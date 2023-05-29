using Application.Contracts;
using CLMS.Domain.Aggregates.AuthorAggregate;

namespace CLMS.Application.Commands.Authors {
    public class CreateAuthorCommand : ICommand<Author> {
        public string Name { get; init; }
        public string Description { get; init; }

        public CreateAuthorCommand (string name, string description) {
            Name = name;
            Description = description;
        }
    }
}

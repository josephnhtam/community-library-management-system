using Application.Contracts;
using CLMS.Domain.Aggregates.AuthorAggregate;

namespace CLMS.Application.Commands.Books {
    public class AddBookCommand : ICommand<Author> {
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTimeOffset PublicationDate { get; init; }
        public List<Guid> Authors { get; init; }

        public AddBookCommand (string title, string description, DateTimeOffset publicationDate, List<Guid> authors) {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            Authors = authors;
        }
    }
}

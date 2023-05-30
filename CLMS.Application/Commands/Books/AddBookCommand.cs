using Application.Contracts;
using CLMS.Domain.Aggregates.BookAggregate;

namespace CLMS.Application.Commands.Books {
    public class AddBookCommand : ICommand<Book> {
        public string Title { get; init; } = default!;
        public string Description { get; init; } = default!;
        public DateTimeOffset PublicationDate { get; init; } = default!;
        public List<Guid> Authors { get; init; } = default!;
    }
}

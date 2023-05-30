using CLMS.Domain.Aggregates.BookAggregate;

namespace CLMS.API.DtoModels.Books {
    public class BookDto {
        public Guid Id { get; private set; } = default!;
        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public DateTimeOffset PublicationDate { get; private set; } = default!;
        public int TotalNumberOfCopies { get; private set; } = 0;
        public int AvailableNumberOfCopies { get; private set; } = 0;

        public IReadOnlyList<BookAuthor> Authors { get; private set; } = default!;
        public IReadOnlyList<BookCopy> Copies { get; private set; } = default!;
    }
}

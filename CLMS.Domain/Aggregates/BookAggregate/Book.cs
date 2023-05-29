using Domain.Aggregates;

namespace CLMS.Domain.Aggregates.BookAggregate {
    public class Book : DomainEntity, IAggregateRoot {

        private List<BookAuthor> _authors = new();
        private List<BookCopy> _copies = new();

        public int Version { get; private set; } = default!;
        public Guid Id { get; private set; } = default!;
        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public DateTimeOffset PublicationDate { get; private set; } = default!;
        public int TotalNumberOfCopies { get; private set; } = 0;
        public int AvailableNumberOfCopies { get; private set; } = 0;

        public IReadOnlyList<BookAuthor> Authors => _authors;
        public IReadOnlyList<BookCopy> Copies => _copies;

        private Book () { }

        public Book (string title, string description, DateTimeOffset publicationDate, IReadOnlyList<Guid> authors) {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            _authors = authors.Select(x => new BookAuthor(this, x)).ToList();
        }

        public BookCopy AddCopy (Guid bookCopyId, Guid patronId) {
            var bookCopy = new BookCopy(this, bookCopyId, patronId);
            _copies.Add(bookCopy);
            TotalNumberOfCopies++;
            AvailableNumberOfCopies++;
            Version++;
            return bookCopy;
        }

        internal void ChangeAvailableNumberOfCopies (int delta) {
            AvailableNumberOfCopies += delta;
            Version++;
        }

        public override object GetId () {
            return Id;
        }

    }
}

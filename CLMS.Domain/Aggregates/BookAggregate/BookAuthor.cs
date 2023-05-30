using Domain.Aggregates;

namespace CLMS.Domain.Aggregates.BookAggregate {
    public class BookAuthor : DomainEntity {
        public int Id { get; set; } = default!;
        public Guid BookId { get; private set; } = default!;
        public Guid AuthorId { get; private set; } = default!;

        private BookAuthor () { }

        public BookAuthor (Guid bookId, Guid authorId) {
            BookId = bookId;
            AuthorId = authorId;
        }

        public override object GetId () {
            return Id;
        }
    }
}

using Domain.Aggregates;

namespace CLMS.Domain.Aggregates.BookAggregate {
    public class BookAuthor : DomainEntity {
        public int Id { get; set; }
        public Book Book { get; private set; }
        public Guid AuthorId { get; private set; }

        private BookAuthor () { }

        public BookAuthor (Book book, Guid authorId) {
            Book = book;
            AuthorId = authorId;
        }

        public override object GetId () {
            return Id;
        }
    }
}

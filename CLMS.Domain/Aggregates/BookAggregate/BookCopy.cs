using Domain.Aggregates;

namespace CLMS.Domain.Aggregates.BookAggregate {
    public class BookCopy : DomainEntity {

        public int Version { get; private set; } = default!;
        public Guid Id { get; private set; } = default!;
        public Book Book { get; private set; } = default!;
        public Guid PatronId { get; private set; } = default!;
        public bool IsAvailable { get; private set; } = default!;

        private BookCopy () { }

        internal BookCopy (Book book, Guid id, Guid patronId) {
            Book = book;
            Id = id;
            PatronId = patronId;
            IsAvailable = true;
        }

        public void SetIsAvailable (bool available) {
            if (IsAvailable == available) {
                return;
            }

            IsAvailable = available;
            Book.ChangeAvailableNumberOfCopies(IsAvailable ? 1 : -1);
            Version++;
        }

        public override object GetId () {
            return Id;
        }

    }
}

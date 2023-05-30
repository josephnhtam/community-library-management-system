using Domain.Aggregates;

namespace CLMS.Domain.Aggregates.BookAggregate {
    public class BookCopy : DomainEntity {

        public int Version { get; private set; } = default!;
        public Guid Id { get; private set; } = default!;
        public Guid BookId { get; private set; } = default!;
        public Guid PatronId { get; private set; } = default!;
        public bool IsAvailable { get; private set; } = default!;

        private BookCopy () { }

        internal BookCopy (Guid id, Guid bookId, Guid patronId) {
            Id = id;
            BookId = bookId;
            PatronId = patronId;
            IsAvailable = true;
        }

        internal bool SetAvailability (bool available) {
            if (IsAvailable == available) {
                return false;
            }

            IsAvailable = available;
            Version++;
            return true;
        }

        public override object GetId () {
            return Id;
        }

    }
}

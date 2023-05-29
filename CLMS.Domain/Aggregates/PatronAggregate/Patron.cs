using Domain.Aggregates;
using Domain.Exceptions;

namespace CLMS.Domain.Aggregates.PatronAggregate {
    public class Patron : DomainEntity, IAggregateRoot {

        private List<BookDonation> _bookDonations = new();
        private List<BookLoan> _bookLoans = new();

        public int Version { get; private set; } = default!;
        public Guid Id { get; private set; } = default!;
        public PatronType Type { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string PhoneNumber { get; private set; } = default!;
        public Address Address { get; private set; } = default!;

        public int TotalBookDonationsCount { get; private set; } = 0;
        public int TotalBookLoansCount { get; private set; } = 0;
        public int ConcurrentBookLoansCount { get; private set; } = 0;

        public IReadOnlyList<BookDonation> BookDonations => _bookDonations;
        public IReadOnlyList<BookLoan> BookLoans => _bookLoans;

        private Patron () { }

        public Patron (PatronType type, string name, string phoneNumber, Address address) {
            Id = Guid.NewGuid();
            Type = type;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public BookDonation DonateBook (Guid bookId, DateTimeOffset date) {
            Guid bookCopyId = Guid.NewGuid();
            var donation = new BookDonation(this, bookId, bookCopyId, date);
            _bookDonations.Add(donation);
            Type = PatronType.BookDonor;
            TotalBookDonationsCount++;
            Version++;
            return donation;
        }

        public BookLoan BorrowBook (Guid bookCopyId, DateTimeOffset date, DateTimeOffset dueDate, int? maxConcurrentBookLoansCount) {
            if (maxConcurrentBookLoansCount.HasValue && ConcurrentBookLoansCount >= maxConcurrentBookLoansCount.Value) {
                throw new BusinessRuleValidationException("Maximum concurrent book loans limit reached");
            }

            if (dueDate <= date) {
                throw new BusinessRuleValidationException("The due date must be greater than borrow date");
            }

            var bookLoan = new BookLoan(this, bookCopyId, date, dueDate);
            ConcurrentBookLoansCount++;
            TotalBookLoansCount++;
            Version++;
            return bookLoan;
        }

        public void ReturnBook (BookLoan bookLoan, DateTimeOffset date) {
            if (bookLoan.Patron != this) {
                throw new BusinessRuleValidationException("Invalid patron");
            }

            if (bookLoan.ReturnDate != null) {
                throw new BusinessRuleValidationException("The book is already returned");
            }

            bookLoan.SetReturnDate(date);
            ConcurrentBookLoansCount--;
            Version++;
        }

        public override object GetId () {
            return Id;
        }

    }
}

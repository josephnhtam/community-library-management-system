namespace CLMS.Domain.Aggregates.PatronAggregate {
    public interface IPatronRepository {
        Task AddPatronAsync (Patron patron);
        Task RemovePatronAsync (Patron patron);
        Task<Patron?> GetPatronByIdAsync (
            Guid id,
            PaginatedBookLoansRetrievalOptions? bookLoansRetrievalOptions = null,
            PaginatedBookDonationsRetrievalOptions? bookDonationsRetrievalOptions = null);
        Task<IEnumerable<Patron?>> GetAllPatronsAsync (PatronType? type = null, PatronRetrievalOptions? options = null);
    }

    public class PatronRetrievalOptions {
        public int? Page { get; private set; }
        public int? PageSize { get; private set; }
        public BookLoansRetrievalOptions? BookLoansRetrievalOptions { get; private set; }
        public BookDonationsRetrievalOptions? BookDonationsRetrievalOptions { get; private set; }

        public PatronRetrievalOptions (
            int? page,
            int? pageSize,
            BookLoansRetrievalOptions? bookLoansRetrievalOptions = null,
            BookDonationsRetrievalOptions? bookDonationsRetrievalOptions = null) {
            Page = page;
            PageSize = pageSize;
            BookLoansRetrievalOptions = bookLoansRetrievalOptions;
            BookDonationsRetrievalOptions = bookDonationsRetrievalOptions;
        }
    }

    public class BookDonationsRetrievalOptions {
        public BookDonationsRetrieval BookDonationsRetrieval { get; private set; }
        public IReadOnlyList<Guid>? BookDonationIds { get; private set; }

        public BookDonationsRetrievalOptions (BookDonationsRetrieval bookDonationsRetrieval) {
            BookDonationsRetrieval = bookDonationsRetrieval;
            BookDonationIds = null;
        }

        public BookDonationsRetrievalOptions (IReadOnlyList<Guid> bookDonationIds) {
            BookDonationsRetrieval = BookDonationsRetrieval.SpecificIds;
            BookDonationIds = bookDonationIds;
        }
    }

    public class PaginatedBookDonationsRetrievalOptions : BookDonationsRetrievalOptions {
        public int? Page { get; private set; }
        public int? PageSize { get; private set; }

        public PaginatedBookDonationsRetrievalOptions (int? page, int? pageSize, BookDonationsRetrieval BookDonationsRetrieval) : base(BookDonationsRetrieval) {
            Page = page;
            PageSize = pageSize;
        }

        public PaginatedBookDonationsRetrievalOptions (int? page, int? pageSize, IReadOnlyList<Guid> BookDonationIds) : base(BookDonationIds) {
            Page = page;
            PageSize = pageSize;
        }
    }

    public enum BookDonationsRetrieval {
        None,
        All,
        SpecificIds
    }

    public class BookLoansRetrievalOptions {
        public BookLoansRetrieval BookLoansRetrieval { get; private set; }
        public IReadOnlyList<Guid>? BookLoanIds { get; private set; }

        public BookLoansRetrievalOptions (BookLoansRetrieval bookLoansRetrieval) {
            BookLoansRetrieval = bookLoansRetrieval;
            BookLoanIds = null;
        }

        public BookLoansRetrievalOptions (IReadOnlyList<Guid> bookLoanIds) {
            BookLoansRetrieval = BookLoansRetrieval.SpecificIds;
            BookLoanIds = bookLoanIds;
        }
    }

    public class PaginatedBookLoansRetrievalOptions : BookLoansRetrievalOptions {
        public int? Page { get; private set; }
        public int? PageSize { get; private set; }

        public PaginatedBookLoansRetrievalOptions (int? page, int? pageSize, BookLoansRetrieval BookLoansRetrieval) : base(BookLoansRetrieval) {
            Page = page;
            PageSize = pageSize;
        }

        public PaginatedBookLoansRetrievalOptions (int? page, int? pageSize, IReadOnlyList<Guid> BookLoanIds) : base(BookLoanIds) {
            Page = page;
            PageSize = pageSize;
        }
    }

    public enum BookLoansRetrieval {
        None,
        Returned,
        NotReturned,
        SpecificIds
    }
}

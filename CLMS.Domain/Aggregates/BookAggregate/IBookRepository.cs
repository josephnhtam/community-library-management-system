namespace CLMS.Domain.Aggregates.BookAggregate {
    public interface IBookRepository {
        Task AddBookAsync (Book book);
        Task RemoveBookAsync (Book book);
        Task<Book?> GetBookByIdAsync (Guid id, PaginatedBookCopiesRetrievalOptions? options = null);
        Task<Book?> GetBookByCopyIdAsync (Guid bookCopyId, PaginatedBookCopiesRetrievalOptions? options = null);
        Task<Book?> GetBookByAuthorIdAsync (Guid authorId, PaginatedBookCopiesRetrievalOptions? options = null);
        Task<IEnumerable<Book>> GetAllBooksAsync (BookRetrievalOptions? options = null);
    }

    public class BookRetrievalOptions {
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public BookCopiesRetrievalOptions? BookCopiesRetrievalOptions { get; private set; }

        public BookRetrievalOptions (int page, int pageSize, BookCopiesRetrievalOptions? bookCopiesRetrievalOptions = null) {
            Page = page;
            PageSize = pageSize;
            BookCopiesRetrievalOptions = bookCopiesRetrievalOptions;
        }
    }

    public class BookCopiesRetrievalOptions {
        public BookCopiesRetrieval BookCopiesRetrieval { get; private set; }
        public IReadOnlyList<Guid>? BookCopyIds { get; private set; }

        public BookCopiesRetrievalOptions (BookCopiesRetrieval bookCopiesRetrieval) {
            BookCopiesRetrieval = bookCopiesRetrieval;
            BookCopyIds = null;
        }

        public BookCopiesRetrievalOptions (IReadOnlyList<Guid> bookCopyIds) {
            BookCopiesRetrieval = BookCopiesRetrieval.SpecificIds;
            BookCopyIds = bookCopyIds;
        }
    }

    public class PaginatedBookCopiesRetrievalOptions : BookCopiesRetrievalOptions {
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public PaginatedBookCopiesRetrievalOptions (int page, int pageSize, BookCopiesRetrieval bookCopiesRetrieval) : base(bookCopiesRetrieval) {
            Page = page;
            PageSize = pageSize;
        }

        public PaginatedBookCopiesRetrievalOptions (int page, int pageSize, IReadOnlyList<Guid> bookCopyIds) : base(bookCopyIds) {
            Page = page;
            PageSize = pageSize;
        }
    }

    public enum BookCopiesRetrieval {
        None,
        Available,
        NotAvailable,
        SpecificIds
    }
}

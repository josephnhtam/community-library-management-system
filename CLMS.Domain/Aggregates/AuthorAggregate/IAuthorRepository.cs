namespace CLMS.Domain.Aggregates.AuthorAggregate {
    public interface IAuthorRepository {
        Task AddAuthorAsync (Author author);
        Task RemoveAuthorAsync (Author author);
        Task<Author?> GetAuthorByIdAsync (Guid id);
        Task<IEnumerable<Author>> GetAuthorsAsync (AuthorRetrievalOptions? options = null);
    }

    public class AuthorRetrievalOptions {
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public AuthorRetrievalOptions (int page, int pageSize) {
            Page = page;
            PageSize = pageSize;
        }
    }
}

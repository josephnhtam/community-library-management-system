using CLMS.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;

namespace CLMS.Infrastructure.Repositories {
    public class BookRepository : IBookRepository {

        private readonly LibraryDbContext _context;

        public BookRepository (LibraryDbContext context) {
            _context = context;
        }

        public async Task AddBookAsync (Book book) {
            await _context.Books.AddAsync(book);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync (BookRetrievalOptions? options = null) {
            IQueryable<Book> query = _context.Books;

            if (options != null) {
                if (options.PageSize.HasValue && options.Page.HasValue) {
                    query = query.Take(options.PageSize.Value)
                                 .Skip(options.Page.Value * options.PageSize.Value);
                }

                if (options.BookCopiesRetrievalOptions?.BookCopiesRetrieval != BookCopiesRetrieval.None) {
                    query = query.Include(x => ApplyOptions(x.Copies, options.BookCopiesRetrievalOptions));
                }
            }

            return await query.ToListAsync();
        }

        public async Task<Book?> GetBookByAuthorIdAsync (Guid authorId, PaginatedBookCopiesRetrievalOptions? options = null) {
            IQueryable<Book> query = _context.Books;

            if (options?.BookCopiesRetrieval != BookCopiesRetrieval.None) {
                query = query.Include(x => ApplyOptions(x.Copies, options));
            }

            return await query.FirstOrDefaultAsync(x => x.Authors.Any(x => x.AuthorId == authorId));
        }

        public async Task<Book?> GetBookByCopyIdAsync (Guid bookCopyId, PaginatedBookCopiesRetrievalOptions? options = null) {
            IQueryable<Book> query = _context.Books;

            if (options?.BookCopiesRetrieval != BookCopiesRetrieval.None) {
                query = query.Include(x => ApplyOptions(x.Copies, options));
            }

            return await query.FirstOrDefaultAsync(x => x.Copies.Any(x => x.Id == bookCopyId));
        }

        public async Task<Book?> GetBookByIdAsync (Guid id, PaginatedBookCopiesRetrievalOptions? options = null) {
            IQueryable<Book> query = _context.Books;

            if (options?.BookCopiesRetrieval != BookCopiesRetrieval.None) {
                query = query.Include(x => ApplyOptions(x.Copies, options));
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task RemoveBookAsync (Book book) {
            _context.Books.Remove(book);
            return Task.CompletedTask;
        }

        private static IEnumerable<BookCopy> ApplyOptions (IEnumerable<BookCopy> copies, PaginatedBookCopiesRetrievalOptions? options) {
            if (options != null) {
                copies = ApplyOptions(copies, options as BookCopiesRetrievalOptions);

                if (options.PageSize.HasValue && options.Page.HasValue) {
                    copies = copies.Take(options.PageSize.Value).Skip(options.Page.Value * options.PageSize.Value);
                }
            }

            return copies;
        }

        private static IEnumerable<BookCopy> ApplyOptions (IEnumerable<BookCopy> copies, BookCopiesRetrievalOptions? options) {
            if (options != null) {
                switch (options.BookCopiesRetrieval) {
                    case BookCopiesRetrieval.Available:
                        copies = copies.Where(y => y.IsAvailable);
                        break;
                    case BookCopiesRetrieval.NotAvailable:
                        copies = copies.Where(y => !y.IsAvailable);
                        break;
                    case BookCopiesRetrieval.SpecificIds:
                        if (options.BookCopyIds != null)
                            copies = copies.Where(y => options.BookCopyIds.Contains(y.Id));
                        break;
                }
            }

            return copies;
        }

    }
}

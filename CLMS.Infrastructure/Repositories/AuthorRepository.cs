using CLMS.Domain.Aggregates.AuthorAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CLMS.Infrastructure.Repositories {
    public class AuthorRepository : IAuthorRepository {

        private readonly LibraryDbContext _context;

        public AuthorRepository (LibraryDbContext context) {
            _context = context;
        }

        public async Task AddAuthorAsync (Author author) {
            await _context.Authors.AddAsync(author);
        }

        public async Task<Author?> GetAuthorByIdAsync (Guid id) {
            return await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync (AuthorRetrievalOptions? options = null) {
            IQueryable<Author> query = _context.Authors;

            if (options != null && options.PageSize.HasValue && options.Page.HasValue) {
                query = query.Take(options.PageSize.Value)
                             .Skip(options.Page.Value * options.PageSize.Value);
            }

            return await query.ToListAsync();
        }

        public Task RemoveAuthorAsync (Author author) {
            _context.Authors.Remove(author);
            return Task.CompletedTask;
        }
    }
}

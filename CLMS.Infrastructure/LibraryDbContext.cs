using CLMS.Domain.Aggregates.AuthorAggregate;
using CLMS.Domain.Aggregates.BookAggregate;
using CLMS.Domain.Aggregates.PatronAggregate;
using Microsoft.EntityFrameworkCore;

namespace CLMS.Infrastructure {
    public class LibraryDbContext : DbContext {

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<BookDonation> BookDonations { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }

        public LibraryDbContext (DbContextOptions<LibraryDbContext> options) : base(options) {

        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
        }

    }
}

using CLMS.Domain.Aggregates.PatronAggregate;
using Microsoft.EntityFrameworkCore;

namespace CLMS.Infrastructure.Repositories {
    public class PatronRepository : IPatronRepository {

        private readonly LibraryDbContext _context;

        public PatronRepository (LibraryDbContext context) {
            _context = context;
        }

        public async Task AddPatronAsync (Patron patron) {
            await _context.Patrons.AddAsync(patron);
        }

        public async Task<IEnumerable<Patron?>> GetAllPatronsAsync (PatronType? type = null, PatronRetrievalOptions? options = null) {
            IQueryable<Patron> patrons = _context.Patrons;

            if (options != null) {
                patrons = patrons.Take(options.PageSize)
                                 .Skip(options.Page * options.PageSize);

                if (options.BookDonationsRetrievalOptions?.BookDonationsRetrieval != BookDonationsRetrieval.None) {
                    patrons = patrons.Include(x => ApplyOptions(x.BookDonations, options.BookDonationsRetrievalOptions));
                }

                if (options.BookLoansRetrievalOptions?.BookLoansRetrieval != BookLoansRetrieval.None) {
                    patrons = patrons.Include(x => ApplyOptions(x.BookLoans, options.BookLoansRetrievalOptions));
                }
            }

            if (type.HasValue) {
                patrons = patrons.Where(x => x.Type == type.Value);
            }

            return await patrons.ToListAsync();
        }

        public async Task<Patron?> GetPatronByIdAsync (Guid id, PaginatedBookLoansRetrievalOptions? bookLoansRetrievalOptions = null, PaginatedBookDonationsRetrievalOptions? bookDonationsRetrievalOptions = null) {
            IQueryable<Patron> patrons = _context.Patrons;

            if (bookDonationsRetrievalOptions?.BookDonationsRetrieval != BookDonationsRetrieval.None) {
                patrons = patrons.Include(x => ApplyOptions(x.BookDonations, bookDonationsRetrievalOptions));
            }

            if (bookLoansRetrievalOptions?.BookLoansRetrieval != BookLoansRetrieval.None) {
                patrons = patrons.Include(x => ApplyOptions(x.BookLoans, bookLoansRetrievalOptions));
            }

            return await patrons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task RemovePatronAsync (Patron patron) {
            _context.Patrons.Remove(patron);
            return Task.CompletedTask;
        }

        private static IEnumerable<BookLoan> ApplyOptions (IEnumerable<BookLoan> loans, PaginatedBookLoansRetrievalOptions? options) {
            if (options != null) {
                loans = ApplyOptions(loans, options as BookLoansRetrievalOptions)
                    .Take(options.PageSize).Skip(options.Page * options.PageSize);
            }

            return loans;
        }

        private static IEnumerable<BookLoan> ApplyOptions (IEnumerable<BookLoan> loans, BookLoansRetrievalOptions? options) {
            if (options != null) {
                switch (options.BookLoansRetrieval) {
                    case BookLoansRetrieval.SpecificIds:
                        if (options.BookLoanIds != null)
                            loans = loans.Where(y => options.BookLoanIds.Contains(y.Id));
                        break;
                }
            }

            return loans;
        }

        private static IEnumerable<BookDonation> ApplyOptions (IEnumerable<BookDonation> donations, PaginatedBookDonationsRetrievalOptions? options) {
            if (options != null) {
                donations = ApplyOptions(donations, options as BookDonationsRetrievalOptions)
                    .Take(options.PageSize).Skip(options.Page * options.PageSize);
            }

            return donations;
        }

        private static IEnumerable<BookDonation> ApplyOptions (IEnumerable<BookDonation> donations, BookDonationsRetrievalOptions? options) {
            if (options != null) {
                switch (options.BookDonationsRetrieval) {
                    case BookDonationsRetrieval.SpecificIds:
                        if (options.BookDonationIds != null)
                            donations = donations.Where(y => options.BookDonationIds.Contains(y.Id));
                        break;
                }
            }

            return donations;
        }

    }
}

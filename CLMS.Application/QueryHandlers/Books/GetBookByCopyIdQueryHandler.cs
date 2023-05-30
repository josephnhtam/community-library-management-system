using Application.Contracts;
using CLMS.Application.Queries.Books;
using CLMS.Domain.Aggregates.BookAggregate;

namespace CLMS.Application.QueryHandlers.Books {
    public class GetBookByCopyIdQueryHandler : IQueryHandler<GetBookByCopyIdQuery, Book?> {

        private readonly IBookRepository _bookRepository;

        public GetBookByCopyIdQueryHandler (IBookRepository bookRepository) {
            _bookRepository = bookRepository;
        }

        public async Task<Book?> Handle (GetBookByCopyIdQuery request, CancellationToken cancellationToken) {
            return await _bookRepository.GetBookByCopyIdAsync(
                request.BookCopyId,
                new(null, null, BookCopiesRetrieval.All));
        }

    }
}

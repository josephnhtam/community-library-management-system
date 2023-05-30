using Application.Contracts;
using CLMS.Application.Queries.Books;
using CLMS.Domain.Aggregates.BookAggregate;

namespace CLMS.Application.QueryHandlers.Books {
    public class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, Book?> {

        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler (IBookRepository bookRepository) {
            _bookRepository = bookRepository;
        }

        public async Task<Book?> Handle (GetBookByIdQuery request, CancellationToken cancellationToken) {
            return await _bookRepository.GetBookByIdAsync(
                request.BookId,
                new(null, null, BookCopiesRetrieval.All));
        }

    }
}

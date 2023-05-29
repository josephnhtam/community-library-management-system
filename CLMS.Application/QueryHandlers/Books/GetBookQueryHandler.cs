using Application.Contracts;
using CLMS.Application.Queris.Books;
using CLMS.Domain.Aggregates.BookAggregate;

namespace CLMS.Application.QueryHandlers.Books {
    public class GetBookQueryHandler : IQueryHandler<GetBookQuery, Book?> {

        private readonly IBookRepository _bookRepository;

        public GetBookQueryHandler (IBookRepository bookRepository) {
            _bookRepository = bookRepository;
        }

        public async Task<Book?> Handle (GetBookQuery request, CancellationToken cancellationToken) {
            return await _bookRepository.GetBookByIdAsync(
                request.BookId,
                new(null, null, BookCopiesRetrieval.All));
        }

    }
}

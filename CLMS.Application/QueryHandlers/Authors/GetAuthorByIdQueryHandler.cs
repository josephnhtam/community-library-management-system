using Application.Contracts;
using CLMS.Application.Queries.Authors;
using CLMS.Domain.Aggregates.AuthorAggregate;

namespace CLMS.Application.QueryHandlers.Authors {
    public class GetAuthorByIdQueryHandler : IQueryHandler<GetAuthorByIdQuery, Author?> {

        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler (IAuthorRepository authorRepository) {
            _authorRepository = authorRepository;
        }

        public async Task<Author?> Handle (GetAuthorByIdQuery request, CancellationToken cancellationToken) {
            var author = await _authorRepository.GetAuthorByIdAsync(request.AuthorId);
            return author;
        }
    }
}

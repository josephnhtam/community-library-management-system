using Application.Contracts;
using CLMS.Application.Commands.Authors;
using CLMS.Domain.Aggregates.AuthorAggregate;
using Domain.Contracts;

namespace CLMS.Application.CommandHandlers.Authors {
    public class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand, Author> {

        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddAuthorCommandHandler (IAuthorRepository authorRepository, IUnitOfWork unitOfWork) {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> Handle (AddAuthorCommand request, CancellationToken cancellationToken) {
            var author = new Author(request.Name, request.Description);

            await _authorRepository.AddAuthorAsync(author);
            await _unitOfWork.CommitAsync(cancellationToken);

            return author;
        }

    }
}

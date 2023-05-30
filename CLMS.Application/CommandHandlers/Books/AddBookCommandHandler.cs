using Application.Contracts;
using CLMS.Application.Commands.Books;
using CLMS.Domain.Aggregates.BookAggregate;
using Domain.Contracts;

namespace CLMS.Application.CommandHandlers.Books {
    public class AddBookCommandHandler : ICommandHandler<AddBookCommand, Book> {

        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddBookCommandHandler (IBookRepository BookRepository, IUnitOfWork unitOfWork) {
            _bookRepository = BookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> Handle (AddBookCommand request, CancellationToken cancellationToken) {
            var Book = new Book(request.Title, request.Description, request.PublicationDate, request.Authors);

            await _bookRepository.AddBookAsync(Book);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Book;
        }

    }
}

using CLMS.Domain.Aggregates.BookAggregate;
using CLMS.Domain.Aggregates.PatronAggregate.Events;
using Domain.Contracts;
using Domain.Events;
using Domain.Exceptions;

namespace CLMS.Application.DomainEventHandlers {
    public class BookCopyDonatedDomainEventHandler : IDomainEventHandler<BookCopyDonatedDomainEvent> {

        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookCopyDonatedDomainEventHandler (IBookRepository bookRepository, IUnitOfWork unitOfWork) {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle (BookCopyDonatedDomainEvent @event, CancellationToken cancellationToken) {
            var donation = @event.Donation;

            var book = await _bookRepository.GetBookByIdAsync(
                donation.BookId,
                new(null, null, new List<Guid>() { donation.BookCopyId }));

            if (book == null) {
                throw new BusinessRuleValidationException("Book not found");
            }

            book.AddCopy(donation.BookCopyId, donation.Patron.Id);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

    }
}

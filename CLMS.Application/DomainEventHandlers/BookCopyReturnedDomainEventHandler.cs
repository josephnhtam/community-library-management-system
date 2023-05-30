using CLMS.Domain.Aggregates.BookAggregate;
using CLMS.Domain.Aggregates.PatronAggregate.Events;
using Domain.Contracts;
using Domain.Events;
using Domain.Exceptions;

namespace CLMS.Application.DomainEventHandlers {
    public class BookCopyReturnedDomainEventHandler : IDomainEventHandler<BookCopyReturnedDomainEvent> {

        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookCopyReturnedDomainEventHandler (IBookRepository bookRepository, IUnitOfWork unitOfWork) {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle (BookCopyReturnedDomainEvent @event, CancellationToken cancellationToken) {
            var loan = @event.Loan;

            var bookWithBookCopy = await _bookRepository.GetBookByCopyIdAsync(
                loan.BookCopyId,
                new(null, null, new List<Guid>() { loan.BookCopyId }));

            if (bookWithBookCopy == null) {
                throw new BusinessRuleValidationException("Book not found");
            }

            bookWithBookCopy.SetBookCopyAvailability(loan.BookCopyId, true);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

    }
}

﻿using CLMS.Domain.Aggregates.BookAggregate;
using CLMS.Domain.Aggregates.PatronAggregate.Events;
using Domain.Contracts;
using Domain.Events;
using Domain.Exceptions;

namespace CLMS.Application.DomainEventHandlers {
    public class BookCopyBorrowedDomainEventHandler : IDomainEventHandler<BookCopyBorrowedDomainEvent> {

        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookCopyBorrowedDomainEventHandler (IBookRepository bookRepository, IUnitOfWork unitOfWork) {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle (BookCopyBorrowedDomainEvent @event, CancellationToken cancellationToken) {
            var loan = @event.Loan;

            var bookWithBookCopy = await _bookRepository.GetBookByCopyIdAsync(
                loan.BookCopyId,
                new(null, null, new List<Guid>() { loan.BookCopyId }));

            var bookCopy = bookWithBookCopy?.Copies.FirstOrDefault(x => x.Id == loan.BookCopyId);

            if (bookCopy == null) {
                throw new BusinessRuleValidationException("Book copy not found");
            }

            if (!bookCopy.IsAvailable) {
                throw new BusinessRuleValidationException("Book copy is not available");
            }

            bookCopy.SetIsAvailable(false);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

    }
}
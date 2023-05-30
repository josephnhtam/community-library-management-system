using Application.Contracts;
using CLMS.Application.Commands.Patrons;
using CLMS.Application.Configurations;
using CLMS.Domain.Aggregates.PatronAggregate;
using Domain.Contracts;
using Domain.Exceptions;

namespace CLMS.Application.CommandHandlers.Patrons {
    public class ReturnBookCommandHandler : ICommandHandler<ReturnBookCommand, BookLoan> {

        private readonly IPatronRepository _patronRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReturnBookCommandHandler (IPatronRepository patronRepository, IUnitOfWork unitOfWork) {
            _patronRepository = patronRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookLoan> Handle (ReturnBookCommand request, CancellationToken cancellationToken) {
            var patron = await _patronRepository.GetPatronByIdAsync(
                request.PatronId,
                new PaginatedBookLoansRetrievalOptions(null, null, new List<Guid>() { request.BookLoanId })
            );

            if (patron == null) {
                throw new BusinessRuleValidationException("Patron not found");
            }

            var bookLoan = patron.BookLoans.FirstOrDefault(x => x.Id == request.BookLoanId);

            if (bookLoan == null) {
                throw new BusinessRuleValidationException("Book loan not found");
            }

            patron.ReturnBook(bookLoan, request.Date);

            await _unitOfWork.CommitAsync(cancellationToken);

            return bookLoan;
        }

    }
}

using Application.Contracts;
using CLMS.Application.Commands.Patrons;
using CLMS.Application.Configurations;
using CLMS.Domain.Aggregates.PatronAggregate;
using Domain.Contracts;
using Domain.Exceptions;
using Microsoft.Extensions.Options;

namespace CLMS.Application.CommandHandlers.Patrons {
    public class BorrowBookCommandHandler : ICommandHandler<BorrowBookCommand, BookLoan> {

        private readonly IPatronRepository _patronRepository;
        private readonly BookLoanPolicy _bookLoanPolicy;
        private readonly IUnitOfWork _unitOfWork;

        public BorrowBookCommandHandler (IPatronRepository patronRepository, IOptions<BookLoanPolicy> bookLoanPolicy, IUnitOfWork unitOfWork) {
            _patronRepository = patronRepository;
            _bookLoanPolicy = bookLoanPolicy.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookLoan> Handle (BorrowBookCommand request, CancellationToken cancellationToken) {
            BookLoan? bookLoan = null;

            await _unitOfWork.ExecuteOptimisticUpdateAsync(async () => {
                var patron = await _patronRepository.GetPatronByIdAsync(request.PatronId);

                if (patron == null) {
                    throw new BusinessRuleValidationException("Patron not found");
                }

                int? maxConcurrentBookLoansCount = null;
                switch (patron.Type) {
                    case PatronType.BookDonor:
                        maxConcurrentBookLoansCount = _bookLoanPolicy.DonorMaxConcurrentBookLoansCount;
                        break;
                    case PatronType.Customer:
                        maxConcurrentBookLoansCount = _bookLoanPolicy.CustomerMaxConcurrentBookLoansCount;
                        break;
                }

                bookLoan = patron.BorrowBook(request.BookCopyId, request.Date, request.DueDate, maxConcurrentBookLoansCount);

                await _unitOfWork.CommitAsync(cancellationToken);
            });

            return bookLoan!;
        }

    }
}

using Application.Contracts;
using CLMS.Application.Commands.Patrons;
using CLMS.Domain.Aggregates.PatronAggregate;
using Domain.Contracts;
using Domain.Exceptions;

namespace CLMS.Application.CommandHandlers.Patrons {
    public class DonateBookCommandHandler : ICommandHandler<DonateBookCommand, BookDonation> {

        private readonly IPatronRepository _patronRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DonateBookCommandHandler (IPatronRepository patronRepository, IUnitOfWork unitOfWork) {
            _patronRepository = patronRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookDonation> Handle (DonateBookCommand request, CancellationToken cancellationToken) {
            BookDonation? donation = null;

            await _unitOfWork.ExecuteOptimisticUpdateAsync(async () => {
                var patron = await _patronRepository.GetPatronByIdAsync(request.PatronId);

                if (patron == null) {
                    throw new BusinessRuleValidationException("Patron not found");
                }

                donation = patron.DonateBook(request.BookId, request.Date);

                await _unitOfWork.CommitAsync(cancellationToken);
            });

            return donation!;
        }

    }
}

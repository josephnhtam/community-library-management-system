using Application.Contracts;
using CLMS.Application.Commands.Patrons;
using CLMS.Domain.Aggregates.PatronAggregate;
using Domain.Contracts;

namespace CLMS.Application.CommandHandlers.Patrons {
    public class AddPatronCommandHandler : ICommandHandler<AddPatronCommand, Patron> {

        private readonly IPatronRepository _patronRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddPatronCommandHandler (IPatronRepository patronRepository, IUnitOfWork unitOfWork) {
            _patronRepository = patronRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Patron> Handle (AddPatronCommand request, CancellationToken cancellationToken) {
            var patron = new Patron(request.Type, request.Name, request.Description, request.PhoneNumber, request.Address);

            await _patronRepository.AddPatronAsync(patron);
            await _unitOfWork.CommitAsync();

            return patron;
        }

    }
}

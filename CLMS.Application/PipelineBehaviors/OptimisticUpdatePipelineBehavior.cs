using Application.Contracts;
using Domain.Contracts;
using MediatR;

namespace CLMS.Application.PipelineBehaviors {
    public class OptimisticUpdatePipelineBehavior<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
        where TCommand : ICommand<TResult> {

        private readonly IUnitOfWork _unitOfWork;

        public OptimisticUpdatePipelineBehavior (IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResult> Handle (TCommand request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken) {
            TResult result = default!;

            await _unitOfWork.ExecuteOptimisticUpdateAsync(async () => {
                result = await next();
            });

            return result!;
        }

    }
}

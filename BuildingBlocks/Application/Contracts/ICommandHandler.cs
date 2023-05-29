using MediatR;

namespace Application.Contracts {
    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest>
        where TRequest : ICommand {
    }

    public interface ICommandHandler<in TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : ICommand<TResult> {
    }
}

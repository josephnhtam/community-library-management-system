using MediatR;

namespace Application.Contracts {
    public interface ICommand<out TResult> : IRequest<TResult> { }
    public interface ICommand : ICommand<Unit> { }
}

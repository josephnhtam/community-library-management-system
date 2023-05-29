using MediatR;

namespace Application.Contracts {
    public interface IQuery<out TResult> : IRequest<TResult> { }
    public interface IQuery : IQuery<Unit> { }
}

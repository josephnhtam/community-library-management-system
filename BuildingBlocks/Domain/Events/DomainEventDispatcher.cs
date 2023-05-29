using MediatR;

namespace Domain.Events {
    public class DomainEventDispatcher : IDomainEventsDispatcher {

        private readonly IMediator _mediator;
        private readonly IDomainEventsAccessor _accessor;

        public DomainEventDispatcher (IMediator mediator, IDomainEventsAccessor accessor) {
            _mediator = mediator;
            _accessor = accessor;
        }

        public async Task DispatchDomainEventsAsync () {
            var domainEvents = _accessor.GetDomainEvents();

            _accessor.ClearDomainEvents();

            foreach (var domainEvent in domainEvents) {
                await _mediator.Publish(domainEvent);
            }
        }

    }
}

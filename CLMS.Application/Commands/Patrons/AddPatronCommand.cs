using Application.Contracts;
using CLMS.Domain.Aggregates.PatronAggregate;

namespace CLMS.Application.Commands.Patrons {
    public class AddPatronCommand : ICommand<Patron> {
        public PatronType Type { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public Address Address { get; init; } = default!;
    }
}

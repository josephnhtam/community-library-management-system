using CLMS.Domain.Aggregates.PatronAggregate;

namespace CLMS.API.DtoModels.Patrons
{
    public class AddPatronRequest
    {
        public PatronType Type { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public Address Address { get; init; } = default!;
    }
}

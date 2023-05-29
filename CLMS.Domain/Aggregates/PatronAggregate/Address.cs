using Domain.Aggregates;

namespace CLMS.Domain.Aggregates.PatronAggregate {
    public class Address : ValueObject {
        public string Street { get; private set; } = default!;
        public string City { get; private set; } = default!;
        public string State { get; private set; } = default!;
        public string Country { get; private set; } = default!;
        public string ZipCode { get; private set; } = default!;

        private Address () { }

        public Address (string street, string city, string state, string country, string zipcode) {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }
    }
}

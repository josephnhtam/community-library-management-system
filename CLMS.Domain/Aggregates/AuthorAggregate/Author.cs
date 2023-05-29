using Domain.Aggregates;

namespace CLMS.Domain.Aggregates.AuthorAggregate {
    public class Author : DomainEntity, IAggregateRoot {

        public int Version { get; private set; } = default!;
        public Guid Id { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        private Author () { }

        public Author (string name, string description) {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public override object GetId () {
            return Id;
        }

    }
}

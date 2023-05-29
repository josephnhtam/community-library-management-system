using CLMS.Domain.Aggregates.PatronAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLMS.Infrastructure.EntityTypeConfigurations {
    public class PatronEntityTypeConfiguration : IEntityTypeConfiguration<Patron> {

        public void Configure (EntityTypeBuilder<Patron> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.Version).IsConcurrencyToken();

            builder.Property(x => x.Type).IsRequired();
            builder.HasIndex(x => x.Type);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.PhoneNumber).IsRequired();

            builder.Property(x => x.TotalBookDonationsCount).IsRequired();

            builder.Property(x => x.TotalBookLoansCount).IsRequired();

            builder.Property(x => x.ConcurrentBookLoansCount).IsRequired();

            var addressBuilder = builder.OwnsOne(x => x.Address);
            addressBuilder.Property(x => x.Street).IsRequired();
            addressBuilder.Property(x => x.City).IsRequired();
            addressBuilder.Property(x => x.State).IsRequired();
            addressBuilder.Property(x => x.Country).IsRequired();
            addressBuilder.Property(x => x.ZipCode).IsRequired();

            builder.Navigation(x => x.BookLoans)
                   .HasField("_bookLoans")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.BookLoans)
                   .WithOne(x => x.Patron)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.Navigation(x => x.BookDonations)
                   .HasField("_bookDonations")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.BookDonations)
                   .WithOne(x => x.Patron)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }

    }
}

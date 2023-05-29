using CLMS.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLMS.Infrastructure.EntityTypeConfigurations {
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book> {

        public void Configure (EntityTypeBuilder<Book> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.Version).IsConcurrencyToken();

            builder.Property(x => x.Title).IsRequired();

            builder.Property(x => x.Description).IsRequired();

            builder.Property(x => x.PublicationDate).IsRequired();

            builder.Property(x => x.TotalNumberOfCopies).IsRequired();
            builder.HasIndex(x => x.TotalNumberOfCopies);

            builder.Property(x => x.AvailableNumberOfCopies).IsRequired();
            builder.HasIndex(x => x.AvailableNumberOfCopies);

            builder.Navigation(x => x.Copies)
                   .HasField("_copies")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.Copies)
                   .WithOne(x => x.Book)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.Navigation(x => x.Authors)
                   .HasField("_authors")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.Authors)
                   .WithOne(x => x.Book)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }

    }
}

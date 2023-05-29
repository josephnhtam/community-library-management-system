using CLMS.Domain.Aggregates.BookAggregate;
using CLMS.Domain.Aggregates.PatronAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLMS.Infrastructure.EntityTypeConfigurations {
    public class BookCopyEntityTypeConfiguration : IEntityTypeConfiguration<BookCopy> {

        public void Configure (EntityTypeBuilder<BookCopy> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.Version).IsConcurrencyToken();

            builder.Property(x => x.IsAvailable).IsRequired();
            builder.HasIndex(x => x.IsAvailable);

            builder.HasOne<Patron>()
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasForeignKey<BookCopy>(x => x.PatronId);
        }

    }
}

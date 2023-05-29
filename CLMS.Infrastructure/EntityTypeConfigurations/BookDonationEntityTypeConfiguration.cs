using CLMS.Domain.Aggregates.BookAggregate;
using CLMS.Domain.Aggregates.PatronAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLMS.Infrastructure.EntityTypeConfigurations {
    public class BookDonationEntityTypeConfiguration : IEntityTypeConfiguration<BookDonation> {

        public void Configure (EntityTypeBuilder<BookDonation> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.Version).IsConcurrencyToken();

            builder.Property(x => x.Date).IsRequired();

            builder.HasOne<BookCopy>()
                   .WithOne()
                   .HasForeignKey<BookDonation>(x => x.BookCopyId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }

    }
}

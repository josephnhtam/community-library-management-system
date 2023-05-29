using CLMS.Domain.Aggregates.BookAggregate;
using CLMS.Domain.Aggregates.PatronAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLMS.Infrastructure.EntityTypeConfigurations {
    public class BookLoanEntityTypeConfiguration : IEntityTypeConfiguration<BookLoan> {

        public void Configure (EntityTypeBuilder<BookLoan> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.Version).IsConcurrencyToken();

            builder.Property(x => x.BorrowDate).IsRequired();
            builder.HasIndex(x => x.BorrowDate);

            builder.Property(x => x.DueDate).IsRequired();
            builder.HasIndex(x => x.DueDate);

            builder.Property(x => x.ReturnDate);
            builder.HasIndex(x => x.ReturnDate);

            builder.HasOne<BookCopy>()
                   .WithOne()
                   .HasForeignKey<BookLoan>(x => x.BookCopyId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }

    }
}

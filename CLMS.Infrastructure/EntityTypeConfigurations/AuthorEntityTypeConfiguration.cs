using CLMS.Domain.Aggregates.AuthorAggregate;
using CLMS.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CLMS.Infrastructure.EntityTypeConfigurations {
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author> {

        public void Configure (EntityTypeBuilder<Author> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.Version).IsConcurrencyToken();

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Description).IsRequired();

            builder.HasMany<BookAuthor>()
                   .WithOne()
                   .HasForeignKey(x => x.AuthorId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }

    }
}

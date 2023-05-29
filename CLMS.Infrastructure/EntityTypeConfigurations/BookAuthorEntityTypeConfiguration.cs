using CLMS.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CLMS.Infrastructure.EntityTypeConfigurations {
    public class BookAuthorEntityTypeConfiguration : IEntityTypeConfiguration<BookAuthor> {
        public void Configure (EntityTypeBuilder<BookAuthor> builder) {
            builder.HasKey(x => x.Id);
        }
    }
}

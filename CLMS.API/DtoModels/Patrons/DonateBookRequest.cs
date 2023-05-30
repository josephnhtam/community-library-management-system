namespace CLMS.API.DtoModels.Patrons {
    public class DonateBookRequest {
        public Guid PatronId { get; init; } = default!;
        public Guid BookId { get; init; } = default!;
        public DateTimeOffset Date { get; init; } = default!;
    }
}

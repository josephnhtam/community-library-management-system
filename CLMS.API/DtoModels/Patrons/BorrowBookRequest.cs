namespace CLMS.API.DtoModels.Patrons {
    public class BorrowBookRequest {
        public Guid PatronId { get; init; } = default!;
        public Guid BookCopyId { get; init; } = default!;
        public DateTimeOffset Date { get; init; } = default!;
        public DateTimeOffset DueDate { get; init; } = default!;
    }
}

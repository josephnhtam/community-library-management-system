namespace CLMS.API.DtoModels.Patrons {
    public class ReturnBookRequest {
        public Guid PatronId { get; init; } = default!;
        public Guid BookLoanId { get; init; } = default!;
        public DateTimeOffset Date { get; init; } = default!;
    }
}
namespace CLMS.Application.Configurations {
    public class BookLoanPolicy {
        public int DonorMaxConcurrentBookLoansCount { get; set; } = 5;
        public int CustomerMaxConcurrentBookLoansCount { get; set; } = 3;
    }
}

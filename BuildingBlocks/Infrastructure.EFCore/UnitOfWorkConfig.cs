namespace Infrastructure.EFCore {
    public class UnitOfWorkConfig {
        public int OptimisticConcurrencyConflictRetryCount { get; set; } = 10;
    }
}

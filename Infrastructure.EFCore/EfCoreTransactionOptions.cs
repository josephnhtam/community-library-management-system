using Domain.Contracts;
using System.Data;

namespace Infrastructure.EFCore {
    public class EfCoreTransactionOptions : ITransactionOptions {
        public IsolationLevel? IsolationLevel { get; set; }
        public bool ResetContext { get; set; } = true;
    }
}

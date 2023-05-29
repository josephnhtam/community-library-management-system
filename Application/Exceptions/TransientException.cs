namespace Application.Exceptions {
    public class TransientException : Exception {
        public TransientException (string? message, Exception? innerException = null) : base(message, innerException) { }
    }
}

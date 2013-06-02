namespace LOB.Util.Contract.Exception {
    public class BaseException : System.Exception {
        public BaseException(string message = null, string detail = null, System.Exception innerException = null)
                : base(message, innerException) { Detail = detail; }
        public string Detail { get; private set; }
    }
}
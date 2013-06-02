namespace LOB.Business.Contract.Exception {
    public class EntityGenerateIdException : EntityGenerateException {
        public EntityGenerateIdException(string message, string detail = null, System.Exception innerException = null)
                : base(message, detail, innerException) { }
    }
}
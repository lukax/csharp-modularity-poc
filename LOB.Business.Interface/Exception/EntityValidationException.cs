#region Usings

using LOB.Business.Contract.Exception.Base;

#endregion

namespace LOB.Business.Contract.Exception {
    public class EntityValidationException : GenericBusinessException {
        public EntityValidationException(string message, string detail = null, System.Exception innerException = null)
                : base(message, detail, innerException) { }
    }
}
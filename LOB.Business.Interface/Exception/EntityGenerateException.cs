#region Usings

using LOB.Business.Contract.Exception.Base;

#endregion

namespace LOB.Business.Contract.Exception {
    public class EntityGenerateException : GenericBusinessException {
        public EntityGenerateException(string message, string detail = null, System.Exception innerException = null)
            : base(message, detail, innerException) {}
    }
}
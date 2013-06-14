#region Usings

using LOB.Util.Contract.Exception;

#endregion

namespace LOB.Business.Contract.Exception.Base {
    public class GenericBusinessException : BaseException {
        public GenericBusinessException(string message, string detail = null, System.Exception innerException = null)
                : base(message, detail, innerException) { }
    }
}
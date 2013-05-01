#region Usings

using LOB.Util.Contract.Exception;

#endregion

namespace LOB.UI.Contract.Exception.Base {
    public class GenericUIException : BaseException {
        public GenericUIException(string message, string detail = null, System.Exception innerException = null)
            : base(message, detail, innerException) {}
    }
}
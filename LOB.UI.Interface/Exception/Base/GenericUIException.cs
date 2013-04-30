#region Usings

using LOB.Util.Contract.Exception;

#endregion

namespace LOB.UI.Contract.Exception.Base {
    public class GenericUIException : BaseException {
        public string LocalizedMessage { get; private set; }
        public GenericUIException(string localizedMessage) {
            LocalizedMessage = localizedMessage;
        }
    }
}
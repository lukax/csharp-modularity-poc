#region Usings

using LOB.UI.Contract.Exception.Base;

#endregion

namespace LOB.UI.Contract.Exception {
    public class ViewLoadException : GenericUIException {
        public ViewLoadException(string localizedMessage)
            : base(localizedMessage) {}
    }
}
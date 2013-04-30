#region Usings

using LOB.UI.Contract.Exception.Base;

#endregion

namespace LOB.UI.Contract.Exception {
    public class ViewModelLoadException : GenericUIException {
        public ViewModelLoadException(string localizedMessage)
            : base(localizedMessage) {}
    }
}
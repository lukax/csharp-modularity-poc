#region Usings

using System;
using System.ComponentModel.Composition;

#endregion

namespace LOB.Dao.Interface {
    [InheritedExport]
    public interface ISessionFactoryCreator : IDisposable {
        object ORMFactory { get; }
        event SessionCreatorEventHandler OnError;
    }

    public delegate void SessionCreatorEventHandler(object sender, SessionCreatorEventArgs e);

    public class SessionCreatorEventArgs : EventArgs {
        public SessionCreatorEventArgs(string description, string errorMessage) {
            Description = description;
            ErrorMessage = errorMessage;
        }
        public string Description { get; private set; }
        public string ErrorMessage { get; set; }
    }
}
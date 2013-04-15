#region Usings

using System;

#endregion

namespace LOB.Dao.Interface {
    public interface ISessionFactoryCreator : IDisposable {
        object ORMFactory { get; }
        event SessionCreatorEventHandler OnCreatingSession;
        event SessionCreatorEventHandler OnSessionCreated;
    }

    public delegate void SessionCreatorEventHandler(object sender, SessionCreatorEventArgs e);

    public class SessionCreatorEventArgs : EventArgs {
        public SessionCreatorEventArgs(string message) { Message = message; }
        public string Message { get; private set; }
    }
}
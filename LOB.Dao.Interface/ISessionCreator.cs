using System;

namespace LOB.Dao.Interface
{
    public interface ISessionCreator
    {
        object Orm { get; }
        event EventHandler OnCreatingSession;
        event EventHandler OnSessionCreated;
    }
}
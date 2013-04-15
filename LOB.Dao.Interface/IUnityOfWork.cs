#region Usings

using System;

#endregion

namespace LOB.Dao.Interface {
    public interface IUnityOfWork : IDisposable {
        /// <summary>
        ///     string Detail Description
        /// </summary>
        event SessionCreatorEventHandler OnError;
        IUnityOfWork BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void FlushTransaction();
        bool IsTransactionActive();
    }
}
#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Interface {
    public interface IUnityOfWork : IDisposable {

        /// <summary>
        ///     string Error Message
        /// </summary>
        event EventHandler<string> OnError;
        IUnityOfWork BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void FlushTransaction();
        bool IsTransactionActive();

    }
}
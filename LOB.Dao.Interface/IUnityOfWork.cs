#region Usings

using System;
using System.ComponentModel.Composition;

#endregion

namespace LOB.Dao.Interface {
    [InheritedExport]
    public interface IUnityOfWork : IDisposable {
        /// <summary>
        ///     Tests the database connection if it's working.
        ///     Invokes Event OnError if connection failed with error message.
        /// </summary>
        /// <returns>True if connection to the database sucessed</returns>
        bool TestConnection();
        event SessionCreatorEventHandler OnError;

        IUnityOfWork BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void FlushTransaction();
        bool IsTransactionActive();
    }
}
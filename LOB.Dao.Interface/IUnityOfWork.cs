#region Usings
using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Interface {
    public interface IUnityOfWork : IDisposable {

        /// <summary>
        ///     ORM's 'Session' goes in here.
        /// </summary>
        object ORM { get; }

        /// <summary>
        ///     string Error Message
        /// </summary>
        event EventHandler<string> OnError;

        void Save<T>(T entity) where T : BaseEntity;
        void SaveOrUpdate<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        IUnityOfWork BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
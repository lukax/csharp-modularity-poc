#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Interface
{
    [InheritedExport]
    public interface IUnityOfWork : IDisposable
    {
        /// <summary>
        ///     ORM's 'Session' goes in here.
        /// </summary>
        object Orm { get; }

        void Save<T>(T entity) where T : BaseEntity;
        void SaveOrUpdate<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
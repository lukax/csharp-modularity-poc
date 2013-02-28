#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using NHibernate;

#endregion

namespace LOB.Dao.Nhibernate
{
    public class UnityOfWork : IUnityOfWork, IDisposable
    {
        //TODO: Try and catches and some logging later..

        private ITransaction _transaction;

        [ImportingConstructor]
        public UnityOfWork(ISessionCreator sessionCreator)
        {
            Orm = sessionCreator.Orm;
        }

        public object Orm { get; private set; }

        public void Save<T>(T entity) where T : BaseEntity
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction not initialized");

            try
            {
                ((ISession) Orm).Save(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveOrUpdate<T>(T entity) where T : BaseEntity
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction not initialized");

            try
            {
                ((ISession) Orm).SaveOrUpdate(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction not initialized");

            try
            {
                ((ISession) Orm).Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction not initialized");

            try
            {
                ((ISession) Orm).Delete(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BeginTransaction()
        {
            if (_transaction == null)
                _transaction = ((ISession) Orm).BeginTransaction();
            else if (_transaction.IsActive)
                throw new InvalidOperationException("Transaction has already been initialized, dispose first");
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction not initialized");
            else if (!_transaction.IsActive)
                throw new InvalidOperationException("Transaction has not been activated, first Begin the Transaction");
            _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction not initialized");
            if (!_transaction.IsActive)
                throw new InvalidOperationException("Transaction has not been activated, first Begin the Transaction");
            _transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _transaction.Dispose();
            _transaction = null;
        }
    }
}
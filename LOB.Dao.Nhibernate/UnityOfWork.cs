#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using Microsoft.Practices.Unity;
using NHibernate;

#endregion

namespace LOB.Dao.Nhibernate
{
    public class UnityOfWork : IUnityOfWork, IDisposable
    {
        //TODO: Try and catches and some logging later..
        private Lazy<object> _lazyOrm;
        private ITransaction _transaction;

        [InjectionConstructor]
        public UnityOfWork(ISessionCreator sessionCreator)
        {
            _lazyOrm = new Lazy<object>(() => sessionCreator.Orm);
        }

        public object Orm
        {
            get { return _lazyOrm.Value; }
            private set { _lazyOrm = new Lazy<object>(() => value); }
        }

        public void Save<T>(T entity) where T : BaseEntity
        {
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
            try
            {
                ((ISession) Orm).Delete(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IUnityOfWork BeginTransaction()
        {
            if (_transaction.IsActive)
                throw new InvalidOperationException("Transaction has already been initialized, dispose first");
            return this;
        }

        public void CommitTransaction()
        {
            if (!_transaction.IsActive)
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
#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using NHibernate;

#endregion

namespace LOB.Dao.Nhibernate {
    public class UnityOfWork : IUnityOfWork {

        private readonly ILoggerFacade _loggerFacade;
        private readonly ISessionCreator _sessionCreator;
        //TODO: Try and catches and some logging later..
        private readonly Lazy<object> _lazyOrm;
        private ITransaction _transaction;

        [InjectionConstructor]
        public UnityOfWork(ISessionCreator sessionCreator, ILoggerFacade loggerFacade) {
            _sessionCreator = sessionCreator;
            _loggerFacade = loggerFacade;
            _lazyOrm = new Lazy<object>(() => sessionCreator.ORM);
        }

        public object ORM {
            get { return _lazyOrm.Value; }
        }

        public event EventHandler<string> OnError;

        public void Save<T>(T entity) where T : BaseEntity {
            try {
                ((ISession)ORM).Save(entity);
            } catch(Exception e) {
                _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
                if(OnError != null) OnError.Invoke(this, e.Message);
            }
        }

        public void SaveOrUpdate<T>(T entity) where T : BaseEntity {
            try {
                ((ISession)ORM).SaveOrUpdate(entity);
            } catch(Exception e) {
                _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
                if(OnError != null) OnError.Invoke(this, e.Message);
            }
        }

        public void Update<T>(T entity) where T : BaseEntity {
            try {
                ((ISession)ORM).Update(entity);
            } catch(Exception e) {
                _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
                if(OnError != null) OnError.Invoke(this, e.Message);
            }
        }

        public void Delete<T>(T entity) where T : BaseEntity {
            try {
                ((ISession)ORM).Delete(entity);
            } catch(Exception e) {
                _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
                if(OnError != null) OnError.Invoke(this, e.Message);
            }
        }

        public IUnityOfWork BeginTransaction() {
            if(_transaction == null) _transaction = ((ISession)ORM).BeginTransaction();
            else if(_transaction.IsActive) throw new InvalidOperationException("Transaction has already been initialized, dispose first");
            return this;
        }

        public void CommitTransaction() {
            if(_transaction == null) throw new InvalidOperationException("Transaction not initialized");
            if(!_transaction.IsActive) throw new InvalidOperationException("Transaction has not been activated, first Begin the Transaction");
            _transaction.Commit();
        }

        public void RollbackTransaction() {
            if(_transaction == null) throw new InvalidOperationException("Transaction not initialized");
            if(!_transaction.IsActive) throw new InvalidOperationException("Transaction has not been activated, first Begin the Transaction");
            _transaction.Rollback();
        }

        public void Dispose() {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(!disposing) return;
            _transaction.Dispose();
            _transaction = null;
        }

    }
}
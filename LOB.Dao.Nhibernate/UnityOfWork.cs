#region Usings
using System;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using NHibernate;

#endregion

namespace LOB.Dao.Nhibernate {
    public class UnityOfWork : IUnityOfWork, IDisposable {

        private readonly ILoggerFacade _loggerFacade;
        private readonly ISessionCreator _sessionCreator;
        //TODO: Try and catches and some logging later..
        private readonly Lazy<object> _lazyOrm;
        private ITransaction _transaction;

        [InjectionConstructor] public UnityOfWork(ISessionCreator sessionCreator, ILoggerFacade loggerFacade) {
            this._sessionCreator = sessionCreator;
            this._loggerFacade = loggerFacade;
            this._lazyOrm = new Lazy<object>(() => sessionCreator.ORM);
        }

        public object ORM {
            get { return this._lazyOrm.Value; }
        }

        public event EventHandler<string> OnError;

        public void Save<T>(T entity) where T : BaseEntity {
            try {
                ((ISession) this.ORM).Save(entity);
            }
            catch(Exception e) {
                this._loggerFacade.Log(e.Message, Category.Exception, Priority.High);
                if(this.OnError != null) this.OnError.Invoke(this, e.Message);
            }
        }

        public void SaveOrUpdate<T>(T entity) where T : BaseEntity {
            try {
                ((ISession) this.ORM).SaveOrUpdate(entity);
            }
            catch(Exception e) {
                this._loggerFacade.Log(e.Message, Category.Exception, Priority.High);
                if(this.OnError != null) this.OnError.Invoke(this, e.Message);
            }
        }

        public void Update<T>(T entity) where T : BaseEntity {
            try {
                ((ISession) this.ORM).Update(entity);
            }
            catch(Exception e) {
                this._loggerFacade.Log(e.Message, Category.Exception, Priority.High);
                if(this.OnError != null) this.OnError.Invoke(this, e.Message);
            }
        }

        public void Delete<T>(T entity) where T : BaseEntity {
            try {
                ((ISession) this.ORM).Delete(entity);
            }
            catch(Exception e) {
                this._loggerFacade.Log(e.Message, Category.Exception, Priority.High);
                if(this.OnError != null) this.OnError.Invoke(this, e.Message);
            }
        }

        public IUnityOfWork BeginTransaction() {
            if(this._transaction == null) this._transaction = ((ISession) this.ORM).BeginTransaction();
            if(this._transaction.IsActive) throw new InvalidOperationException("Transaction has already been initialized, dispose first");
            return this;
        }

        public void CommitTransaction() {
            if(this._transaction == null) throw new InvalidOperationException("Transaction not initialized");
            if(!this._transaction.IsActive) throw new InvalidOperationException("Transaction has not been activated, first Begin the Transaction");
            this._transaction.Commit();
        }

        public void RollbackTransaction() {
            if(this._transaction == null) throw new InvalidOperationException("Transaction not initialized");
            if(!this._transaction.IsActive) throw new InvalidOperationException("Transaction has not been activated, first Begin the Transaction");
            this._transaction.Rollback();
        }

        public void Dispose() {
            this.Dispose(true);
            //GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(!disposing) return;
            this._transaction.Dispose();
            this._transaction = null;
        }

    }
}
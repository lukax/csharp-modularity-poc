#region Usings

using System;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Linq;

#endregion

namespace LOB.Dao.Nhibernate {
    public class UnityOfWork : IUnityOfWork {

        private readonly ILoggerFacade _loggerFacade;
        //TODO: Try and catches and some logging later..
        private readonly Lazy<object> _lazyOrm;
        private ITransaction _transaction;
        public object ORM {
            get { return _lazyOrm.Value; }
        }
        protected ISessionCreator SessionCreator { get; set; }
        public event EventHandler<string> OnError;

        [InjectionConstructor]
        public UnityOfWork(ISessionCreator sessionCreator, ILoggerFacade loggerFacade) {
            SessionCreator = sessionCreator;
            _loggerFacade = loggerFacade;
            _lazyOrm = new Lazy<object>(() => sessionCreator.ORM.As<ISessionFactory>().OpenSession());
        }

        public IUnityOfWork BeginTransaction() {
            if(_transaction == null) _transaction = ORM.As<ISession>().BeginTransaction();
            else if(_transaction.IsActive) throw new InvalidOperationException(Strings.Notification_Dao_Transaction_AlreadyActivated);
            return this;
        }

        public void CommitTransaction() {
            if(_transaction == null) throw new InvalidOperationException(Strings.Notification_Dao_Transaction_NotInitialized);
            if(!_transaction.IsActive) throw new InvalidOperationException(Strings.Notification_Dao_Transaction_NotActivated);
            _transaction.Commit();
        }

        public void RollbackTransaction() {
            if(_transaction == null) throw new InvalidOperationException(Strings.Notification_Dao_Transaction_NotInitialized);
            if(!_transaction.IsActive) throw new InvalidOperationException(Strings.Notification_Dao_Transaction_NotActivated);
            _transaction.Rollback();
        }
        public void FlushTransaction() { ORM.As<ISession>().Flush(); }
        public bool IsTransactionActive() {
            if(_transaction == null) return false;
            return _transaction.IsActive;
        }
        #region Implementation of IDisposable

        ~UnityOfWork() { Dispose(false); }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(_transaction != null) if(_transaction.IsActive) _transaction.Rollback();
            if(!disposing) return;
            if(_transaction != null) _transaction.Dispose();
            ORM.As<ISession>().Dispose();
        }

        #endregion
        #region old functionality

        //public void Save<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORM).Save(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Message);
        //    }
        //}

        //public void SaveOrUpdate<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORM).SaveOrUpdate(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Message);
        //    }
        //}

        //public void Update<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORM).Update(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Message);
        //    }
        //}

        //public void Delete<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORM).Delete(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Message);
        //    }
        //}

        #endregion
    }
}
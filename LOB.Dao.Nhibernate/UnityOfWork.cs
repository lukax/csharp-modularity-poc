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

// ReSharper disable NotAccessedField.Local
        private readonly ILoggerFacade _loggerFacade;
// ReSharper restore NotAccessedField.Local
        //TODO: Try and catches and some logging later..
        private readonly Lazy<ISession> _lazyOrm;
        private ITransaction _transaction;
        public object ORM { get { return _lazyOrm.Value; } }
        protected ISessionFactoryCreator SessionFactoryCreator { get; set; }
        public event EventHandler<string> OnError;

        [InjectionConstructor]
        public UnityOfWork(ISessionFactoryCreator sessionFactoryCreator, ILoggerFacade loggerFacade) {
            SessionFactoryCreator = sessionFactoryCreator;
            _loggerFacade = loggerFacade;
            _lazyOrm = new Lazy<ISession>(() => sessionFactoryCreator.ORMFactory.As<ISessionFactory>().OpenSession());
        }

        public IUnityOfWork BeginTransaction() {
            if(_transaction == null) {
                if(!ORM.As<ISession>().IsConnected) ORM.As<ISession>().Reconnect();
                _transaction = ORM.As<ISession>().BeginTransaction();
            }
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
            if(_transaction != null) {
                _transaction.Dispose();
                _transaction = null;
            }
            ORM.As<ISession>().Disconnect();
            //ORMFactory.As<ISession>().Dispose();
        }

        #endregion
        #region old functionality

        //public void Save<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORMFactory).Save(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Message);
        //    }
        //}

        //public void SaveOrUpdate<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORMFactory).SaveOrUpdate(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Message);
        //    }
        //}

        //public void Update<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORMFactory).Update(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Message);
        //    }
        //}

        //public void Delete<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORMFactory).Delete(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Message, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Message);
        //    }
        //}

        #endregion
    }
}
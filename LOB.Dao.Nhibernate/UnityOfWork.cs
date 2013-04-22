#region Usings

using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using Microsoft.Practices.Prism.Logging;
using NHibernate;
using NHibernate.Linq;
using NullGuard;

#endregion

namespace LOB.Dao.Nhibernate {
    [Export(typeof(IUnityOfWork)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnityOfWork : IUnityOfWork {
        protected ILoggerFacade LoggerFacade { get; set; }
        protected ITransaction Transaction { get; set; }
        private object _orm;
        [AllowNull] public object ORM {
            get {
                return _orm ?? (_orm = Task.Run(() => {
                                                    try {
                                                        if(OnError != null) SessionFactoryCreator.OnError += OnError;
                                                        return SessionFactoryCreator.ORMFactory.As<ISessionFactory>().OpenSession();
                                                    } catch(NullReferenceException) {
                                                        return null;
                                                    }
                                                }).Result);
            }
        }
        protected ISessionFactoryCreator SessionFactoryCreator { get; set; }
        public bool TestConnection() {
            try {
                if(!ORM.As<ISession>().IsConnected) ORM.As<ISession>().Reconnect();
            } catch(NullReferenceException) {
                return false;
            }
            return true;
        }
        public event SessionCreatorEventHandler OnError;

        [ImportingConstructor]
        public UnityOfWork(ISessionFactoryCreator sessionFactoryCreator, ILoggerFacade loggerFacade) {
            SessionFactoryCreator = sessionFactoryCreator;
            LoggerFacade = loggerFacade;
        }

        public IUnityOfWork BeginTransaction() {
            if(Transaction == null) {
                if(ORM == null) return this;
                if(!ORM.As<ISession>().IsConnected) ORM.As<ISession>().Reconnect();
                Transaction = ORM.As<ISession>().BeginTransaction();
            }
            else if(Transaction.IsActive) throw new InvalidOperationException(Strings.Notification_Dao_Transaction_AlreadyActivated);
            return this;
        }

        public void CommitTransaction() {
            if(Transaction == null) throw new InvalidOperationException(Strings.Notification_Dao_Transaction_NotInitialized);
            if(!Transaction.IsActive) throw new InvalidOperationException(Strings.Notification_Dao_Transaction_NotActivated);
            Transaction.Commit();
        }

        public void RollbackTransaction() {
            if(Transaction == null) throw new InvalidOperationException(Strings.Notification_Dao_Transaction_NotInitialized);
            if(!Transaction.IsActive) throw new InvalidOperationException(Strings.Notification_Dao_Transaction_NotActivated);
            Transaction.Rollback();
        }
        public void FlushTransaction() { if(ORM != null) ORM.As<ISession>().Flush(); }
        public bool IsTransactionActive() {
            if(Transaction == null) return false;
            return Transaction.IsActive;
        }
        #region Implementation of IDisposable

        ~UnityOfWork() { Dispose(false); }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            //if(_transaction != null) if(_transaction.IsActive) _transaction.Rollback();
            if(!disposing) return;
            if(Transaction != null) {
                Transaction.Dispose();
                Transaction = null;
            }
            if(_orm != null) _orm.As<ISession>().Disconnect();
            //ORMFactory.As<ISession>().Dispose(); INFO: Disconnecting instead of disposing // Allows Multi-usage
        }

        #endregion
        #region old functionality

        //public void Save<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORMFactory).Save(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Description, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Description);
        //    }
        //}

        //public void SaveOrUpdate<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORMFactory).SaveOrUpdate(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Description, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Description);
        //    }
        //}

        //public void Update<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORMFactory).Update(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Description, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Description);
        //    }
        //}

        //public void Delete<T>(T entity) where T : BaseEntity {
        //    try {
        //        ((ISession)ORMFactory).Delete(entity);
        //    } catch(Exception e) {
        //        _loggerFacade.Log(e.Description, Category.Exception, Priority.High);
        //        if(OnError != null) OnError.Invoke(this, e.Description);
        //    }
        //}

        #endregion
    }
}
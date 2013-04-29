#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using Microsoft.Practices.Prism.Logging;
using NHibernate;
using NHibernate.Linq;

#endregion

namespace LOB.Dao.Nhibernate {
    [Export(typeof(IUnityOfWork)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnityOfWork : IUnityOfWork {
        private bool _isDisposed;
        private bool _isInitialized;
        private object _orm;
        protected ITransaction Transaction { get; set; }
        [Import] protected Lazy<ILoggerFacade> LoggerFacade { get; set; }
        [Import] protected Lazy<OrmFactory> OrmFactory { get; set; }
        public object Orm {
            get { return _orm ?? (_orm = OrmFactory.Value.Orm.As<ISession>()); }
        }

        public IUnityOfWork Initialize() {
            check_is_disposed();
            begin_new_Transaction();
            _isInitialized = true;
            return this;
        }

        public void Commit() {
            check_is_disposed();
            check_is_not_initialized();
            Transaction.Commit();
            begin_new_Transaction();
        }

        public void Rollback() {
            check_is_disposed();
            check_is_not_initialized();
            Transaction.Rollback();
            begin_new_Transaction();
        }
        public void Flush() {
            check_is_disposed();
            check_is_not_initialized();
            Orm.As<ISession>().Flush();
            begin_new_Transaction();
        }

        public bool IsInitialized() { return _isInitialized; }

        protected void begin_new_Transaction() {
            if(Transaction != null) Transaction.Dispose();
            Transaction = Orm.As<ISession>().BeginTransaction();
        }
        protected void check_is_not_initialized() { if(!_isInitialized) throw new InvalidOperationException("Must initialize (call Initialize()) on UnitOfWork before commiting or rolling back"); }
        protected void check_is_disposed() { if(_isDisposed) throw new ObjectDisposedException(GetType().Name); }
        protected void check_is_transaction_active() { if(!Transaction.IsActive) throw new InvalidOperationException(Strings.Notification_Dao_NotInitialized); }
        #region Implementation of IDisposable

        ~UnityOfWork() { Dispose(false); }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(_isDisposed || ! _isInitialized) return;
            Transaction.Dispose();
            Orm.As<ISession>().Dispose();
            _isDisposed = true;
        }

        #endregion
    }
}
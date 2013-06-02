#region Usings

using System;
using LOB.Business.Contract.Logic.Base;
using LOB.Dao.Contract;

#endregion

namespace LOB.Business.Logic.Base {
    public abstract class BaseEntityFacade : IBaseEntityFacade {
        protected BaseEntityFacade(IRepository repository) { Repository = repository; }
        #region Implementation of IDisposable

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~BaseEntityFacade() { Dispose(false); }
        private void Dispose(bool disposing) {
            if(Repository.Uow.IsInitialized()) Repository.Uow.Flush();
            if(!disposing) return;
            Repository.Uow.Dispose();
        }

        #endregion
        protected IRepository Repository { get; private set; }
    }
}
#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LOB.Business.Contract.Logic.Base;
using LOB.Dao.Contract;
using LOB.Dao.Contract.Exception.Database;
using LOB.Domain.Base;
using NullGuard;

#endregion

namespace LOB.Business.Logic.Base {
    public abstract class BaseEntityFacade<TEntity> : IBaseEntityFacade<TEntity> where TEntity : BaseEntity, new() {
        private TEntity _entity;
        protected IRepository Repository { get; private set; }
        protected BaseEntityFacade(IRepository repository) {
            Repository = repository;
        }
        

        public virtual TEntity GenerateEntity() { //TODO: Substitude initial entity generation with repository.Load()
            var entity = new TEntity();
            return entity;
        }

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
        #region Implementation of IBaseEntityFacade

        public Tuple<bool, IEnumerable<ValidationResult>> CanAdd() { throw new NotImplementedException(); }
        public Tuple<bool, IEnumerable<ValidationResult>>  CanUpdate() { throw new NotImplementedException(); }
        public Tuple<bool, IEnumerable<ValidationResult>> CanDelete() { throw new NotImplementedException(); }

        #endregion
    }
}
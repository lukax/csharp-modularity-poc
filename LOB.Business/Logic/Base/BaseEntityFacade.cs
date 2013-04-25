#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOB.Business.Contract.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic.Base {
    public abstract class BaseEntityFacade<TEntity> : IBaseEntityFacade<TEntity> where TEntity : BaseEntity, new() {
        private TEntity _entity;
        private List<ValidationDelegate> ValidationDelegates { get; set; }
        private List<ValidationResult> ValidationResults { get; set; }
        protected IRepository Repository { get; private set; }

        protected BaseEntityFacade(IRepository repository) {
            Repository = repository;
            ValidationResults = new List<ValidationResult>();
            ValidationDelegates = new List<ValidationDelegate>();
        }

        public TEntity Entity { //INFO: U probably dont wanna override this trust me
            protected get { return _entity; }
            set {
                _entity = value;
                if(ReferenceEquals(value, null)) return;
                _entity.ValidationFunc += s => {
                                              var val = GetValidations(s).FirstOrDefault();
                                              return val != null ? val.ErrorDescription : null;
                                          };
            }
        }

        public virtual TEntity GenerateEntity() { //TODO: Substitude initial entity generation with repository.Load()
            var entity = new TEntity();
            Task.Run(() => EntityPreprocessors(entity));
            return entity;
        }

        private void EntityPreprocessors(TEntity entity) { //TODO: Substitute with object interceptor in nhibernate 
            using(Repository.Uow.BeginTransaction()) entity.Code = Repository.Count<TEntity>() + 1;
        }

        public virtual Tuple<bool, IEnumerable<ValidationResult>> CanAdd() {
            IEnumerable<ValidationResult> results;
            bool result = ProcessBasicValidations(out results);
            return new Tuple<bool, IEnumerable<ValidationResult>>(result, results);
        }

        public virtual Tuple<bool, IEnumerable<ValidationResult>> CanUpdate() {
            IEnumerable<ValidationResult> results;
            bool result = ProcessBasicValidations(out results);
            return new Tuple<bool, IEnumerable<ValidationResult>>(result, results);
        }

        public virtual Tuple<bool, IEnumerable<ValidationResult>> CanDelete() {
            IEnumerable<ValidationResult> results;
            bool result = ProcessBasicValidations(out results);
            return new Tuple<bool, IEnumerable<ValidationResult>>(result, results);
        }

        protected virtual bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            invalidFields = ValidationResults;
            if(
                ValidationResults.Where(validationResult => validationResult != null)
                                 .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

        protected void AddValidation(ValidationDelegate func) { ValidationDelegates.Add(func); }
        protected void RemoveValidation(ValidationDelegate func) { if(ContainsValidation(func)) ValidationDelegates.Remove(func); }
        protected bool ContainsValidation(ValidationDelegate func) { return ValidationDelegates.Contains(func); }
        protected IEnumerable<ValidationResult> GetValidations(string propertyName) {
            var propName = propertyName;
            return
                ValidationDelegates.Select(validationDel => validationDel(this, propertyName))
                                   .Where(result => result != null)
                                   .Where(result => result.FieldName == propName);
        }
        #region Implementation of IDisposable

        ~BaseEntityFacade() { Dispose(false); }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool b) {
            if(Repository.Uow.IsTransactionActive()) Repository.Uow.FlushTransaction();
            if(!b) return;
            Repository.Uow.Dispose();
        }

        #endregion
    }
}
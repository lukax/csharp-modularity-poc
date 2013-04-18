#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LOB.Business.Interface.Logic.Base;
using LOB.Dao.Interface;
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
            entity.Code = Repository.Count<TEntity>() + 1;
        }

        public virtual bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
        }

        public virtual bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
        }

        public virtual bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
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
    }
}
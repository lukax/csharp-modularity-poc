using System.Collections.Generic;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Domain;
using LOB.Domain.Logic;

namespace LOB.Business.Logic {
    public class SaleFacade : ISaleFacade
    {
        #region Implementation of IBaseEntityFacade

        void IBaseEntityFacade.SetEntity<T>(T entity) { throw new System.NotImplementedException(); }
        public Sale GenerateEntity() { throw new System.NotImplementedException(); }
        void ISaleFacade.SetEntity<T>(T entity) { throw new System.NotImplementedException(); }
        public void ConfigureValidations() { throw new System.NotImplementedException(); }
        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) { throw new System.NotImplementedException(); }
        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) { throw new System.NotImplementedException(); }
        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) { throw new System.NotImplementedException(); }

        #endregion
    }
}
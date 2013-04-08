#region Usings

using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Domain;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    public class SaleFacade : ISaleFacade {
        #region Implementation of IBaseEntityFacade

        void IBaseEntityFacade.SetEntity<T>(T entity) { throw new NotImplementedException(); }
        public Sale GenerateEntity() { throw new NotImplementedException(); }
        void ISaleFacade.SetEntity<T>(T entity) { throw new NotImplementedException(); }
        public void ConfigureValidations() { throw new NotImplementedException(); }
        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) { throw new NotImplementedException(); }
        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) { throw new NotImplementedException(); }
        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) { throw new NotImplementedException(); }

        #endregion
    }
}
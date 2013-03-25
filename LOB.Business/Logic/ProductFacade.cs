#region Usings



#endregion

using System.Collections.Generic;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Domain;
using LOB.Domain.Logic;

namespace LOB.Business.Logic
{
    public class ProductFacade : IProductFacade
    {
        private readonly IServiceFacade _serviceFacade;

        public ProductFacade(IServiceFacade serviceFacade)
        {
            _serviceFacade = serviceFacade;
        }

        private Product _entity;
        public void SetEntity<T>(T entity) where T : Product
        {
            _entity = entity;
        }

        public void ConfigureValidations()
        {
            _serviceFacade.ConfigureValidations();
            if (_entity != null)
            {
                //Validations for product later..        
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        void IBaseEntityFacade.SetEntity<T>(T entity)
        {
            ((IBaseEntityFacade)_serviceFacade).SetEntity(entity);
        }

        void IServiceFacade.SetEntity<T>(T entity)
        {
            ((IServiceFacade)_serviceFacade).SetEntity(entity);
        }
    }
}
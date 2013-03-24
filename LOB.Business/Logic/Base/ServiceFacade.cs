#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.Base;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace LOB.Business.Logic.Base
{
    public class ServiceFacade<TEntity> : IServiceFacade<TEntity> where TEntity : Service
    {
        private readonly IBaseEntityFacade<BaseEntity> _baseEntityFacade;

        public ServiceFacade(IBaseEntityFacade<BaseEntity> baseEntityFacade)
        {
            _baseEntityFacade = baseEntityFacade;
        }

        public bool CanAdd(TEntity entity, out IEnumerable<InvalidField> invalidFields)
        {
            IEnumerable<InvalidField> baseEntityFields;
            bool result = _baseEntityFacade.CanAdd(entity, out baseEntityFields);
            IList<InvalidField> fields = new List<InvalidField>(baseEntityFields);
            if (string.IsNullOrEmpty(entity.Description))
            {
                fields.Add(new InvalidField(Strings.Common_Name, Strings.Error_Field_Empty));
                result = false;
            }
            if (string.IsNullOrEmpty(entity.Name))
            {
                fields.Add(new InvalidField(Strings.Common_Id, Strings.Error_Field_Empty));
                result = false;
            }
            invalidFields = fields;
            return result;
        }

        public bool CanUpdate(TEntity entity, out IEnumerable<InvalidField> invalidFields)
        {
            IEnumerable<InvalidField> baseEntityFields;
            bool result = _baseEntityFacade.CanAdd(entity, out baseEntityFields);
            IList<InvalidField> fields = new List<InvalidField>(baseEntityFields);
            if (string.IsNullOrEmpty(entity.Description))
            {
                fields.Add(new InvalidField(Strings.Common_Name, Strings.Error_Field_Empty));
                result = false;
            }
            if (string.IsNullOrEmpty(entity.Name))
            {
                fields.Add(new InvalidField(Strings.Common_Id, Strings.Error_Field_Empty));
                result = false;
            }
            invalidFields = fields;
            return result;
        }

        public bool CanDelete(TEntity entity, out IEnumerable<InvalidField> invalidFields)
        {
            IEnumerable<InvalidField> baseEntityFields;
            bool result = _baseEntityFacade.CanAdd(entity, out baseEntityFields);
            IList<InvalidField> fields = new List<InvalidField>(baseEntityFields);
            if (string.IsNullOrEmpty(entity.Description))
            {
                fields.Add(new InvalidField(Strings.Common_Name, Strings.Error_Field_Empty));
                result = false;
            }
            if (string.IsNullOrEmpty(entity.Name))
            {
                fields.Add(new InvalidField(Strings.Common_Id, Strings.Error_Field_Empty));
                result = false;
            }
            invalidFields = fields;
            return result;
        }
    }
}
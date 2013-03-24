#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace LOB.Business.Logic.Base
{
    public abstract class ServiceFacade<TEntity> : IServiceFacade<TEntity> where TEntity : Service
    {
        private TEntity _entity;
        public virtual TEntity Entity
        {
            get { return _entity; }
            set
            {
                if (_entity == value) return;  
                _entity = value;
                AddValidators(_entity);        
            }
        }

        protected void AddValidators(Service s)
        {
            s.AddValidation((sender, name) =>
                {
                    if (s.Name.Length < 3)
                    {
                        return new ValidationResult("Name", Strings.Error_Field_Empty);
                    }
                    return null;
                }); 
            s.AddValidation((sender, name) =>
                {
                    if (s.Description.Length < 3)
                    {
                        return new ValidationResult("Description", Strings.Error_Field_Empty);
                    }
                    return null;
                });
        }

        public virtual bool CanAdd(out IEnumerable<InvalidField> invalidFields)
        {
            throw new NotImplementedException();
        }

        public virtual bool CanUpdate(out IEnumerable<InvalidField> invalidFields)
        {
            throw new NotImplementedException();
        }

        public virtual bool CanDelete(out IEnumerable<InvalidField> invalidFields)
        {
            throw new NotImplementedException();
        }


        public abstract void GenerateEntity();
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using NullGuard;

namespace LOB.Business.Logic.Base
{
    public abstract class BaseEntityFacade : IBaseEntityFacade<BaseEntity>
    {
        private BaseEntity _entity;

        public BaseEntity Entity
        {
            get { return _entity; }
            set
            {
                if (_entity == value) return;
                _entity = value;
                AddValidation();
            }
        }

        private void AddValidation()
        {
            
        }

        public bool CanAdd(out IEnumerable<InvalidField> invalidFields)
        {
            throw new NotImplementedException();
        }

        public bool CanUpdate(out IEnumerable<InvalidField> invalidFields)
        {
            throw new NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<InvalidField> invalidFields)
        {
            throw new NotImplementedException();
        }


    }
}

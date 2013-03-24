using System;
using System.Collections.Generic;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.Base;
using NullGuard;

namespace LOB.Business.Logic.Base
{
    public class BaseEntityFacade: IBaseEntityFacade<BaseEntity>
    {
        public bool CanAdd(BaseEntity entity, out IEnumerable<InvalidField> invalidFields)
        {
            var fields = new List<InvalidField>();
            bool result = true;
            if (entity.Code != default(int))
            {
                fields.Add(new InvalidField(Strings.Common_Code,Strings.Error_Field_NotEmpty));
                result = false;
            }
            if (entity.Id != default(Guid))
            {
                fields.Add(new InvalidField(Strings.Common_Id, Strings.Error_Field_NotEmpty));
                result = false;
            }
            invalidFields = fields;
            return result;
        }

        public bool CanUpdate(BaseEntity entity, out IEnumerable<InvalidField> invalidFields )
        {
            var fields = new List<InvalidField>();
            bool result = true;
            if (entity.Code == default(int))
            {
                fields.Add(new InvalidField(Strings.Common_Code, Strings.Error_Field_Empty));
                result = false;
            }
            if (entity.Id == default(Guid))
            {
                fields.Add(new InvalidField(Strings.Common_Id, Strings.Error_Field_Empty));
                result = false;
            }
            invalidFields = fields;
            return result;
        }

        public bool CanDelete(BaseEntity entity, out IEnumerable<InvalidField> invalidFields)
        {
            var fields = new List<InvalidField>();
            bool result = true;
            if (entity.Code == default(int))
            {
                fields.Add(new InvalidField(Strings.Common_Code, Strings.Error_Field_Empty));
                result = false;
            }
            if (entity.Id == default(Guid))
            {
                fields.Add(new InvalidField(Strings.Common_Id, Strings.Error_Field_Empty));
                result = false;
            }
            invalidFields = fields;
            return result;
        }
    }
}

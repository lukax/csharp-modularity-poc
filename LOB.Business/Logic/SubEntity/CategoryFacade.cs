using System.Collections.Generic;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Domain.SubEntity;

namespace LOB.Business.Logic.SubEntity
{
    public class CategoryFacade : ICategoryFacade
    {
        public bool CanAdd(Category entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanUpdate(Category entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanDelete(Category entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }
    }
}

#region Usings

using System.Collections.Generic;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic;
using LOB.Business.Logic.Base;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Logic
{
    public class ProductFacade :  IProductFacade
    {
        public bool CanAdd(Product entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanUpdate(Product entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanDelete(Product entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }
    }
}
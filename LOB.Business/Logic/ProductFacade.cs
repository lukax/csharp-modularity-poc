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
        public Product Entity { get; set; }
        public bool CanAdd(out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanUpdate(out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public void GenerateEntity()
        {
            throw new System.NotImplementedException();
        }
    }
}
#region Usings



#endregion

using System.Collections.Generic;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic;
using LOB.Business.Logic.Base;
using LOB.Domain;

namespace LOB.Business.Logic
{
    public class SaleFacade :ISaleFacade
    {
        public Sale Entity { get; set; }
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
    }
}
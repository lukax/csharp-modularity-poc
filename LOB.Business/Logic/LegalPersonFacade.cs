using System.Collections.Generic;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Logic.Base;
using LOB.Domain;
using LOB.Domain.Base;

namespace LOB.Business.Logic
{
    public class LegalPersonFacade :  ILegalPersonFacade
    {
        public LegalPerson Entity { get; set; }
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
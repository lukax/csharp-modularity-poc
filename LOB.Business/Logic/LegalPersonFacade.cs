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
        public bool CanAdd(LegalPerson entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanUpdate(LegalPerson entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanDelete(LegalPerson entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }
    }
}
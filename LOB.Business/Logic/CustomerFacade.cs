﻿#region Usings

using System.Collections.Generic;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic;
using LOB.Business.Logic.Base;
using LOB.Domain;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Logic
{
    public class CustomerFacade : ICustomerFacade
    {
        public bool CanAdd(Customer entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanUpdate(Customer entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanDelete(Customer entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new System.NotImplementedException();
        }
    }
}
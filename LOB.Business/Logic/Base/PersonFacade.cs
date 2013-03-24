#region Usings

using System;
using System.Collections.Generic;
using LOB.Business.Interface;
using LOB.Business.Interface.Logic.Base;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.Base
{
    public class PersonFacade<TEntity> : IPersonFacade<TEntity> where TEntity : Person
    {
        public bool CanAdd(TEntity entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new NotImplementedException();
        }

        public bool CanUpdate(TEntity entity, out IEnumerable<InvalidField> invalidFields)
        {

            throw new NotImplementedException();

        }

        public bool CanDelete(TEntity entity, out IEnumerable<InvalidField> invalidFields)
        {
            throw new NotImplementedException();
        }
    }
}
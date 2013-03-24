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
    public abstract class PersonFacade<TEntity> : IPersonFacade<TEntity> where TEntity : Person
    {
        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public string Error { get; private set; }
        public TEntity Entity { get; set; }
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
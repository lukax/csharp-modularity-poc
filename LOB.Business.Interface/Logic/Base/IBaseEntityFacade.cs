using System.Collections.Generic;
using System.ComponentModel;
using LOB.Domain.Base;

namespace LOB.Business.Interface.Logic.Base
{
    public interface IBaseEntityFacade<TEntity> where TEntity : BaseEntity
    {
        TEntity Entity { get; set; }
        bool CanAdd(out IEnumerable<InvalidField> invalidFields);
        bool CanUpdate(out IEnumerable<InvalidField> invalidFields);
        bool CanDelete(out IEnumerable<InvalidField> invalidFields);
    }
}
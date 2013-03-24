using System.Collections.Generic;
using LOB.Domain.Base;

namespace LOB.Business.Interface.Logic.Base
{
    public interface IBaseEntityFacade <in TEntity> where TEntity:BaseEntity
    {
        bool CanAdd(TEntity entity, out IEnumerable<InvalidField> invalidFields);
        bool CanUpdate(TEntity entity,out IEnumerable<InvalidField> invalidFields);
        bool CanDelete(TEntity entity, out IEnumerable<InvalidField> invalidFields);
    }
}
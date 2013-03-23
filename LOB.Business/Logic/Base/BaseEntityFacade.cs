using LOB.Business.Interface.Logic.Base;
using LOB.Domain.Base;

namespace LOB.Business.Logic.Base
{
    public class BaseEntityFacade: IBaseEntityFacade
    {
        public bool CanAlter<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            throw new System.NotImplementedException();
        }

        public bool CanDelete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            throw new System.NotImplementedException();
        }
    }
}

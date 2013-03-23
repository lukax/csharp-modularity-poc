using LOB.Domain.Base;

namespace LOB.Business.Interface.Logic.Base
{
    public interface IBaseEntityFacade <in TEntity> where TEntity:BaseEntity
    {
        bool CanAlter(TEntity entity);
        bool CanDelete(TEntity entity) ;
    }
}
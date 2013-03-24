using LOB.Domain.Base;

namespace LOB.Business.Interface.Logic.Base
{
    public interface IPersonFacade<TEntity> : IBaseEntityFacade<TEntity> where TEntity:Person
    {

    }
}
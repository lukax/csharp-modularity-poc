using LOB.Domain.Base;

namespace LOB.Business.Interface.Logic.Base
{
    public interface IPersonFacade<in TEntity> : IBaseEntityFacade<TEntity> where TEntity:Person
    {

    }
}
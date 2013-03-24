using LOB.Domain.Base;

namespace LOB.Business.Interface.Logic.Base
{
    public interface IServiceFacade<in TEntity> : IBaseEntityFacade<TEntity> where TEntity:Service
    {
         
    }
}
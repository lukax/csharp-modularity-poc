using LOB.Domain.Base;

namespace LOB.Business.Interface.Logic.Base
{
    public interface IServiceFacade<TEntity> : IBaseEntityFacade<TEntity> where TEntity:Service
    {
        void GenerateEntity();
    }
}
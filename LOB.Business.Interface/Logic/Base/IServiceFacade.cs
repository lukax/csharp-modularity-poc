#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.Business.Interface.Logic.Base {
    public interface IServiceFacade : IBaseEntityFacade {

        new void SetEntity<T>(T entity) where T : Service;
        Service GenerateEntity();

    }
}
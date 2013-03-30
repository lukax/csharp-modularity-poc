#region Usings
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Interface.Logic.Base {
    public interface IPersonFacade : IBaseEntityFacade {

        new void SetEntity<T>(T entity) where T : Person;

    }
}
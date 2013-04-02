#region Usings

using LOB.Business.Interface.Logic.Base;
using LOB.Domain;

#endregion

namespace LOB.Business.Interface.Logic {
    public interface ISaleFacade : IBaseEntityFacade {

        new void SetEntity<T>(T entity) where T : Sale;
        Sale GenerateEntity();

    }
}
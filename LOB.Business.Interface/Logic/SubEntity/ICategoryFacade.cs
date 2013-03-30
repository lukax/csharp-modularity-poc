#region Usings

using LOB.Business.Interface.Logic.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Interface.Logic.SubEntity {
    public interface ICategoryFacade : IServiceFacade {

        new void SetEntity<T>(T entity) where T : Category;
        new Category GenerateEntity();

    }
}
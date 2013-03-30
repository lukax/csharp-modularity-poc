#region Usings
using LOB.Business.Interface.Logic.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Interface.Logic.SubEntity {
    public interface IEmailFacade : IBaseEntityFacade {

        new void SetEntity<T>(T entity) where T : Email;

    }
}
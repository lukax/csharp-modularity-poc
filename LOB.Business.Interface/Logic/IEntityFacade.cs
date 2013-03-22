#region Usings

using LOB.Dao.Interface;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Interface.Logic
{
    public interface IEntityFacade<T> where T : BaseEntity
    {
        IUnityOfWork UnityOfWork { get; }
        T Entity { get; }
    }
}
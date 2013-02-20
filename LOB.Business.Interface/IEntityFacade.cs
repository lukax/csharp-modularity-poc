#region Usings

using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Interface
{
    [InheritedExport]
    public interface IEntityFacade<T> where T : BaseEntity
    {
        IUnityOfWork UnityOfWork { get; }
        T Entity { get; }
    }
}
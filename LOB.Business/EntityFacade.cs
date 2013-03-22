#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.Business
{
    public class EntityFacade<T> : IEntityFacade<T> where T : BaseEntity
    {
        public EntityFacade(IUnityOfWork unityOfWork, T entity)
        {
            UnityOfWork = unityOfWork;
            Entity = entity;
        }

        public IUnityOfWork UnityOfWork { get; private set; }

        public T Entity { get; private set; }
    }
}
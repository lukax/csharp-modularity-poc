#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface;
using LOB.Dao.Interface;
using LOB.Domain.Base;

#endregion

namespace LOB.Business
{
    public class EntityFacade<T> : IEntityFacade<T> where T : BaseEntity
    {
        public EntityFacade(T entity)
        {
            Entity = entity;
        }

        [Import]
        public IUnityOfWork UnityOfWork { get; private set; }
        public T Entity { get; private set; }
    }
}
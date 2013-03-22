﻿#region Usings

using LOB.Business.Interface;
using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Logic
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
#region Usings

using System;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.Infrastructure {
    public class EntityIncludeEvent<TEntity> : CompositePresentationEvent<EntityIncludePayload<TEntity>> where TEntity : BaseEntity {}

    public class EntityIncludePayload<TEntity> where TEntity : BaseEntity {
        public Guid ViewId { get; private set; }
        public TEntity Entity { get; private set; }
        public EntityIncludePayload(Guid viewId, TEntity entity) {
            ViewId = viewId;
            Entity = entity;
        }
    }
}
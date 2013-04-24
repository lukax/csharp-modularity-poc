#region Usings

using System;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.Infrastructure {
    public class EntityChangedEvent<TEntity> : CompositePresentationEvent<EntityChangedPayload<TEntity>> where TEntity : BaseEntity {}

    public class EntityChangedPayload<TEntity> where TEntity : BaseEntity {
        public TEntity Entity { get; private set; }
        public Guid ViewId { get; private set; }
        public EntityChangedPayload(Guid viewId, TEntity entity) {
            Entity = entity;
            ViewId = viewId;
        }
    }
}
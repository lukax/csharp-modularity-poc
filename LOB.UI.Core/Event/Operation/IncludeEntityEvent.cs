#region Usings

using System;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.Operation {
    public class IncludeEntityEvent : CompositePresentationEvent<IncludeEntityPayload> {
        
    }

    public struct IncludeEntityPayload {
        private readonly Guid _viewId;
        private readonly BaseEntity _entity;
        public Guid ViewId {
            get { return _viewId; }
        }
        public BaseEntity Entity {
            get { return _entity; }
        }
        public IncludeEntityPayload(Guid viewId, BaseEntity entity) {
            _viewId = viewId;
            _entity = entity;
        }
    }
}
#region Usings

using System;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.View {
    public class SetupChildViewEvent : CompositePresentationEvent<SetupChildPayload> {}

    public class SetupChildPayload {
        public Guid OldId { get; set; }
        public Guid NewId { get; set; }
        public BaseEntity Entity { get; set; }
        public SetupChildPayload(Guid oldId, Guid newId, BaseEntity entity) {
            OldId = oldId;
            NewId = newId;
            Entity = entity;
        }
    }
}
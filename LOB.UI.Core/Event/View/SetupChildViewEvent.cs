#region Usings

using System;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.View {
    public class SetupChildViewEvent : CompositePresentationEvent<SetupChildPayload> {}

    public class SetupChildPayload {
        private BaseEntity _entity;
        public Guid OldId { get; set; }
        public Guid NewId { get; set; }
        public BaseEntity Entity {
            get { return _entity; }
            set {
                _entity = value;
                if(OnEntityChanged != null) OnEntityChanged(value);
            }
        }
        public Action<BaseEntity> OnEntityChanged;

        public SetupChildPayload(Guid oldId, Guid newId, BaseEntity entity) {
            OldId = oldId;
            NewId = newId;
            Entity = entity;
        }
    }
}
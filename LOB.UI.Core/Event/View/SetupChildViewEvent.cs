#region Usings

using System;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.View {
    public class SetupChildViewEvent : CompositePresentationEvent<SetupChildPayload> {}

    public class SetupChildPayload {
        public Guid OldId { get; set; }
        public Guid NewId { get; set; }
        public SetupChildPayload(Guid oldId, Guid newId) {
            OldId = oldId;
            NewId = newId;
        }
    }
}
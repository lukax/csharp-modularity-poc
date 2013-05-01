#region Usings

using System;
using LOB.Domain.Base;
using LOB.UI.Contract.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.View {
    public class CloseViewEvent : CompositePresentationEvent<CloseViewPayload>, IBaseEvent {}

    public class CloseViewPayload {
        public Guid ViewId { get; set; }
        public BaseEntity OperatedEntity { get; set; }
        public CloseViewPayload(Guid viewId, BaseEntity operatedEntity = null) {
            ViewId = viewId;
            OperatedEntity = operatedEntity;
        }
    }
}
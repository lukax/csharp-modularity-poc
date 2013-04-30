#region Usings

using System;
using LOB.UI.Contract.Event;
using LOB.UI.Contract.Infrastructure;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.View {
    public class OpenViewEvent : CompositePresentationEvent<OpenViewPayload>, IBaseEvent {}

    public class OpenViewTEvent : CompositePresentationEvent<OpenViewTPayload>, IBaseEvent {}

    public class OpenViewPayload {
        public IViewInfo ViewInfo { get; private set; }
        public Type ViewType { get; private set; }
        public Action<Guid> GetIdFunc { get; private set; }
        public OpenViewPayload(IViewInfo viewInfo, Action<Guid> getIdFunc = null) {
            ViewInfo = viewInfo;
            GetIdFunc = getIdFunc;
        }
    }

    public class OpenViewTPayload {
        public Type ViewType { get; private set; }
        public Action<Guid> GetIdFunc { get; private set; }
        public OpenViewTPayload(Type viewInfo, Action<Guid> getIdFunc = null) {
            ViewType = viewInfo;
            GetIdFunc = getIdFunc;
        }
    }
}
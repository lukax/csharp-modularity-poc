#region Usings

using System;
using LOB.UI.Interface.Event;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.View {
    public class OpenViewEvent : CompositePresentationEvent<OpenViewPayload>, IBaseEvent {}

    public class OpenViewPayload {
        public IViewInfo ViewInfo { get; private set; }
        public Type ViewType { get; private set; }
        public Action<Guid> GetIdFunc { get; private set; }
        public OpenViewPayload(IViewInfo viewInfo, Action<Guid> getIdFunc = null) {
            ViewInfo = viewInfo;
            GetIdFunc = getIdFunc;
        }
        public OpenViewPayload(Type viewType, Action<Guid> getIdFunc = null) {
            ViewType = viewType;
            GetIdFunc = getIdFunc;
        }
    }
}
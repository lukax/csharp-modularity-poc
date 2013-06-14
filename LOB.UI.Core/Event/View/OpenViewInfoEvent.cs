#region Usings

using System;
using LOB.UI.Contract.Event;
using LOB.UI.Contract.Infrastructure;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.View {
    public class OpenViewEvent : CompositePresentationEvent<OpenViewPayload>, IBaseEvent {}

    public class OpenViewPayload {
        public Type ViewType { get; private set; }
        public ViewState ViewState { get; private set; }
        public Action<Guid> GetIdFunc { get; private set; }
        public OpenViewPayload(Type viewInfo, Action<Guid> getIdFunc = null, ViewState viewState = ViewState.Add) {
            ViewType = viewInfo;
            GetIdFunc = getIdFunc;
            ViewState = viewState;
        }
    }

    public class OpenViewInfoEvent : CompositePresentationEvent<OpenViewInfoPayload>, IBaseEvent {}

    public class OpenViewInfoPayload {
        public IViewInfo ViewInfo { get; private set; }
        public Action<Guid> GetIdFunc { get; private set; }
        public OpenViewInfoPayload(IViewInfo viewInfo, Action<Guid> getIdFunc = null) {
            ViewInfo = viewInfo;
            GetIdFunc = getIdFunc;
        }
    }
}
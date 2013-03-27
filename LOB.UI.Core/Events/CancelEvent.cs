#region Usings

using LOB.UI.Interface.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Events {
    public class CancelEvent : CompositePresentationEvent<string>, IBaseEvent {
        public CancelEvent() {
        }
    }
}
#region Usings



#endregion

using LOB.UI.Interface.Event;
using Microsoft.Practices.Prism.Events;

namespace LOB.UI.Core.Events {
    public class MessageHideEvent : CompositePresentationEvent<string>, IBaseEvent {
    }
}
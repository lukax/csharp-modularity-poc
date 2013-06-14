#region Usings

using LOB.Domain.Logic;
using LOB.UI.Contract.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event {
    public class NotificationEvent : CompositePresentationEvent<Notification>, IBaseEvent {}
}
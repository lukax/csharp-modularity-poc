#region Usings

using System;
using LOB.UI.Interface.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.Infrastructure {
    public class CancelEvent : CompositePresentationEvent<Guid>, IBaseEvent {}
}
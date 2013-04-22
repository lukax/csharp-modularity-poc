#region Usings

using System;
using LOB.UI.Interface.Event;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.Operation {
    public class RefreshEvent : CompositePresentationEvent<Guid>, IBaseEvent {}
}
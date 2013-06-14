#region Usings

using System;
using LOB.UI.Contract.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.Infrastructure {
    public class RefreshEvent : CompositePresentationEvent<Guid>, IBaseEvent {}
}
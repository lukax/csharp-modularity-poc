#region Usings

using System;
using LOB.UI.Contract.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.View {
    public class CloseViewEvent : CompositePresentationEvent<Guid>, IBaseEvent {}
}
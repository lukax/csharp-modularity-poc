﻿#region Usings

using LOB.Domain.Logic;
using LOB.UI.Interface.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Events {
    public class NotificationEvent : CompositePresentationEvent<Notification>, IBaseEvent {

    }

}
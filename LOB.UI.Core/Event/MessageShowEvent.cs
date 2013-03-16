#region Usings

using LOB.UI.Interface.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event
{
    /// <summary>
    ///     Event that requests a Customer
    ///     The TPayload of the event represents the Customer which will be passed to the subscriber to the event.
    /// </summary>
    public class MessageShowEvent : CompositePresentationEvent<string>, IBaseEvent
    {
    }
}
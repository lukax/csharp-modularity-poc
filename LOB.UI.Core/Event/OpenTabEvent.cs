#region Usings

using LOB.UI.Interface.Event;
using Microsoft.Practices.Prism.Events;
using LOB.UI.Interface.Infrastructure;
#endregion

namespace LOB.UI.Core.Event
{
    public class OpenTabEvent : CompositePresentationEvent<OperationType>, IBaseEvent
    {
    }
}
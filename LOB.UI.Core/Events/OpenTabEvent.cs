#region Usings

using LOB.UI.Interface.Events;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Events
{
    public class OpenTabEvent : CompositePresentationEvent<string>, IBaseEvent
    {
    }
}
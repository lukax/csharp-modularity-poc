#region Usings

using LOB.UI.Interface.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event
{
    public class QuitEvent : CompositePresentationEvent<string>, IBaseEvent
    {
        public QuitEvent()
        {
        }
    }
}
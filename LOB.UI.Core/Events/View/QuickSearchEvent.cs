#region Usings

using LOB.UI.Interface.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Events.View
{
    public class QuickSearchEvent : CompositePresentationEvent<string>, IBaseEvent
    {
        public QuickSearchEvent()
        {
        }
    }
}
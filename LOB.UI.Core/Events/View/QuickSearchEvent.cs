#region Usings
using LOB.UI.Interface.Event;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Events.View {
    public class QuickSearchEvent : CompositePresentationEvent<OperationType>, IBaseEvent {

    }
}
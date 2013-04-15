#region Usings

using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Events.Operation {
    /// <summary>
    ///     object view
    /// </summary>
    public class PersonTypeChangedEvent : CompositePresentationEvent<ViewID> {}
}
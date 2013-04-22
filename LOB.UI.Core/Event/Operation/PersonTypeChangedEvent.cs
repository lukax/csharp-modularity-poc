#region Usings

using System;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Event.Operation {
    /// <summary>
    ///     object view
    /// </summary>
    public class PersonTypeChangedEvent : CompositePresentationEvent<Guid> {}
}
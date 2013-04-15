#region Usings

using LOB.Domain.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Events.Operation {
    public class IncludeEntityEvent : CompositePresentationEvent<BaseEntity> {}
}
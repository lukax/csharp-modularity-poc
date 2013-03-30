#region Usings
using LOB.UI.Interface.Event;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.Events {
    public class ReportProgressEvent : CompositePresentationEvent<Progress>, IBaseEvent {

    }

    public struct Progress {

        public string Message { get; set; }
        public int Percentage { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.UI.Interface.Event;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;

namespace LOB.UI.Core.Events
{
    public class ReportProgressEvent : CompositePresentationEvent<Progress>, IBaseEvent
    {
    }

    public struct Progress
    {
        public string Message { get; set; }
        public int Percentage { get; set; }
    }
}

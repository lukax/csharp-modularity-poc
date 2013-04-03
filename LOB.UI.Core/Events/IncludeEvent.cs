using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Events;

namespace LOB.UI.Core.Events
{
    public class IncludeEvent : CompositePresentationEvent<BaseEntity>
    {
    }
}

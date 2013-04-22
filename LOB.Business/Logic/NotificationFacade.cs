using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Domain.Logic;

namespace LOB.Business.Logic
{
    internal class NotificationFacade
    {
        [Export(typeof(Notification))]
        public Notification CreateNotification() {
            return new Notification();          
        }
    }
}

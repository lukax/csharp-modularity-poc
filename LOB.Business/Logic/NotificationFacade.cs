#region Usings

using System.ComponentModel.Composition;
using LOB.Domain.Util;

#endregion

namespace LOB.Business.Logic {
    internal class NotificationFacade {
        [Export(typeof(Notification))]
        public Notification CreateNotification {
            get { return new Notification(); }
        }
    }
}
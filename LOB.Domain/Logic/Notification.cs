#region Usings

using LOB.Core.Localization;
using LOB.Domain.Base;
using NullGuard;

#endregion

namespace LOB.Domain.Logic {
    public class Notification : BaseNotifyChange {

        public Severity Severity { get; set; }
        [AllowNull] public string Message { get; set; }
        [AllowNull] public string Detail { get; set; }
        [AllowNull] public Command Fix { get; set; }
        [AllowNull] public int? Progress { get; set; }

    }

    public enum Severity {

        Warning,
        Attention,
        Information,

    }

    public static class NotificationExtensions {

        public static Notification ToNotificationMessage(this ValidationResult validationResult) {
            return new Notification {
                Detail = validationResult.ErrorDescription,
                Message = Strings.Common_Error + " "
            };
        }

    }
}
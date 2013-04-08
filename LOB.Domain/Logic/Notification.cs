#region Usings

using LOB.Core.Localization;
using LOB.Domain.Base;
using NullGuard;

#endregion

namespace LOB.Domain.Logic {
    public class Notification : BaseNotifyChange {

        public Notification() { Progress = -1; }
        public Severity Severity { get; set; }
        [AllowNull]
        public string Message { get; set; }
        [AllowNull]
        public string Detail { get; set; }
        [AllowNull]
        public Command Fix { get; set; }
        public int Progress { get; set; }

    }

    public enum Severity {

        Ok,
        Info,
        Warning,
        Error,

    }

    public static class NotificationExtensions {

        public static Notification ToNotificationMessage(this ValidationResult validationResult) {
            return new Notification {
                Detail = validationResult.ErrorDescription,
                Message = Strings.Common_Error + " "
            };
        }

        public static Notification Sevirity(this Notification notification, Severity severity) {
            notification.Severity = severity;
            return notification;
        }
        public static Notification Message(this Notification notification, string message) {
            notification.Message = message;
            return notification;
        }
        public static Notification Detail(this Notification notification, string detail) {
            notification.Detail = detail;
            return notification;
        }
        public static Notification Fix(this Notification notification, Command fix) {
            notification.Fix = fix;
            return notification;
        }
        public static Notification Progress(this Notification notification, int progress) {
            notification.Progress = progress;
            return notification;
        }

    }
}
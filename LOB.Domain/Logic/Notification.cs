#region Usings

using System;
using System.Diagnostics;
using LOB.Core.Localization;
using LOB.Domain.Base;
using NullGuard;

#endregion

namespace LOB.Domain.Logic {
    public sealed class Notification : BaseNotifyChange, IEquatable<Notification> {

        public Notification() { Progress = -1; }
        public Severity Severity { get; set; }
        public string Message { get; set; }
        [AllowNull]
        public string Detail { get; set; }
        [AllowNull]
        public Command Fix { get; set; }
        /// <summary>
        /// Value: -2 = Indeterminate progress
        /// Value: -1 = Hidden
        /// Value: 0 -> 100 = Normal progress
        /// </summary>
        public int Progress { get; set; }
        #region Implementation of IEquatable<Notification>

        public bool Equals(Notification other) {
            try {
                return other.Severity.Equals(Severity) && other.Message.Equals(Message) && other.Detail == Detail && other.Fix == (Fix) &&
                       other.Progress.Equals(Progress); //Comparison with == so dont throw null
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }

    public enum Severity {

        Ok,
        Info,
        Warning,
        Error,

    }

    public static class NotificationExtensions {

        public static Notification ToNotificationMessage(this ValidationResult validationResult) { return new Notification {Detail = validationResult.ErrorDescription, Message = Strings.Common_Error + " "}; }

        public static Notification Severity(this Notification notification, Severity severity) {
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
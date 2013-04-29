#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.Logic {
    public sealed class Notification : BaseNotifyChange, IEquatable<Notification> {
        public Notification(string message = null, string detail = null, int progress = -1, NotificationType type = NotificationType.Ok,
            Command fixCommand = null) {
            Message = message;
            Detail = detail;
            Progress = progress;
            Type = type;
            FixCommand = fixCommand;
        }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public DateTime Time { get; set; }
        public Command FixCommand { get; set; }
        /// <summary>
        ///     Value: -2 = Indeterminate progress
        ///     Value: -1 = Hidden
        ///     Value: 0 -> 100 = Normal progress
        /// </summary>
        public int Progress { get; set; }
        #region Implementation of IEquatable<Notification>

        public bool Equals(Notification other) {
            try {
                if(ReferenceEquals(null, other)) return false;
                return Type == other.Type && Message == other.Message && Detail == other.Detail && //FixCommand == other.FixCommand &&
                       Progress == other.Progress; //Comparison with == so dont throw null
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }

    public enum NotificationType {
        Ok,
        Info,
        Warning,
        Error,
    }

    public static class NotificationExtension {
        // public static Notification ToNotificationMessage(this ValidationResult validationResult) { return new Notification {Detail = validationResult.ErrorDescription, Message = Strings.Common_Error + " "}; }

        public static Notification State(this Notification notification, NotificationType type) {
            notification.Type = type;
            return notification;
        }
        public static Notification Message(this Notification notification, string message) {
            notification.Message = message;
            return notification;
        }
        public static Notification Type(this Notification notification, NotificationType type) {
            notification.Type = type;
            return notification;
        }
        public static Notification Detail(this Notification notification, string detail) {
            notification.Detail = detail;
            return notification;
        }
        public static Notification Fix(this Notification notification, Command fix) {
            notification.FixCommand = fix;
            return notification;
        }
        public static Notification Progress(this Notification notification, int progress) {
            notification.Progress = progress;
            return notification;
        }
    }
}
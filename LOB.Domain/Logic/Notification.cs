#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.Base;

//using NullGuard;

#endregion

namespace LOB.Domain.Logic {
    public sealed class Notification : BaseNotifyChange, IEquatable<Notification> {
        public Notification() { Progress = -1; }
        public AttentionState AttentionState { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public Command Fix { get; set; }
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
                return AttentionState == other.AttentionState && Message == other.Message && Detail == other.Detail && Fix == other.Fix &&
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

    public enum AttentionState {
        Ok,
        Info,
        Warning,
        Error,
    }

    public static class NotificationExtensions {
        // public static Notification ToNotificationMessage(this ValidationResult validationResult) { return new Notification {Detail = validationResult.ErrorDescription, Message = Strings.Common_Error + " "}; }

        public static Notification Severity(this Notification notification, AttentionState attentionState) {
            notification.AttentionState = attentionState;
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
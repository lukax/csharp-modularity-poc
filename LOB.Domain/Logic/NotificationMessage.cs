using LOB.Core.Localization;

namespace LOB.Domain.Logic {
    public class NotificationMessage {

        public Severity Severity { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public Command Fix { get; set; }

    }

    public enum Severity {

        Warning,
        Attention,
        Information,

    }

    public static class NotificationMessageExtensions {

        public static NotificationMessage ToNotificationMessage(
            this ValidationResult validationResult) {
            return new NotificationMessage {
                Detail = validationResult.ErrorDescription,
                Message = Strings.Common_Error + " "
            };
        }

    }
}
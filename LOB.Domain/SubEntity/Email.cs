#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using LOB.Core.Localization;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Email : BaseEntity, IEquatable<Email> {
        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        [EmailAddress(ErrorMessageResourceName = "Notification_Field_InvalidFormat", ErrorMessageResourceType = typeof(Strings))]
        public string Value { get; set; }

        public string Detail { get; set; }
        #region EasyToUse

        public static implicit operator string(Email e) { return e.Value; }
        public static implicit operator Email(string value) { return new Email {Value = value}; }
        public override string ToString() { return Value; }

        #endregion
        #region Implementation of IEquatable<Email>

        public bool Equals(Email other) {
            try {
                return
                        base.Equals(other) &&
                        other.Value.Equals(Value) &&
                        other.Detail.Equals(Detail);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }
}
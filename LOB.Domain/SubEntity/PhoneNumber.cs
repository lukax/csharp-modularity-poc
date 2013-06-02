#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading;
using LOB.Core.Localization;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class PhoneNumber : BaseEntity, IEquatable<PhoneNumber> {
        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        [RegularExpression(@"\d", ErrorMessageResourceName = "Notification_Field_OnlyNumbers", ErrorMessageResourceType = typeof(Strings))]
        [MinLength(8, ErrorMessageResourceName = "Notification_Field_MinLength", ErrorMessageResourceType = typeof(Strings))]
        public string Number { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public PhoneNumberType Type { get; set; }

        public string Description { get; set; }
        #region Implementation of IEquatable

        public bool Equals(PhoneNumber other) {
            try {
                return base.Equals(other) && other.Number.Equals(Number) && other.Type.Equals(Type) && other.Description.Equals(Description);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }
        public override string ToString() { return string.Format("{0}", Number.ToString(Thread.CurrentThread.CurrentCulture)); }

        #endregion
    }

    [Serializable]
    public enum PhoneNumberType {
        Residential,
        Mobile,
        Work
    }
}
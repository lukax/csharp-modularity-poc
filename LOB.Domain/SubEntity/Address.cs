#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using LOB.Core.Localization;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Address : BaseEntity, IEquatable<Address> {
        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public AddressStatus Status { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public string Street { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public int StreetNumber { get; set; }

        public string StreetComplement { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessageResourceName = "Notification_Field_InvalidFormat", ErrorMessageResourceType = typeof(Strings)
                )]
        public string PostalCode { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public string Country { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public string State { get; set; }

        public string District { get; set; }

        public string County { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public bool IsDefault { get; set; }

        #region Implementation of IEquatable<Address>

        public bool Equals(Address other) {
            try {
                return
                        base.Equals(other) &&
                        Status.Equals(other.Status) &&
                        Street.Equals(other.Street) &&
                        StreetNumber.Equals(other.StreetNumber) &&
                        StreetComplement.Equals(other.StreetComplement) &&
                        PostalCode.Equals(other.PostalCode) &&
                        Country.Equals(other.Country) &&
                        State.Equals(other.State) &&
                        District.Equals(other.District) &&
                        Country.Equals(other.Country) &&
                        IsDefault.Equals(other.IsDefault);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }

    [Serializable]
    public enum AddressStatus {
        Active,
        Inactive,
        Deprecated
    }
}
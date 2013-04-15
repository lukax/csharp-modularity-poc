#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Address : BaseEntity, IEquatable<Address> {
        public AddressStatus Status { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string StreetComplement { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string County { get; set; }
        public bool IsDefault { get; set; }
        #region Implementation of IEquatable<Address>

        public bool Equals(Address other) {
            try {
                return base.Equals(other) && Status.Equals(other.Status) && Street.Equals(other.Street) && StreetNumber.Equals(other.StreetNumber) &&
                       StreetComplement.Equals(other.StreetComplement) && ZipCode.Equals(other.ZipCode) && Country.Equals(other.Country) &&
                       State.Equals(other.State) && District.Equals(other.District) && Country.Equals(other.Country) &&
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
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
                return base.Equals(other) && other.Status.Equals(Status) && other.Street.Equals(Street) && other.StreetNumber.Equals(StreetNumber) &&
                       other.StreetComplement.Equals(StreetComplement) && other.ZipCode.Equals(ZipCode) && other.Country.Equals(Country) &&
                       other.State.Equals(State) && other.District.Equals(District) && other.Country.Equals(Country) && other.IsDefault.Equals(IsDefault);
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
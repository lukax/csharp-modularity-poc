#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Address : BaseEntity {

        public AddressStatus Status { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string StreetComplement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public bool IsDefault { get; set; }

    }

    [Serializable]
    public enum AddressStatus {

        Active,
        Inactive,
        Deprecated

    }
}
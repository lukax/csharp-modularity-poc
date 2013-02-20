#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity
{
    [Serializable]
    public class Address : BaseEntity
    {
        public virtual AdressStatus Status { get; set; }
        public virtual string Street { get; set; }
        public virtual int StreetNumber { get; set; }
        public virtual string StreetComplement { get; set; }
        public virtual string District { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual int ZipCode { get; set; }
        public virtual bool IsDefault { get; set; }
    }

    [Serializable]
    public enum AdressStatus
    {
        Active,
        Inactive,
        Deprecated
    }
}
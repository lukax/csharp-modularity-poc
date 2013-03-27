#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class PhoneNumber : BaseEntity {
        public int Number { get; set; }
        public PhoneNumberType PhoneNumberType { get; set; }
        public string Description { get; set; }

        public override string ToString() {
            return Number.ToString();
        }
    }

    [Serializable]
    public enum PhoneNumberType {
        Telephone,
        Cellphone,
        Fax
    }
}
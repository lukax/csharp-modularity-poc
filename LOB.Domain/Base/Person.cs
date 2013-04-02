#region Usings

using System;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain.Base {
    [Serializable]
    public abstract class Person : BaseEntity {

        public Address Address { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public string Notes { get; set; }

    }
}
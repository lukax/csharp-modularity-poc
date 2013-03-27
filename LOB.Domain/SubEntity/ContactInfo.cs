#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class ContactInfo : BaseEntity {
        public ContactStatus Status { get; set; }
        public IList<PhoneNumber> PhoneNumbers { get; set; }
        public IList<Email> Emails { get; set; }
        public string WebSite { get; set; }
        public string SpeakWith { get; set; }
        public string Ps { get; set; }
    }

    [Serializable]
    public enum ContactStatus {
        Active,
        Inactive,
        Deprecated
    }
}
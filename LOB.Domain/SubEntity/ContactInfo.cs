#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity
{
    [Serializable]
    public class ContactInfo : BaseEntity
    {
        public virtual ContactStatus Status { get; set; }
        public virtual IList<PhoneNumber> PhoneNumbers { get; set; }
        public virtual IList<Email> Emails { get; set; }
        public virtual string WebSite { get; set; }
        public virtual IList<Person> SpeakWith { get; set; }
        public virtual string Ps { get; set; }
    }

    [Serializable]
    public enum ContactStatus
    {
        Active,
        Inactive,
        Deprecated
    }
}
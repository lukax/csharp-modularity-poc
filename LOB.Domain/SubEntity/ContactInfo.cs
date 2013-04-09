#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class ContactInfo : BaseEntity, IEquatable<ContactInfo> {

        public ContactStatus Status { get; set; }
        public string Description { get; set; }
        public IList<PhoneNumber> PhoneNumbers { get; set; }
        public IList<Email> Emails { get; set; }
        public string WebSite { get; set; }
        public string SpeakWith { get; set; }
        public string PS { get; set; }
        #region Implementation of IEquatable<ContactInfo>

        public bool Equals(ContactInfo other) {
            try {
                return base.Equals(other) && other.Status.Equals(Status) && other.Description.Equals(Description) &&
                       other.PhoneNumbers.SequenceEqual(PhoneNumbers) && other.Emails.SequenceEqual(Emails) && other.WebSite.Equals(WebSite) &&
                       other.PS.Equals(PS);
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
    public enum ContactStatus {

        Active,
        Inactive,
        Deprecated

    }
}
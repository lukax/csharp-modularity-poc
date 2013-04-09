#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain.Base {
    [Serializable]
    public abstract class Person : BaseEntity, IEquatable<Person> {

        public Address Address { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public string Notes { get; set; }
        #region Implementation of IEquatable<Person>

        public bool Equals(Person other) {
            try {
                return base.Equals(other) && other.Address.Equals(Address) && other.ContactInfo.Equals(ContactInfo) && other.Notes.Equals(Notes);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }
}
#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    public class Email : BaseEntity, IEquatable<Email> {
        public string Value { get; set; }

        public static implicit operator string(Email e) { return e.Value; }

        public static implicit operator Email(string value) { return new Email {Value = value}; }
        #region Implementation of IEquatable<Email>

        public bool Equals(Email other) {
            try {
                return base.Equals(other) && other.Value.Equals(Value);
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
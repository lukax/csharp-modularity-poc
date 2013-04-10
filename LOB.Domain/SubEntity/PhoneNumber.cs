#region Usings

using System;
using System.Diagnostics;
using System.Threading;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class PhoneNumber : BaseEntity, IEquatable<PhoneNumber> {

        public string Number { get; set; }
        public PhoneNumberType Type { get; set; }
        public string Description { get; set; }

        public bool Equals(PhoneNumber other) {
            try {
                return base.Equals(other) && other.Number.Equals(Number) && other.Type.Equals(Type) &&
                       other.Description.Equals(Description);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }
        public override string ToString() { return Number.ToString(Thread.CurrentThread.CurrentCulture); }

    }

    [Serializable]
    public enum PhoneNumberType {

        Telephone,
        Cellphone,
        Fax

    }
}
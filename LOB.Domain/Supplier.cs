#region Usings

using System;
using System.Diagnostics;

#endregion

namespace LOB.Domain {
    public class Supplier : LegalPerson, IEquatable<Supplier> {
        #region Implementation of IEquatable<Supplier>

        public bool Equals(Supplier other) {
            try {
                return base.Equals(other);
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
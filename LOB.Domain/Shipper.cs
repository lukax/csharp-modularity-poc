#region Usings

using System;
using System.Diagnostics;

#endregion

namespace LOB.Domain {
    public class Shipper : LegalPerson, IEquatable<Shipper> {
        #region Implementation of IEquatable<Shipper>

        public bool Equals(Shipper other) {
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
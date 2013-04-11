#region Usings

using System;
using System.Diagnostics;

#endregion

namespace LOB.Domain.Base {
    [Serializable]
    public abstract class Service : BaseEntity, IEquatable<Service> {

        public string Name { get; set; }
        public string Description { get; set; }
        #region Implementation of IEquatable<Service>

        public bool Equals(Service other) {
            try {
                if(other == null) return false;
                return string.Equals(Name, other.Name) && string.Equals(Description, other.Description);
            } catch(InvalidOperationException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }
}
#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Category : BaseEntity, IEquatable<Category> {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Equals(Category other) {
            try {
                return base.Equals(other);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }
        public override string ToString() { return Name; }
    }
}
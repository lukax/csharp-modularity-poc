#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class EntityGroup<T> : BaseEntity, IEquatable<EntityGroup<T>> where T : BaseEntity {
        public T Entity { get; set; }
        public string Description { get; set; }
        #region Implementation of IEquatable<EntityGroup<T>>

        public bool Equals(EntityGroup<T> other) {
            try {
                return base.Equals(other) && other.Entity.Equals(Entity) && other.Description.Equals(Description);
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
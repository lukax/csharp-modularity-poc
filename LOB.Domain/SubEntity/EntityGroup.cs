#region Usings
using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable] public class EntityGroup<T> : BaseEntity where T : BaseEntity {

        public T Entity { get; set; }
        public string Description { get; set; }

    }
}
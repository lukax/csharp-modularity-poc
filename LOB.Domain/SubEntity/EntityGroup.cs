#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity
{
    [Serializable]
    public class EntityGroup<T> : BaseEntity where T : BaseEntity
    {
        public virtual T Entity { get; set; }
        public virtual string Description { get; set; }
    }
}
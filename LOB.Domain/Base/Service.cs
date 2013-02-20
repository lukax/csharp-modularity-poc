#region Usings

using System;

#endregion

namespace LOB.Domain.Base
{
    [Serializable]
    public class Service : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain.Base
{
    [Serializable]
    public abstract class Person : BaseEntity
    {
        public virtual Address Address { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }
        public virtual string Notes { get; set; }
    }
}
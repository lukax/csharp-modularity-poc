#region Usings

using System;

#endregion

namespace LOB.Domain.Base
{
    [Serializable]
    public abstract class BaseEntity : BaseNotifyChange
    {
        public virtual Guid Id { get; private set; }
        public virtual int Code { get; set; }
    }
}
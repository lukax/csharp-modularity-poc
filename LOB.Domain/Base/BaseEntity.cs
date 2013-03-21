#region Usings

using System;

#endregion

namespace LOB.Domain.Base
{
    [Serializable]
    public abstract class BaseEntity : BaseNotifyChange
    {
        public Guid Id { get; private set; }
        public int Code { get; set; }
    }
}
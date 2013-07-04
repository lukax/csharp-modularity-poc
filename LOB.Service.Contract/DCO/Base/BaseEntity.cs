#region Usings

using System;

//using NullGuard;

#endregion

namespace LOB.Service.Contract.DCO.Base {
    public abstract class BaseEntity   {
        public Guid Id { get; set; }
        public long Code { get; set; }
    }
}
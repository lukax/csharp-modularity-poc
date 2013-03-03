#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class Customer : BaseEntity
    {
        public virtual Person Person { get; set; }
        public virtual PersonType PersonType { get; set; }
        public virtual IList<Store> CustomerOf { get; set; }
        public virtual CustomerStatus Status { get; set; }
        public virtual IList<Sale> BoughtHistory { get; set; }
    }

    [Serializable]
    public enum PersonType
    {
        Natural,
        Legal
    }

    [Serializable]
    public enum CustomerStatus
    {
        New,
        Active,
        Inactive
    }
}
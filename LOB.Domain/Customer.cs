#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Customer : BaseEntity {
        public Person Person { get; set; }
        public PersonType PersonType { get; set; }
        public IList<Store> CustomerOf { get; set; }
        public CustomerStatus Status { get; set; }
        public IList<Sale> BoughtHistory { get; set; }
    }

    [Serializable]
    public enum PersonType {
        Natural,
        Legal
    }

    [Serializable]
    public enum CustomerStatus {
        New,
        Active,
        Inactive
    }
}
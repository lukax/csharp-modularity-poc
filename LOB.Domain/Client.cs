#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class Client : BaseEntity
    {
        public Client()
        {
            Person = new Person();
        }

        public virtual Person Person { get; set; }
        public virtual IList<Store> ClientOf { get; set; }
        public virtual ClientStatus Status { get; set; }
        public virtual IList<Sale> BoughtHistory { get; set; }
    }

    [Serializable]
    public enum ClientStatus
    {
        New,
        Active,
        Inactive
    }
}
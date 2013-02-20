#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class Sale : BaseEntity
    {
        public virtual SaleState State { get; set; }
        public virtual DateTime SaleDate { get; set; }
        public virtual Client Buyer { get; set; }
        public virtual IList<Product> Products { get; set; }
        public virtual double TotalValue { get; set; }
        public virtual double UnitValue { get; set; }
        public virtual int Quantity { get; set; }
        public virtual string Ps { get; set; }
    }

    [Serializable]
    public enum SaleState
    {
        Open,
        Finalized,
        Canceled,
        Paused
    }
}
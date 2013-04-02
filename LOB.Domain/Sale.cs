#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Sale : BaseEntity {

        public SaleState State { get; set; }
        public DateTime SaleDate { get; set; }
        public Customer Buyer { get; set; }
        public IList<Product> Products { get; set; }
        public double TotalValue { get; set; }
        public double UnitValue { get; set; }
        public int Quantity { get; set; }
        public string PS { get; set; }

    }

    [Serializable]
    public enum SaleState {

        Open,
        Finalized,
        Canceled,
        Paused

    }
}
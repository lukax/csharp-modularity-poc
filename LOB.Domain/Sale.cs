#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Sale : BaseEntity, IEquatable<Sale> {
        public SaleState State { get; set; }
        public DateTime SaleDate { get; set; }
        public Customer Buyer { get; set; }
        public IList<Product> Products { get; set; }
        public double TotalValue { get; set; }
        public double UnitValue { get; set; }
        public int Quantity { get; set; }
        public string PS { get; set; }
        #region Implementation of IEquatable<Sale>

        public bool Equals(Sale other) {
            try {
                return base.Equals(other) && other.State.Equals(State) && other.SaleDate.Equals(SaleDate) && other.Buyer.Equals(Buyer) &&
                       other.Products.SequenceEqual(Products) && other.TotalValue.Equals(TotalValue) && other.UnitValue.Equals(UnitValue) &&
                       other.Quantity.Equals(Quantity) && other.PS.Equals(PS);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }

    [Serializable]
    public enum SaleState {
        Open,
        Finalized,
        Canceled,
        Paused
    }
}
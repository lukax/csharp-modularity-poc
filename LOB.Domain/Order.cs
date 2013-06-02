#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Order : BaseEntity, IEquatable<Order> {
        public OrderStatus Status { get; set; }
        public string Description { get; set; }
        public Customer Buyer { get; set; }
        public Employee Seller { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public float TotalValue { get; set; }

        #region Implementation of IEquatable<Sale>

        public bool Equals(Order other) {
            try {
                return base.Equals(other) &&
                       other.Status.Equals(Status) &&
                       other.Description.Equals(Description) &&
                       other.Buyer.Equals(Buyer) &&
                       other.Seller.Equals(Seller) &&
                       other.Date.Equals(Date) &&
                       other.Products.SequenceEqual(Products) &&
                       other.Services.SequenceEqual(Services) &&
                       other.TotalValue.Equals(TotalValue);
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
    public enum OrderStatus {
        Pending,
        Completed,
        Canceled
    }
}
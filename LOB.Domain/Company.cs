#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Company : LegalPerson, IEquatable<Company> {
        public string LocalName { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        #region Implementation of IEquatable<Store>

        public bool Equals(Company other) {
            try {
                return base.Equals(other) && other.LocalName.Equals(LocalName) && other.Employees.SequenceEqual(Employees) &&
                       other.Products.SequenceEqual(Products) && other.Customers.SequenceEqual(Customers) && other.Orders.SequenceEqual(Orders);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }
}
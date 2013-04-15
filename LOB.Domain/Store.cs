#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace LOB.Domain {
    public class Store : LegalPerson, IEquatable<Store> {
        public string Name { get; set; }
        public IList<Employee> Employees { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Customer> Clients { get; set; }
        public IList<Sale> Sales { get; set; }
        #region Implementation of IEquatable<Store>

        public bool Equals(Store other) {
            try {
                return base.Equals(other) && other.Name.Equals(Name) && other.Employees.SequenceEqual(Employees) &&
                       other.Products.SequenceEqual(Products) && other.Clients.SequenceEqual(Clients) && other.Sales.SequenceEqual(Sales);
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
#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Employee : NaturalPerson, IEquatable<Employee> {
        public Company AssociatedCompany { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public Paycheck Paycheck { get; set; }
        public IEnumerable<Order> Sales { get; set; }
        #region Implementation of IEquatable<Employee>

        public bool Equals(Employee other) {
            try {
                return
                        base.Equals(other) &&
                        other.AssociatedCompany.Equals(AssociatedCompany) &&
                        other.Title.Equals(Title) &&
                        other.HireDate.Equals(HireDate) &&
                        other.Paycheck.Equals(Paycheck) &&
                        other.Sales.SequenceEqual(Sales);
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
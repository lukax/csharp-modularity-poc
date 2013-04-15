#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Employee : NaturalPerson, IEquatable<Employee> {
        public Store WorksIn { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public PayCheck PayCheck { get; set; }
        public string Password { get; set; }
        #region Implementation of IEquatable<Employee>

        public bool Equals(Employee other) {
            try {
                return base.Equals(other) && other.WorksIn.Equals(WorksIn) && other.Title.Equals(Title) && other.HireDate.Equals(HireDate) &&
                       other.PayCheck.Equals(PayCheck) && other.Password.Equals(Password);
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
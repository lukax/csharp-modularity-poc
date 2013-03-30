#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable] public class Employee : NaturalPerson {

        public Store WorksIn { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public PayCheck PayCheck { get; set; }
        public string Password { get; set; }

    }

    [Serializable] public class PayCheck : BaseEntity {

        public double CurrentSalary { get; set; }
        public double Bonus { get; set; }
        public string Ps { get; set; }

    }
}
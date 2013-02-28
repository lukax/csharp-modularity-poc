#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class Employee : BaseEntity
    {
        public virtual NaturalPerson Person { get; set; }
        public virtual Store WorksIn { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime HireDate { get; set; }
        public virtual PayCheck PayCheck { get; set; }
        public virtual string Password { get; set; }
    }

    [Serializable]
    public class PayCheck : BaseEntity
    {
        public virtual double CurrentSalary { get; set; }
        public virtual double Bonus { get; set; }
        public virtual string Ps { get; set; }
    }
}
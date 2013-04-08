#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class PayCheck : BaseEntity {

        public double CurrentSalary { get; set; }
        public double Bonus { get; set; }
        public string PS { get; set; }

    }
}
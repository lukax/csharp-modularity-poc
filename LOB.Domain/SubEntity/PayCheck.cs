#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class PayCheck : BaseEntity, IEquatable<PayCheck> {
        public double CurrentSalary { get; set; }
        public double Bonus { get; set; }
        public string PS { get; set; }
        #region Implementation of IEquatable<PayCheck>

        public bool Equals(PayCheck other) {
            try {
                return base.Equals(other) && other.CurrentSalary.Equals(CurrentSalary) && other.Bonus.Equals(Bonus) && other.PS.Equals(PS);
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
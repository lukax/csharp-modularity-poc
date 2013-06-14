#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using LOB.Core.Localization;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Paycheck : BaseEntity, IEquatable<Paycheck> {
        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public double CurrentSalary { get; set; }
        public double InitialSalary { get; set; }

        public double Bonus { get; set; }

        public string Detail { get; set; }
        #region Implementation of IEquatable<PayCheck>

        public bool Equals(Paycheck other) {
            try {
                return base.Equals(other) && other.CurrentSalary.Equals(CurrentSalary) && other.Bonus.Equals(Bonus) &&
                       other.Detail.Equals(Detail);
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
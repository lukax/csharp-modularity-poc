#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using LOB.Core.Localization;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain.Base {
    [Serializable]
    public abstract class NaturalPerson : Person, IEquatable<NaturalPerson> {
        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public string LastName { get; set; }

        public string FullName {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string NickName { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        [RegularExpression(@"^\d{3}.\d{3}.\d{3}-\d{2}$", ErrorMessageResourceName = "Notification_Field_InvalidFormat",
                ErrorMessageResourceType = typeof(Strings))]
        public string CPF { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        [RegularExpression(@"^\d{2}.\d{3}.\d{3}-\d{1}$", ErrorMessageResourceName = "Notification_Field_InvalidFormat",
                ErrorMessageResourceType = typeof(Strings))]
        public string RG { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public UF RGUF { get; set; }
        #region Implementation of IEquatable<NaturalPerson>

        public bool Equals(NaturalPerson other) {
            try {
                return base.Equals(other) &&
                       other.FirstName.Equals(FirstName) &&
                       other.LastName.Equals(LastName) &&
                       other.NickName.Equals(NickName) &&
                       other.BirthDate.Equals(BirthDate) &&
                       other.CPF.Equals(CPF) &&
                       other.RG.Equals(RG) &&
                       other.RGUF.Equals(RGUF);
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
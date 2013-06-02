#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using LOB.Core.Localization;

#endregion

namespace LOB.Domain.Base {
    [Serializable]
    public abstract class LegalPerson : Person, IEquatable<LegalPerson> {
        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public string CorporateName { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public string TradingName { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        [RegularExpression(@"^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$", ErrorMessageResourceName = "Notification_Field_InvalidFormat",
                ErrorMessageResourceType = typeof(Strings))]
        public string CNPJ { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        //[RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceName = "Notification_Field_InvalidFormat", ErrorMessageResourceType = typeof(Strings))]
        public string InscEstadual { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        //[RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceName = "Notification_Field_InvalidFormat", ErrorMessageResourceType = typeof(Strings))]
        public string InscMunicipal { get; set; }

        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        [RegularExpression(@"^\d{4}-\d{1}/\d{2}$", ErrorMessageResourceName = "Notification_Field_InvalidFormat",
                ErrorMessageResourceType = typeof(Strings))]
        public string CNAEFiscal { get; set; }

        [Url(ErrorMessageResourceName = "Notification_Field_InvalidFormat", ErrorMessageResourceType = typeof(Strings))]
        public string Website { get; set; }
        #region Implementation of IEquatable<LegalPerson>

        public bool Equals(LegalPerson other) {
            try {
                return
                        base.Equals(other) &&
                        other.CorporateName.Equals(CorporateName) &&
                        other.TradingName.Equals(TradingName) &&
                        other.CNPJ.Equals(CNPJ) &&
                        other.InscEstadual.Equals(InscEstadual) &&
                        other.InscMunicipal.Equals(InscMunicipal) &&
                        other.CNAEFiscal.Equals(CNAEFiscal) &&
                        other.Website.Equals(Website);
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
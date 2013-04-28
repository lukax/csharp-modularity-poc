#region Usings

using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using LOB.Business.Contract.Logic;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Domain;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(ILegalPersonFacade)), Export(typeof(IBaseEntityFacade<LegalPerson>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class LegalPersonFacade : BaseEntityFacade<LegalPerson>, ILegalPersonFacade {
        private readonly IContactInfoFacade _contactInfoFacade;
        private readonly IAddressFacade _addressFacade;

        [ImportingConstructor]
        public LegalPersonFacade(IAddressFacade addressFacade, IContactInfoFacade contactInfoFacade, IRepository repository)
            : base(repository) {
            _contactInfoFacade = contactInfoFacade;
            _addressFacade = addressFacade;
            ConfigureValidations();
        }

        public override LegalPerson GenerateEntity() {
            var result = base.GenerateEntity();
            result.Address = _addressFacade.GenerateEntity();
            result.ContactInfo = _contactInfoFacade.GenerateEntity();
            result.Notes = "";
            result.CNAEFiscal = "";
            result.CNPJ = "";
            result.CorporateName = "";
            result.InscEstadual = "";
            result.InscMunicipal = "";
            result.TradingName = "";
            return result;
        }

        public void ConfigureValidations() {
            AddValidation(delegate {
                              if(string.IsNullOrWhiteSpace(Entity.CorporateName)) return new ValidationResult("LastName", Strings.Notification_Field_Empty);
                              if(
                                  !Regex.IsMatch(Entity.CorporateName,
                                                 @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$"))
                                  return new ValidationResult("CorporateName",
                                                              string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_CorporateName));
                              return null;
                          });
            AddValidation(delegate {
                              if(string.IsNullOrWhiteSpace(Entity.TradingName)) return new ValidationResult("LastName", Strings.Notification_Field_Empty);
                              if(
                                  !Regex.IsMatch(Entity.TradingName,
                                                 @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$"))
                                  return new ValidationResult("TradingName",
                                                              string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_TradingName));
                              return null;
                          });
            AddValidation(delegate {
                              if(string.IsNullOrWhiteSpace(Entity.CNPJ)) return new ValidationResult("CNPJ", Strings.Notification_Field_Empty);
                              if(!Regex.IsMatch(Entity.CNPJ, @"^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$")) return new ValidationResult("CNPJ", string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_Cnpj));
                              return null;
                          });
            AddValidation(delegate {
                              if(string.IsNullOrWhiteSpace(Entity.CNAEFiscal)) return new ValidationResult("CNAEFiscal", Strings.Notification_Field_Empty);
                              if(!Regex.IsMatch(Entity.CNAEFiscal, @"^\d{4}-\d{1}/\d{2}$"))
                                  return new ValidationResult("CNAEFiscal",
                                                              string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_CnaeFiscal));
                              return null;
                          });
        }
    }
}
#region Usings

using System;
using System.Text.RegularExpressions;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    public class NaturalPersonFacade : BaseEntityFacade<NaturalPerson>, INaturalPersonFacade {
        private readonly IAddressFacade _addressFacade;
        private readonly IContactInfoFacade _contactInfoFacade;
        public NaturalPersonFacade(IAddressFacade addressFacade, IContactInfoFacade contactInfoFacade, IRepository repository)
            : base(repository) {
            _addressFacade = addressFacade;
            _contactInfoFacade = contactInfoFacade;
            ConfigureValidations();
        }

        public override NaturalPerson GenerateEntity() {
            var result = base.GenerateEntity();
            result.FirstName = "";
            result.LastName = "";
            result.NickName = "";
            result.BirthDate = DateTime.Now;
            result.CPF = "";
            result.RG = "";
            result.RGUF = "";
            result.Address = _addressFacade.GenerateEntity();
            result.ContactInfo = _contactInfoFacade.GenerateEntity();
            result.Notes = "";
            return result;
        }

        public void ConfigureValidations() {
            AddValidation(delegate {
                              if(string.IsNullOrWhiteSpace(Entity.FirstName)) return new ValidationResult("FirstName", Strings.Notification_Field_Empty);
                              if(
                                  !Regex.IsMatch(Entity.FirstName,
                                                 @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$"))
                                  return new ValidationResult("FirstName",
                                                              string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_FirstName));
                              return null;
                          });
            AddValidation(delegate {
                              if(string.IsNullOrWhiteSpace(Entity.LastName)) return new ValidationResult("LastName", Strings.Notification_Field_Empty);
                              if(
                                  !Regex.IsMatch(Entity.LastName,
                                                 @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$")) return new ValidationResult("LastName", string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_LastName));
                              return null;
                          });
            AddValidation(delegate {
                              if(string.IsNullOrWhiteSpace(Entity.CPF)) return new ValidationResult("CPF", Strings.Notification_Field_Empty);
                              if(!Regex.IsMatch(Entity.CPF, @"^\d{3}.\d{3}.\d{3}-\d{2}$")) return new ValidationResult("CPF", string.Format(Strings.Notification_Field_X_Invalid, "CPF"));
                              return null;
                          });
            AddValidation(delegate {
                              if(string.IsNullOrWhiteSpace(Entity.RG)) return new ValidationResult("RG", Strings.Notification_Field_Empty);
                              if(!Regex.IsMatch(Entity.RG, @"^\d{2}.\d{3}.\d{3}-\d{1}$")) return new ValidationResult("RG", string.Format(Strings.Notification_Field_X_Invalid, "RG"));
                              return null;
                          });
            AddValidation(delegate {
                              if(Entity.BirthDate.CompareTo(new DateTime(1910, 1, 1)) < 0) return new ValidationResult("DeliverDate", Strings.Notification_Field_DateTooEarly);
                              if(Entity.BirthDate.CompareTo(new DateTime(2014, 1, 1)) > 0) return new ValidationResult("DeliverDate", Strings.Notification_Field_DateTooEarly);
                              return null;
                          });
        }
    }
}
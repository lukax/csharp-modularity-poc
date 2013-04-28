#region Usings

using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IPhoneNumberFacade)), Export(typeof(IBaseEntityFacade<PhoneNumber>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class PhoneNumberFacade : BaseEntityFacade<PhoneNumber>, IPhoneNumberFacade {
        [ImportingConstructor]
        public PhoneNumberFacade(IRepository repository)
            : base(repository) { ConfigureValidations(); }

        public override PhoneNumber GenerateEntity() {
            var result = base.GenerateEntity();
            result.Number = "";
            result.Type = default(PhoneNumberType);
            result.Description = "";
            return result;
        }

        private void ConfigureValidations() {
            AddValidation(delegate {
                              if(string.IsNullOrWhiteSpace(Entity.Number)) return new ValidationResult("Number", Strings.Notification_Field_Empty);
                              if(Entity.Number.Length < 8) return new ValidationResult("Number", string.Format(Strings.Notification_Field_X_MinLength, 8));
                              if(!Regex.IsMatch(Entity.Number, @"\d")) return new ValidationResult("Number", Strings.Notification_Field_IntOnly);
                              return null;
                          });
            AddValidation(
                delegate { return string.IsNullOrWhiteSpace(Entity.Type.ToString()) ? new ValidationResult("Type", Strings.Notification_Field_Empty) : null; });

            AddValidation(
                delegate { return string.IsNullOrWhiteSpace(Entity.Description) ? new ValidationResult("Description", Strings.Notification_Field_Empty) : null;
                });
        }
    }
}
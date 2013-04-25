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
    [Export(typeof(IAddressFacade)), Export(typeof(IBaseEntityFacade<Address>))]
    public sealed class AddressFacade : BaseEntityFacade<Address>, IAddressFacade {
        [ImportingConstructor]
        public AddressFacade(IRepository repository)
            : base(repository) { ConfigureValidations(); }

        public override Address GenerateEntity() {
            var result = base.GenerateEntity();
            result.Country = "Brasil";
            result.District = "";
            result.IsDefault = false;
            result.State = "Rio de Janeiro";
            result.Status = default(AddressStatus);
            result.Street = "";
            result.StreetComplement = "";
            result.StreetNumber = "";
            result.ZipCode = "";
            return result;
        }

        public void ConfigureValidations() {
            AddValidation(
                delegate { return string.IsNullOrWhiteSpace(Entity.Street) ? new ValidationResult("Street", Strings.Notification_Field_Empty) : null; });
            AddValidation((delegate {
                               if(string.IsNullOrWhiteSpace(Entity.StreetNumber)) return new ValidationResult("StreetNumber", Strings.Notification_Field_Empty);
                               if(!Regex.IsMatch(Entity.StreetNumber, @"\d")) return new ValidationResult("StreetNumber", Strings.Notification_Field_IntOnly);
                               return null;
                           }));
            AddValidation((delegate {
                               if(string.IsNullOrWhiteSpace(Entity.ZipCode)) return new ValidationResult("ZipCode", Strings.Notification_Field_Empty);
                               if(Regex.IsMatch(Entity.ZipCode, @"^\d{5}-\d{3}$")) return new ValidationResult("ZipCode", string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_ZipCode));
                               return null;
                           }));
            AddValidation(
                ((sender, name) => string.IsNullOrWhiteSpace(Entity.County) ? new ValidationResult("County", Strings.Notification_Field_Empty) : null));
            AddValidation(
                (sender, name) =>
                string.IsNullOrWhiteSpace(Entity.District) ? new ValidationResult("District", Strings.Notification_Field_Empty) : null);
            AddValidation(
                (sender, name) => string.IsNullOrWhiteSpace(Entity.State) ? new ValidationResult("State", Strings.Notification_Field_Empty) : null);
        }
    }
}
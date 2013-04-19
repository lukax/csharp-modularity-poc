#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic.Base {
    [Export(typeof(IPersonFacade))]
    public class PersonFacade : BaseEntityFacade<Person>, IPersonFacade {
        private readonly IAddressFacade _addressFacade;
        private readonly IContactInfoFacade _contactInfoFacade;

        [ImportingConstructor]
        public PersonFacade(IAddressFacade addressFacade, IContactInfoFacade contactInfoFacade, IRepository repository)
            : base(repository) {
            _addressFacade = addressFacade;
            _contactInfoFacade = contactInfoFacade;
            ConfigureValidations();
        }

        public override Person GenerateEntity() {
            var local = base.GenerateEntity();
            local.Address = _addressFacade.GenerateEntity();
            local.ContactInfo = _contactInfoFacade.GenerateEntity();
            local.Notes = "";
            return local;
        }

        public void ConfigureValidations() {
            AddValidation(
                (sender, name) =>
                Entity.Notes.Length > 300 ? new ValidationResult("Notes", string.Format(Strings.Notification_Field_X_MaxLength, 300)) : null);
        }
    }
}
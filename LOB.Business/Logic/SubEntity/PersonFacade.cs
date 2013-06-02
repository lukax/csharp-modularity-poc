#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IPersonFacade)), Export(typeof(IBaseEntityFacade<Person>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonFacade : BaseEntityFacade<LocalPerson>, IPersonFacade {
        private readonly IAddressFacade _addressFacade;
        private readonly IContactInfoFacade _contactInfoFacade;

        [ImportingConstructor]
        public PersonFacade(IAddressFacade addressFacade, IContactInfoFacade contactInfoFacade, IRepository repository)
                : base(repository) {
            _addressFacade = addressFacade;
            _contactInfoFacade = contactInfoFacade;
        }

        public override Person GenerateEntity() {
            Person local = base.GenerateEntity();
            local.Address = _addressFacade.GenerateEntity();
            local.ContactInfo = _contactInfoFacade.GenerateEntity();
            local.Notes = "";
            return local;
        }
    }

    public class LocalPerson : Person
    {

    }
}
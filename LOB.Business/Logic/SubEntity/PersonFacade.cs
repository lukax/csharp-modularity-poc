#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IPersonFacade)), Export(typeof(IBaseEntityFacade<Person>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonFacade : BaseEntityFacade, IPersonFacade {
        private readonly IAddressFacade _addressFacade;

        [ImportingConstructor]
        public PersonFacade(IAddressFacade addressFacade, IRepository repository)
                : base(repository) { _addressFacade = addressFacade; }

        public Person Generate() {
            Person local = new LocalPerson();
            local.Address = _addressFacade.Generate();
            return local;
        }

        private class LocalPerson : Person {}
    }
}
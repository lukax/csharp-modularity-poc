#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IPhoneNumberFacade)), Export(typeof(IBaseEntityFacade<PhoneNumber>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class PhoneNumberFacade : BaseEntityFacade, IPhoneNumberFacade {
        [ImportingConstructor]
        public PhoneNumberFacade(IRepository repository)
                : base(repository) { }

        public PhoneNumber Generate() {
            var result = new PhoneNumber {Number = "", Type = default(PhoneNumberType), Description = ""};
            return result;
        }
    }
}
#region Usings

using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IAddressFacade)), Export(typeof(IBaseEntityFacade<Address>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class AddressFacade : BaseEntityFacade<Address>, IAddressFacade {
        [ImportingConstructor]
        public AddressFacade(IRepository repository)
                : base(repository) { }

        public override Address GenerateEntity() {
            Address result = base.GenerateEntity();
            result.Country = "Brasil";
            result.District = "";
            result.IsDefault = false;
            result.State = "Rio de Janeiro";
            result.Status = default(AddressStatus);
            result.Street = "";
            result.StreetComplement = "";
            result.StreetNumber = 0;
            result.PostalCode = "00000-000";
            return result;
        }


    }
}
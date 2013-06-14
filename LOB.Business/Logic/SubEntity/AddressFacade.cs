#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IAddressFacade)), Export(typeof(IBaseEntityFacade<Address>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class AddressFacade : BaseEntityFacade, IAddressFacade {
        [ImportingConstructor]
        public AddressFacade(IRepository repository)
                : base(repository) { }

        public Address Generate() {
            var result = new Address {
                    Country = "Brasil",
                    District = "",
                    IsDefault = false,
                    State = "Rio de Janeiro",
                    Status = default(AddressStatus),
                    Street = "",
                    StreetComplement = "",
                    StreetNumber = 0,
                    PostalCode = "00000-000"
            };
            return result;
        }

        public Tuple<bool, IEnumerable<ValidationResult>> SaveOrUpdate(Address address) {
            Repository.SaveOrUpdate(address);
            throw new NotImplementedException();
        }
        public Tuple<bool, IEnumerable<ValidationResult>> Delete(Address address) {
            Repository.Delete(address);
            throw new NotImplementedException();
        }
    }
}
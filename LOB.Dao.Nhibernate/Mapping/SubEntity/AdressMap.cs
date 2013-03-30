#region Usings
using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class AddressMap : BaseEntityMap<Address> {

        public AddressMap() {
            this.Map(x => x.Status);
            this.Map(x => x.Street);
            this.Map(x => x.StreetNumber);
            this.Map(x => x.StreetComplement);
            this.Map(x => x.District);
            this.Map(x => x.City);
            this.Map(x => x.State);
            this.Map(x => x.Country);
            this.Map(x => x.ZipCode);
            this.Map(x => x.IsDefault);
        }

    }
}
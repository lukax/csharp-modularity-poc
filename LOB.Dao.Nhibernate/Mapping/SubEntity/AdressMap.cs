#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity
{
    public class AddressMap : BaseEntityMap<Address>
    {
        public AddressMap() {
            Map(x => x.Status);
            Map(x => x.Street);
            Map(x => x.StreetNumber);
            Map(x => x.StreetComplement);
            Map(x => x.District);
            Map(x => x.City);
            Map(x => x.State);
            Map(x => x.Country);
            Map(x => x.ZipCode);
            Map(x => x.IsDefault);
        }
    }
}
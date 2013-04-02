#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class ShipperMap : BaseEntityMap<Shipper> {

        public ShipperMap() {
            Map(x => x.Address);
            Map(x => x.ContactInfo);
        }

    }
}
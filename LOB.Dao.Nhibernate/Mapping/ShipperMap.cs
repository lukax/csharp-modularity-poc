#region Usings

using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class ShipperMap : SubclassMap<Shipper> {

        public ShipperMap() {
            References(x => x.Address);
            References(x => x.ContactInfo);
        }

    }
}
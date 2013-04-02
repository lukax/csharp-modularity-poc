#region Usings

using FluentNHibernate.Mapping;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class ShipmentInfoMap : SubclassMap<ShipmentInfo> {

        public ShipmentInfoMap() {
            Map(x => x.Shipper);
            Map(x => x.Status);
            References(x => x.Address);
            Map(x => x.DeliverDate);
            Map(x => x.DaySchedule);
            HasMany(x => x.Products);
        }

    }
}
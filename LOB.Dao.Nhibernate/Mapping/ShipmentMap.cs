#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class ShipmentMap : BaseEntityMap<Shipment> {
        public ShipmentMap() {
            References(x => x.Shipper);
            Map(x => x.Status);
            Map(x => x.DeliverDate);
            Map(x => x.ScheduleDate);
            HasMany(x => x.Products);
        }
    }
}
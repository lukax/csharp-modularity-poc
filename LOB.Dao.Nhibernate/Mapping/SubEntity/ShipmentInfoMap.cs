#region Usings
using FluentNHibernate.Mapping;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class ShipmentInfoMap : SubclassMap<ShipmentInfo> {

        public ShipmentInfoMap() {
            this.Map(x => x.Status);
            References(x => x.Address);
            this.Map(x => x.DeliverDate);
            this.Map(x => x.DaySchedule);
            HasMany(x => x.Products);
        }

    }
}
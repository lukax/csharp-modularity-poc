#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class PayCheckMap : BaseEntityMap<Paycheck> {
        public PayCheckMap() {
            Map(x => x.CurrentSalary);
            Map(x => x.Bonus);
            Map(x => x.Description);
        }
    }
}
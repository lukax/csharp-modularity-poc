#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class PayCheckMap : BaseEntityMap<PayCheck> {

        public PayCheckMap() {
            Map(x => x.CurrentSalary);
            Map(x => x.Bonus);
            Map(x => x.Ps);
        }

    }
}
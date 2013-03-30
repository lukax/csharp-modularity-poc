#region Usings
using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class PayCheckMap : BaseEntityMap<PayCheck> {

        public PayCheckMap() {
            this.Map(x => x.CurrentSalary);
            this.Map(x => x.Bonus);
            this.Map(x => x.Ps);
        }

    }
}
#region Usings
using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class EmployeeMap : BaseEntityMap<Employee> {

        public EmployeeMap() {
            References(x => x.WorksIn);
            this.Map(x => x.Title);
            this.Map(x => x.HireDate);
            References(x => x.PayCheck).Cascade.All();
            this.Map(x => x.Password);
        }

    }
}
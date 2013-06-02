#region Usings

using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class EmployeeMap : SubclassMap<Employee> {
        public EmployeeMap() {
            References(x => x.AssociatedCompany);
            Map(x => x.Title);
            Map(x => x.HireDate);
            References(x => x.Paycheck).Cascade.All();
            Map(x => x.Password);
        }
    }
}
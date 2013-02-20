using FluentNHibernate.Mapping;
using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

namespace LOB.Dao.Nhibernate.Mapping
{
    public class EmployeeMap : BaseEntityMap<Employee>
    {
        public EmployeeMap()
        {
            References(x => x.Person);
            References(x => x.Store);
            Map(x => x.Title);
            Map(x => x.HireDate);
            References(x => x.PayCheck)
                .Cascade.All();
            Map(x => x.Password);
        }
    }
}
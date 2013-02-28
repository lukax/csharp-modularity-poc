#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping
{
    public class EmployeeMap : BaseEntityMap<Employee>
    {
        public EmployeeMap()
        {
            References(x => x.Person);
            References(x => x.WorksIn);
            Map(x => x.Title);
            Map(x => x.HireDate);
            References(x => x.PayCheck)
                .Cascade.All();
            Map(x => x.Password);
        }
    }
}
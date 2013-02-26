#region Usings

using LOB.Domain;

#endregion

namespace LOB.Business
{
    public class EmployeeFacade : EntityFacade<Employee>
    {
        public EmployeeFacade(Employee employee)
            : base(employee) {
        }
    }
}
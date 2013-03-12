#region Usings

using LOB.Business.Interface;
using LOB.Domain;

#endregion

namespace LOB.Business
{
    public class EmployeeFacade : EntityFacade<Employee>, IEmployeeFacade
    {
        public EmployeeFacade(Employee entity)
            : base(entity)
        {
        }
    }
}
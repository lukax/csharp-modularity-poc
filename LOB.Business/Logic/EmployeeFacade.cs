#region Usings

using LOB.Business.Interface;
using LOB.Dao.Interface;
using LOB.Domain;

#endregion

namespace LOB.Business.Logic
{
    public class EmployeeFacade : EntityFacade<Employee>, IEmployeeFacade
    {
        private readonly IUnityOfWork _unityOfWork;

        public EmployeeFacade(IUnityOfWork unityOfWork, Employee entity)
            : base(unityOfWork, entity)
        {
            _unityOfWork = unityOfWork;
        }
    }
}
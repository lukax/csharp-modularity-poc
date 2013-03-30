#region Usings

using LOB.Domain;

#endregion

namespace LOB.Business.Interface.Logic {
    public interface IEmployeeFacade : INaturalPersonFacade {

        new void SetEntity<T>(T entity) where T : Employee;

    }
}
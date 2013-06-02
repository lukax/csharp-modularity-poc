#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(IEmployeeFacade)), Export(typeof(IBaseEntityFacade<Employee>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeFacade : BaseEntityFacade, IEmployeeFacade {
        private readonly IAddressFacade _addressFacade;
        private readonly IPayCheckFacade _payCheckFacade;

        [ImportingConstructor]
        public EmployeeFacade(IAddressFacade addressFacade, IPayCheckFacade payCheckFacade,
                IRepository repository)
                : base(repository) {
            _addressFacade = addressFacade;
            _payCheckFacade = payCheckFacade;
        }

        public Employee Generate() {
            var result = new Employee {
                    Address = _addressFacade.Generate(),
                    BirthDate = DateTime.Now,
                    CPF = "",
                    FirstName = "",
                    LastName = "",
                    HireDate = DateTime.Now,
                    NickName = "",
                    Paycheck = _payCheckFacade.Generate(),
                    RG = "",
                    RGUF = default(UF),
                    Title = "",
                    AssociatedCompany = new Company()
            };
            return result;
        }
    }
}
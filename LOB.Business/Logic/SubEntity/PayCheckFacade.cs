#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IPayCheckFacade)), Export(typeof(IBaseEntityFacade<Paycheck>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class PayCheckFacade : BaseEntityFacade, IPayCheckFacade {
        [ImportingConstructor]
        public PayCheckFacade(IRepository repository)
                : base(repository) { }

        public Paycheck Generate() {
            var result = new Paycheck {CurrentSalary = 0, Description = ""};
            return result;
        }
    }
}
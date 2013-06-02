#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IPayCheckFacade)), Export(typeof(IBaseEntityFacade<Paycheck>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class PayCheckFacade : BaseEntityFacade<Paycheck>, IPayCheckFacade {
        [ImportingConstructor]
        public PayCheckFacade(IRepository repository)
                : base(repository) { }

        public override Paycheck GenerateEntity() {
            Paycheck result = base.GenerateEntity();
            result.CurrentSalary = 0;
            result.Description = "";
            return result;
        }

    }
}
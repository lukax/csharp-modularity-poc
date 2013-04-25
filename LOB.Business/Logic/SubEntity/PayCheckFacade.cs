#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    [Export(typeof(IPayCheckFacade)), Export(typeof(IBaseEntityFacade<PayCheck>))]
    public sealed class PayCheckFacade : BaseEntityFacade<PayCheck>, IPayCheckFacade {
        [ImportingConstructor]
        public PayCheckFacade(IRepository repository)
            : base(repository) { ConfigureValidations(); }

        public override PayCheck GenerateEntity() {
            var result = base.GenerateEntity();
            result.CurrentSalary = 0;
            result.PS = "";
            return result;
        }

        public void ConfigureValidations() {
            AddValidation((sender, name) => Entity.Bonus > 30000 ? new ValidationResult("Bonus", Strings.Notification_Field_TooLong) : null);
            AddValidation(
                (sender, name) => Entity.CurrentSalary < 0 ? new ValidationResult("CurrentSalary", Strings.Notification_Field_Negative) : null);
            AddValidation((sender, name) => string.IsNullOrWhiteSpace(Entity.PS) ? new ValidationResult("PS", Strings.Notification_Field_Empty) : null);
        }
    }
}
#region Usings

using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public class AlterPayCheckViewModel : AlterBaseEntityViewModel<PayCheck>
    {
        public AlterPayCheckViewModel(PayCheck payCheck, IRepository repository)
            : base(payCheck, repository) {
        }

        public override bool CanSaveChanges(object arg) {
            //TODO: Business logic
            return true;
        }

        public override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        public override void InitializeServices() {
        }

        public override void Refresh() {
        }
    }
}
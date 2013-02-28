#region Usings

using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public sealed class AlterPayCheckViewModel : AlterBaseEntityViewModel<PayCheck>
    {
        public AlterPayCheckViewModel(PayCheck payCheck, IRepository repository)
            : base(payCheck, repository)
        {
        }

        protected override bool CanSaveChanges(object arg)
        {
            //TODO: Business logic
            return true;
        }

        protected override bool CanCancel(object arg)
        {
            //TODO: Business logic
            return true;
        }

        protected override void QuickSearch(object arg)
        {
            throw new System.NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            throw new System.NotImplementedException();
        }

    }
}
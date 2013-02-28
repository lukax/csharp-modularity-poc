#region Usings

using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public sealed class AlterAddressViewModel : AlterBaseEntityViewModel<Address>
    {

        public AlterAddressViewModel(Address entity, IRepository repository)
            : base(entity, repository)
        {
        }

        protected override void SaveChanges(object arg)
        {
            using (Repository.Uow)
            {
                Repository.Uow.BeginTransaction();
                Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
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
#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public sealed class AlterCustomerViewModel : AlterBaseEntityViewModel<Customer>
    {
        [ImportingConstructor]
        public AlterCustomerViewModel(Customer client, IRepository repository,
                                    AlterAddressViewModel alterAddressViewModel,
                                    AlterContactInfoViewModel alterContactInfoViewModel)
            : base(client, repository)
        {
        }

        protected override bool CanSaveChanges(object arg)
        {
            return true;
        }

        protected override bool CanCancel(object arg)
        {
            return true;
        }

        protected override void SaveChanges(object arg)
        {
            using (Repository.Uow)
            {
                Repository.Uow.BeginTransaction();
                Repository.Uow.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void QuickSearch(object arg)
        {
            throw new System.NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            Entity = new Customer();
        }
    }
}
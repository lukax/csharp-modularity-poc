#region Usings

using System.ComponentModel.Composition;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
{
    [Export]
    public abstract class AlterPersonViewModel<T> : AlterBaseEntityViewModel<T> where T : Person
    {
        private IUnityContainer _container;
        [ImportingConstructor]
        public AlterPersonViewModel(T entity, Address entityAddress, ContactInfo entityContactInfo,
                                    IRepository repository,
                                    AlterAddressViewModel alterAddressViewModel,
                                    AlterContactInfoViewModel alterContactInfoViewModel, IUnityContainer container)
            : base(entity, repository)
        {
            _container = container;
            AlterAddressViewModel = alterAddressViewModel;
            AlterContactInfoViewModel = alterContactInfoViewModel;

            Entity.Address = entityAddress;
            Entity.ContactInfo = entityContactInfo;

            AlterAddressViewModel.Entity = this.Entity.Address;
            AlterContactInfoViewModel.Entity = this.Entity.ContactInfo;
        }

        public AlterAddressViewModel AlterAddressViewModel { get; set; }
        public AlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

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
            Messenger.Default.Send<object>(_container.Resolve<ListPersonViewModel<Person>>(), "QuickSearchCommand");
        }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }
    }
}
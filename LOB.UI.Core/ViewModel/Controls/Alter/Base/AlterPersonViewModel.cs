#region Usings

using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
{
    public abstract class AlterPersonViewModel<T> : AlterBaseEntityViewModel<T>, IAlterPersonViewModel<T>
        where T : Person
    {
        private IUnityContainer _container;

        [InjectionConstructor]
        public AlterPersonViewModel(T entity, Address address, ContactInfo contactInfo,
                                    IRepository repository,
                                    AlterAddressViewModel alterAddressViewModel,
                                    AlterContactInfoViewModel alterContactInfoViewModel, IUnityContainer container)
            : base(entity, repository)
        {
            _container = container;
            AlterAddressViewModel = alterAddressViewModel;
            AlterContactInfoViewModel = alterContactInfoViewModel;

            Entity.Address = address;
            Entity.ContactInfo = contactInfo;
            //TODO: Use business logic to set default params
            if (Entity.Address.State == null && Entity.Address.Country == null)
            {
                Entity.Address.Country = "Brasil";
                Entity.Address.State = UfBrDictionary.Ufs[UfBr.RJ];
            }

            AlterAddressViewModel.Entity = Entity.Address;
            AlterContactInfoViewModel.Entity = Entity.ContactInfo;
        }

        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
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
            //Messenger.Default.Send<object>(_container.Resolve<ListPersonViewModel<Person>>(), "QuickSearchCommand");
        }
    }
}
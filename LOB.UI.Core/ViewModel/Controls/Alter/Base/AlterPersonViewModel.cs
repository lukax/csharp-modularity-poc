#region Usings
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    public abstract class AlterPersonViewModel : AlterBaseEntityViewModel<Person>, IAlterPersonViewModel {

        private IUnityContainer _container;

        [InjectionConstructor] public AlterPersonViewModel(Person entity, Address address, ContactInfo contactInfo,
            IRepository repository, AlterAddressViewModel alterAddressViewModel,
            AlterContactInfoViewModel alterContactInfoViewModel, IUnityContainer container)
            : base(entity, repository) {
            this._container = container;
            this.AlterAddressViewModel = alterAddressViewModel;
            this.AlterContactInfoViewModel = alterContactInfoViewModel;

            this.Entity.Address = address;
            this.Entity.ContactInfo = contactInfo;
            //TODO: Use business logic to set default params
            if(this.Entity.Address.State == null && this.Entity.Address.Country == null) {
                this.Entity.Address.Country = "Brasil";
                this.Entity.Address.State = UfBrDictionary.Ufs[UfBr.RJ];
            }

            //AlterAddressViewModel = Entity.Address;
            //AlterContactInfoViewModel.Entity = Entity.ContactInfo;
        }

        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

        public override void InitializeServices() {}

        public override void Refresh() {}

        protected override void SaveChanges(object arg) {
            using(this.Repository.Uow) {
                this.Repository.Uow.BeginTransaction();
                this.Repository.SaveOrUpdate(this.Entity);
                this.Repository.Uow.CommitTransaction();
            }
        }

        protected override bool CanSaveChanges(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void QuickSearch(object arg) {
            //Messenger.Default.Send<object>(_container.Resolve<ListPersonViewModel<Person>>(), "QuickSearchCommand");
        }

    }
}
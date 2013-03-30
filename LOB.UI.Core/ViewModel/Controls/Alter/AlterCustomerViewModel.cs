#region Usings
using System.Threading.Tasks;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterCustomerViewModel : AlterBaseEntityViewModel<Customer>, IAlterCustomerViewModel {

        private readonly AlterLegalPersonViewModel _alterLegalPersonViewModel;
        private readonly AlterNaturalPersonViewModel _alterNaturalPersonViewModel;
        private ICommandService _commandService;
        private IUnityContainer _container;
        private readonly IFluentNavigator _navigator;

        [InjectionConstructor] public AlterCustomerViewModel(Customer entity, IRepository repository,
            IUnityContainer container, IFluentNavigator navigator, AlterLegalPersonViewModel alterLegalPersonViewModel,
            AlterNaturalPersonViewModel alterNaturalPersonViewModel, ICommandService commandService)
            : base(entity, repository) {
            this._navigator = navigator;
            this._container = container;
            this._commandService = commandService;
            this._alterLegalPersonViewModel = alterLegalPersonViewModel;
            this._alterNaturalPersonViewModel = alterNaturalPersonViewModel;
            //default init customer as natural person
            this.NaturalPersonCfg();
            this.PersonTypeChanged();
        }

        public override void InitializeServices() {}

        public override void Refresh() {
            this.Entity = new Customer();
        }

        public override OperationType OperationType {
            get { return OperationType.AlterCustomer; }
        }

        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

        private void PersonTypeChanged() {
            this.Entity.PropertyChanged += (s, e) => {
                switch(this.Entity.PersonType) {
                    case PersonType.Natural:
                        this.NaturalPersonCfg();
                        break;
                    case PersonType.Legal:
                        this.LegalPersonCfg();
                        break;
                }
            };
        }

        private async void LegalPersonCfg() {
            await Task.Delay(500);
            var viewL =
                this._navigator.ResolveView(OperationType.AlterLegalPerson)
                    .SetViewModel(this._alterLegalPersonViewModel)
                    .GetView();
            //Messenger.Default.Send<object>(viewL, "PersonTypeChanged");

            this.Entity.Person = this._alterLegalPersonViewModel.Entity;
        }

        private async void NaturalPersonCfg() {
            await Task.Delay(500);
            var viewN =
                this._navigator.ResolveView(OperationType.AlterNaturalPerson)
                    .SetViewModel(this._alterNaturalPersonViewModel)
                    .GetView();
            //Messenger.Default.Send<object>(viewN, "PersonTypeChanged");

            this.Entity.Person = this._alterNaturalPersonViewModel.Entity;
        }

        protected override bool CanSaveChanges(object arg) {
            return true;
        }

        protected override bool CanCancel(object arg) {
            return true;
        }

        protected override void SaveChanges(object arg) {
            using(this.Repository.Uow) {
                this.Repository.Uow.BeginTransaction();
                this.Repository.Uow.SaveOrUpdate(this.Entity);
                this.Repository.Uow.CommitTransaction();
            }
        }

        protected override void QuickSearch(object arg) {
            //_commandService.Execute("QuickSearch", OperationName.ListCustomer);
        }

        protected override void ClearEntity(object arg) {
            this.Entity = new Customer();
        }

    }
}
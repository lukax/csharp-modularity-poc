#region Usings

using System.Threading.Tasks;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterCustomerViewModel : AlterBaseEntityViewModel<Customer>, IAlterCustomerViewModel {

        private readonly AlterLegalPersonViewModel _alterLegalPersonViewModel;
        private readonly AlterNaturalPersonViewModel _alterNaturalPersonViewModel;
        private readonly IEventAggregator _eventAggregator;
        private readonly IFluentNavigator _navigator;
        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }
        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Customer,
            State = UIOperationState.Add
        };

        [InjectionConstructor]
        public AlterCustomerViewModel(Customer entity, IRepository repository, IFluentNavigator navigator,
            AlterLegalPersonViewModel alterLegalPersonViewModel, AlterNaturalPersonViewModel alterNaturalPersonViewModel,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _navigator = navigator;
            _eventAggregator = eventAggregator;
            _alterLegalPersonViewModel = alterLegalPersonViewModel;
            _alterNaturalPersonViewModel = alterNaturalPersonViewModel;
            //default init customer as natural person
            NaturalPersonCfg();
            PersonTypeChanged();
        }

        public override void InitializeServices() { Operation = _operation; }

        public override void Refresh() { Entity = new Customer(); }

        private void PersonTypeChanged() {
            Entity.PropertyChanged += (s, e) => {
                                          switch(Entity.PersonType) {
                                              case PersonType.Natural:
                                                  NaturalPersonCfg();
                                                  break;
                                              case PersonType.Legal:
                                                  LegalPersonCfg();
                                                  break;
                                          }
                                      };
        }

        private async void LegalPersonCfg() {
            await Task.Delay(500);
            var viewL = _navigator.ResolveView(Operation).SetViewModel(_alterLegalPersonViewModel).GetView();
            //Messenger.Default.Send<object>(viewL, "PersonTypeChanged");
            Entity.Person = _alterLegalPersonViewModel.Entity;
        }

        private async void NaturalPersonCfg() {
            await Task.Delay(500);
            var viewN = _navigator.ResolveView(Operation).SetViewModel(_alterNaturalPersonViewModel).GetView();
            //Messenger.Default.Send<object>(viewN, "PersonTypeChanged");
            Entity.Person = _alterNaturalPersonViewModel.Entity;
        }

        protected override bool CanSaveChanges(object arg) { return true; }

        protected override bool CanCancel(object arg) { return true; }

        protected override void SaveChanges(object arg) {
            using(Repository.Uow.BeginTransaction()) {
                Repository.Uow.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override void ClearEntity(object arg) { Entity = new Customer {}; }

    }
}
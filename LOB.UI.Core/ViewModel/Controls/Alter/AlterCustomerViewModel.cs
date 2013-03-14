#region Usings

using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public sealed class AlterCustomerViewModel : AlterBaseEntityViewModel<Customer>, IAlterCustomerViewModel
    {
        private AlterLegalPersonViewModel _alterLegalPersonViewModel;
        private AlterNaturalPersonViewModel _alterNaturalPersonViewModel;
        private ICommandService _commandService;
        private IUnityContainer _container;
        private IFluentNavigator _navigator;

        [InjectionConstructor]
        public AlterCustomerViewModel(Customer entity, IRepository repository, IUnityContainer container,
                                      IFluentNavigator navigator,
                                      AlterLegalPersonViewModel alterLegalPersonViewModel,
                                      AlterNaturalPersonViewModel alterNaturalPersonViewModel,
                                      ICommandService commandService)
            : base(entity, repository)
        {
            _navigator = navigator;
            _container = container;
            _commandService = commandService;
            _alterLegalPersonViewModel = alterLegalPersonViewModel;
            _alterNaturalPersonViewModel = alterNaturalPersonViewModel;
            //default init customer as natural person
            Header = "ASDPOKDSAP";
            NaturalPersonCfg();
            PersonTypeChanged();
        }
        
        private void PersonTypeChanged()
        {
            Entity.PropertyChanged += (s, e) =>
                {
                    switch (Entity.PersonType)
                    {
                        case PersonType.Natural:
                            NaturalPersonCfg();
                            break;
                        case PersonType.Legal:
                            LegalPersonCfg();
                            break;
                    }
                };
        }

        private async void LegalPersonCfg()
        {
            await Task.Delay(500);
            var viewL = _navigator.ResolveView("AlterLegalPerson").SetViewModel(_alterLegalPersonViewModel).GetView();
            Messenger.Default.Send<object>(viewL, "PersonTypeChanged");

            Entity.Person = _alterLegalPersonViewModel.Entity;
        }

        private async void NaturalPersonCfg()
        {
            await Task.Delay(500);
            var viewN =
                _navigator.ResolveView("AlterNaturalPerson").SetViewModel(_alterNaturalPersonViewModel).GetView();
            Messenger.Default.Send<object>(viewN, "PersonTypeChanged");

            Entity.Person = _alterNaturalPersonViewModel.Entity;
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
            _commandService["QuickSearch"].Execute(
                _container.Resolve<ListCustomerViewModel>(new ParameterOverride("entity", Entity)));
            //Messenger.Default.ExecuteCommand<object>(_container.ResolveView<ListCustomerViewModel>(), "QuickSearchCommand");
        }

        protected override void ClearEntity(object arg)
        {
            Entity = new Customer();
        }
    }
}
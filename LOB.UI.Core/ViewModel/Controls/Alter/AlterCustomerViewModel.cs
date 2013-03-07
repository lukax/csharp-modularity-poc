#region Usings

using System.ComponentModel.Composition;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public sealed class AlterCustomerViewModel : AlterBaseEntityViewModel<Customer>
    {
        private AlterLegalPersonViewModel _alterLegalPersonViewModel;
        private AlterNaturalPersonViewModel _alterNaturalPersonViewModel;
        private IUnityContainer _container;
        private IFluentNavigator _navigator;

        [ImportingConstructor]
        public AlterCustomerViewModel(Customer client, IRepository repository, IUnityContainer container,
                                      IFluentNavigator navigator,
                                      AlterLegalPersonViewModel alterLegalPersonViewModel,
                                      AlterNaturalPersonViewModel alterNaturalPersonViewModel)
            : base(client, repository)
        {
            _navigator = navigator;
            _container = container;
            _alterLegalPersonViewModel = alterLegalPersonViewModel;
            _alterNaturalPersonViewModel = alterNaturalPersonViewModel;
            //default init customer as natural person
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
            var viewL = _navigator.Resolve("AlterLegalPerson").SetViewModel(_alterLegalPersonViewModel).Get();
            Messenger.Default.Send<object>(viewL, "PersonTypeChanged");

            Entity.Person = _alterLegalPersonViewModel.Entity;
        }
        private async void NaturalPersonCfg()
        {
            await Task.Delay(500);
            var viewN = _navigator.Resolve("AlterNaturalPerson").SetViewModel(_alterNaturalPersonViewModel).Get();
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
            Messenger.Default.Send<object>(_container.Resolve<ListCustomerViewModel>(), "QuickSearchCommand");
        }

        protected override void ClearEntity(object arg)
        {
            Entity = new Customer();
        }
    }
}
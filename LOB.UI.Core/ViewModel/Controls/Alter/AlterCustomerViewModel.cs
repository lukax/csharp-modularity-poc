#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.UI.Core.View.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
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
        private INavigator _navigator;

        [ImportingConstructor]
        public AlterCustomerViewModel(Customer client, IRepository repository, IUnityContainer container, INavigator navigator,
            AlterLegalPersonViewModel alterLegalPersonViewModel,
            AlterNaturalPersonViewModel alterNaturalPersonViewModel )
            : base(client, repository)
        {
            _navigator = navigator;
            _container = container;
            _alterLegalPersonViewModel = alterLegalPersonViewModel;
            _alterNaturalPersonViewModel = alterNaturalPersonViewModel;
            PersonTypeChanged();
        }


        private void PersonTypeChanged()
        {
            Entity.PropertyChanged += (s, e) =>
            {
                switch (Entity.PersonType)
                {
                    case PersonType.Legal:
                        dynamic viewL = _navigator.ResolveView("AlterLegalPerson").GetView;
                        viewL.DataContext = _alterLegalPersonViewModel;
                        Messenger.Default.Send<object>(viewL, "PersonTypeChanged");
                        
                        Entity.Person = _alterLegalPersonViewModel.Entity;
                        break;
                    case PersonType.Natural:
                        dynamic viewN = _navigator.ResolveView("AlterNaturalPerson").GetView;
                        viewN.DataContext = _alterNaturalPersonViewModel;
                        Messenger.Default.Send<object>(viewN, "PersonTypeChanged");

                        Entity.Person = _alterNaturalPersonViewModel.Entity;
                        break;
                }
            };
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
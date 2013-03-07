#region Usings

using System.ComponentModel.Composition;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public sealed class AlterLegalPersonViewModel : AlterPersonViewModel<LegalPerson>
    {
        private IUnityContainer _container;

        [ImportingConstructor]
        public AlterLegalPersonViewModel(LegalPerson entity, Address address, ContactInfo contactInfo,
                                         IRepository repository,
                                         AlterAddressViewModel alterAddressViewModel,
                                         AlterContactInfoViewModel alterContactInfoViewModel, IUnityContainer container)
            : base(entity, address, contactInfo, repository, alterAddressViewModel, alterContactInfoViewModel, container
                )
        {
            _container = container;
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

        protected override void QuickSearch(object arg)
        {
            Messenger.Default.Send<object>(_container.Resolve<ListLegalPersonViewModel>(), "QuickSearchCommand");
        }

        protected override void ClearEntity(object arg)
        {
            Entity = new LegalPerson();
        }
    }
}
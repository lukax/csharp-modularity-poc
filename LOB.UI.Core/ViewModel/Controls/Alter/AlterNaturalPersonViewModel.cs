#region Usings

using System;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public class AlterNaturalPersonViewModel : AlterPersonViewModel<NaturalPerson>, IAlterNaturalPersonViewModel
    {
        private IUnityContainer _container;

        [InjectionConstructor]
        public AlterNaturalPersonViewModel(NaturalPerson entity, Address address, ContactInfo contactInfo,
                                           IRepository repository,
                                           AlterAddressViewModel alterAddressViewModel,
                                           AlterContactInfoViewModel alterContactInfoViewModel,
                                           IUnityContainer container)
            : base(entity, address, contactInfo, repository, alterAddressViewModel, alterContactInfoViewModel, container
                )
        {
            _container = container;
        }

        public string BirthDate
        {
            get { return Entity.BirthDate.ToShortDateString(); }
            set
            {
                if (Entity.BirthDate.ToShortDateString() == value) return;

                DateTime parsed;
                if (DateTime.TryParse(value, out parsed))
                {
                    Entity.BirthDate = parsed;
                    OnPropertyChanged();
                }
            }
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
            Messenger.Default.Send<object>(_container.Resolve<ListNaturalPersonViewModel>(),
                                           "QuickSearchCommand");
        }

        protected override void ClearEntity(object arg)
        {
            Entity = new NaturalPerson();
        }
    }
}
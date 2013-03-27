#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>, IAlterContactInfoViewModel {
        private readonly IContactInfoFacade _contactInfoFacade;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRepository _repository;

        public AlterContactInfoViewModel(ContactInfo entity, IRepository repository,
                                         IContactInfoFacade contactInfoFacade, IEventAggregator eventAggregator)
            : base(entity, repository) {
            _repository = repository;
            _contactInfoFacade = contactInfoFacade;
            _eventAggregator = eventAggregator;
            Entity = entity;
            AddEmailCommand = new DelegateCommand(AddEmail, CanAddEmail);
            DeleteEmailCommand = new DelegateCommand(DeleteEmail, CanDeleteEmail);
            AddPhoneNumberCommand = new DelegateCommand(AddPhoneNumber, CanAddPhoneNumber);
            DeletePhoneNumberCommand = new DelegateCommand(DeletePhoneNumber, CanDeletePhoneNumber);
        }

        public ICommand AddEmailCommand { get; set; }
        public ICommand DeleteEmailCommand { get; set; }
        public ICommand AddPhoneNumberCommand { get; set; }
        public ICommand DeletePhoneNumberCommand { get; set; }

        [AllowNull]
        public Email Email { get; set; }

        [AllowNull]
        public PhoneNumber PhoneNumber { get; set; }

        [AllowNull]
        public ICollectionView Emails { get; set; }

        [AllowNull]
        public ICollectionView PhoneNumbers { get; set; }

        public override void InitializeServices() {
            Refresh();
            UpdateLists().GetAwaiter();
        }

        public override void Refresh() {
            Entity = new ContactInfo
                {
                    Emails = new List<Email>(),
                    PhoneNumbers = new List<PhoneNumber>(),
                    SpeakWith = "",
                    Ps = "",
                    WebSite = "",
                };
            _contactInfoFacade.SetEntity(Entity);
            _contactInfoFacade.ConfigureValidations();
        }

        public override OperationType OperationType {
            get { return OperationType.AlterContactInfo; }
        }

        #region Sub

        private void AddEmail(object arg) {
            _eventAggregator.GetEvent<OpenViewEvent>().Publish(OperationType.AlterEmail);
        }

        private bool CanAddEmail(object arg) {
            //TODO: Business logic
            return true;
        }

        private void AddPhoneNumber(object arg) {
            _eventAggregator.GetEvent<OpenViewEvent>().Publish(OperationType.AlterPhoneNumber);
        }

        private bool CanAddPhoneNumber(object arg) {
            //TODO: Business logic
            return true;
        }

        private void DeleteEmail(object arg) {
            //TODO: Verify if can delete
            if (Email != null)
                using (Repository.Uow) {
                    Repository.Uow.BeginTransaction();
                    Repository.Delete(Email);
                    Repository.Uow.CommitTransaction();
                }
        }

        private bool CanDeleteEmail(object arg) {
            if (Email != null)
                return true;
            return false;
        }

        private void DeletePhoneNumber(object arg) {
            //TODO: Verify if can delete
            if (PhoneNumber != null)
                using (Repository.Uow) {
                    Repository.Uow.BeginTransaction();
                    Repository.Delete(PhoneNumber);
                    Repository.Uow.CommitTransaction();
                }
        }

        private bool CanDeletePhoneNumber(object arg) {
            if (PhoneNumber != null)
                return true;
            return false;
        }

        #endregion

        private async Task UpdateLists() {
            while (true) {
                await Task.Delay(2000);

                Emails = new ListCollectionView((await Repository.GetListAsync<Email>()).ToList());
                PhoneNumbers = new ListCollectionView((await Repository.GetListAsync<PhoneNumber>()).ToList());
            }
        }

        protected override void QuickSearch(object arg) {
            _eventAggregator.GetEvent<QuickSearchEvent>().Publish(OperationType.ListContactInfo);
        }

        protected override void ClearEntity(object arg) {
            Refresh();
        }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            return _contactInfoFacade.CanAdd(out results);
        }

        protected override bool CanCancel(object arg) {
            return true;
        }
    }
}
#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Data;
using System.Windows.Input;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>, IAlterContactInfoViewModel {

        private readonly IContactInfoFacade _contactInfoFacade;
        private readonly IEventAggregator _eventAggregator;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly Notification _notification;
        public AlterContactInfoViewModel(ContactInfo entity, IRepository repository, IContactInfoFacade contactInfoFacade,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _contactInfoFacade = contactInfoFacade;
            _eventAggregator = eventAggregator;
            _notification = new Notification();
            Entity = entity;
            EmailOperation = new UIOperation {State = UIOperationState.Add, Type = UIOperationType.Email};
            PhoneNumberOperation = new UIOperation {State = UIOperationState.Add, Type = UIOperationType.PhoneNumber};
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
            Operation = _operation;
            ClearEntity(null);
            InitBackgroundWorker();
        }
        protected override void SaveChanges(object arg)
        {
            using (Repository.Uow.BeginTransaction())
            {
                Entity = Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
            _eventAggregator.GetEvent<NotificationEvent>().Publish(_notification.Message(Strings.Notification_Field_Added).Severity(Severity.Ok));
        }
        public override void Refresh() { ClearEntity(null); }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.ContactInfo, State = UIOperationState.Add};
        #region UI Validations

        public UIOperation EmailOperation { get; set; }
        public UIOperation PhoneNumberOperation { get; set; }

        private void AddEmail(object arg) { _eventAggregator.GetEvent<OpenViewEvent>().Publish(EmailOperation); }

        private bool CanAddEmail(object arg) {
            //TODO: Business logic
            return true;
        }

        private void AddPhoneNumber(object arg) { _eventAggregator.GetEvent<OpenViewEvent>().Publish(PhoneNumberOperation); }

        private bool CanAddPhoneNumber(object arg) {
            //TODO: Business logic
            return true;
        }

        private void DeleteEmail(object arg) {
            //TODO: Verify if can delete
            if(Email != null)
                using(Repository.Uow) {
                    Repository.Uow.BeginTransaction();
                    Repository.Delete(Email);
                    Repository.Uow.CommitTransaction();
                }
        }

        private bool CanDeleteEmail(object arg) {
            if(Email != null) return true;
            return false;
        }

        private void DeletePhoneNumber(object arg) {
            //TODO: Verify if can delete
            if(PhoneNumber != null)
                using(Repository.Uow) {
                    Repository.Uow.BeginTransaction();
                    Repository.Delete(PhoneNumber);
                    Repository.Uow.CommitTransaction();
                }
        }

        private bool CanDeletePhoneNumber(object arg) {
            if(PhoneNumber != null) return true;
            return false;
        }

        #endregion
        #region Repo Operations

        private void InitBackgroundWorker() {
            _worker.DoWork += WorkerListsGetFromRepo;
            _worker.RunWorkerCompleted += WorkerListsSetFromRepo;
            _worker.ProgressChanged += WorkerListsProgress;
            _worker.WorkerSupportsCancellation = true;
            _worker.WorkerReportsProgress = true;
            _worker.RunWorkerAsync();
        }

        private void WorkerListsGetFromRepo(object sender, DoWorkEventArgs args) {
            do {
                _worker.ReportProgress(-1);
                Thread.Sleep(2000);
                _worker.ReportProgress(5);
                var result = new object[2];
                _worker.ReportProgress(10);
                Emails = new ListCollectionView(Repository.GetList<Email>().ToList());
                _worker.ReportProgress(50);
                PhoneNumbers = new ListCollectionView(Repository.GetList<PhoneNumber>().ToList());
                _worker.ReportProgress(90);
                args.Result = result;
                _worker.ReportProgress(100);
            } while(!_worker.CancellationPending);
        }

        private void WorkerListsSetFromRepo(object sender, RunWorkerCompletedEventArgs args) {
            var result = args.Result as object[];
            if(result != null) {
                Emails = result[0] as ListCollectionView;
                PhoneNumbers = result[1] as ListCollectionView;
            }
        }

        private readonly Notification _singleNotification;
        private void WorkerListsProgress(object sender, ProgressChangedEventArgs args) {
            _singleNotification.Message(args.ProgressPercentage == -1 ? Strings.Notification_List_Updated : Strings.Notification_List_Updating);
            _singleNotification.Progress(args.ProgressPercentage);
            _eventAggregator.GetEvent<NotificationEvent>().Publish(_singleNotification);
        }

        #endregion
        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override void ClearEntity(object arg) {
            Entity = _contactInfoFacade.GenerateEntity();
            _contactInfoFacade.SetEntity(Entity);
            _contactInfoFacade.ConfigureValidations();
        }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            return _contactInfoFacade.CanAdd(out results);
        }

        protected override bool CanCancel(object arg) { return true; }

        ~AlterContactInfoViewModel() { Dispose(false); }

        public override void Dispose() {
            base.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            _worker.CancelAsync();
            if(disposing) _worker.Dispose();
        }

    }
}
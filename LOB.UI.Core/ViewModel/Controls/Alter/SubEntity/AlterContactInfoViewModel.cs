#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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

        private readonly IRepository _repository;
        private readonly IContactInfoFacade _contactInfoFacade;
        private readonly IEventAggregator _eventAggregator;
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public AlterContactInfoViewModel(ContactInfo entity, IRepository repository,
            IContactInfoFacade contactInfoFacade, IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
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
            ClearEntity(null);
            InitBackgroundWorker();
        }

        public override void Refresh() { ClearEntity(null); }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.ContactInfo,
            State = UIOperationState.Add
        };
        public override UIOperation UIOperation {
            get { return _operation; }
        }
        #region UI Validations

        private void AddEmail(object arg) { _eventAggregator.GetEvent<OpenViewEvent>().Publish(UIOperation); }

        private bool CanAddEmail(object arg) {
            //TODO: Business logic
            return true;
        }

        private void AddPhoneNumber(object arg) { _eventAggregator.GetEvent<OpenViewEvent>().Publish(UIOperation); }

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
            _worker.WorkerReportsProgress = true;
            _worker.RunWorkerAsync();
        }

        private void WorkerListsGetFromRepo(object sender, DoWorkEventArgs args) {
            while(!_worker.CancellationPending) {
                _worker.ReportProgress(0);
                Task.Delay(2000);
                _worker.ReportProgress(5);
                var result = new object[2];
                _worker.ReportProgress(10);
                Emails = new ListCollectionView(Repository.GetList<Email>().ToList());
                _worker.ReportProgress(50);
                PhoneNumbers = new ListCollectionView(Repository.GetList<PhoneNumber>().ToList());
                _worker.ReportProgress(90);
                args.Result = result;
                _worker.ReportProgress(100);
            }
        }

        private void WorkerListsSetFromRepo(object sender, RunWorkerCompletedEventArgs args) {
            var result = args.Result as object[];
            if(result != null) {
                Emails = result[0] as ListCollectionView;
                PhoneNumbers = result[1] as ListCollectionView;
            }
        }

        private void WorkerListsProgress(object sender, ProgressChangedEventArgs args) {
            var k = new Progress {Message = Strings.Progress_List_Updating, Percentage = args.ProgressPercentage};
            _eventAggregator.GetEvent<ReportProgressEvent>().Publish(k);
        }

        #endregion
        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(UIOperation); }

        protected override void QuickSearch(object arg) { _eventAggregator.GetEvent<QuickSearchEvent>().Publish(UIOperation); }

        protected override void ClearEntity(object arg) {
            Entity = new ContactInfo {
                Code = 0,
                Emails = new List<Email>(),
                PhoneNumbers = new List<PhoneNumber>(),
                SpeakWith = "",
                Status = default(ContactStatus),
                Ps = "",
                WebSite = "",
            };
            _contactInfoFacade.SetEntity(Entity);
            _contactInfoFacade.ConfigureValidations();
        }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            return _contactInfoFacade.CanAdd(out results);
        }

        protected override bool CanCancel(object arg) { return true; }

    }
}
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
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>, IAlterContactInfoViewModel {

        private readonly IContactInfoFacade _contactInfoFacade;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRepository _repository;
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public AlterContactInfoViewModel(ContactInfo entity, IRepository repository,
            IContactInfoFacade contactInfoFacade, IEventAggregator eventAggregator)
            : base(entity, repository) {
            this._repository = repository;
            this._contactInfoFacade = contactInfoFacade;
            this._eventAggregator = eventAggregator;
            this.Entity = entity;
            this.AddEmailCommand = new DelegateCommand(this.AddEmail, this.CanAddEmail);
            this.DeleteEmailCommand = new DelegateCommand(this.DeleteEmail, this.CanDeleteEmail);
            this.AddPhoneNumberCommand = new DelegateCommand(this.AddPhoneNumber, this.CanAddPhoneNumber);
            this.DeletePhoneNumberCommand = new DelegateCommand(this.DeletePhoneNumber, this.CanDeletePhoneNumber);
        }

        public ICommand AddEmailCommand { get; set; }
        public ICommand DeleteEmailCommand { get; set; }
        public ICommand AddPhoneNumberCommand { get; set; }
        public ICommand DeletePhoneNumberCommand { get; set; }

        [AllowNull] public Email Email { get; set; }

        [AllowNull] public PhoneNumber PhoneNumber { get; set; }

        [AllowNull] public ICollectionView Emails { get; set; }

        [AllowNull] public ICollectionView PhoneNumbers { get; set; }

        public override void InitializeServices() {
            this.Refresh();
            this.InitBackgroundWorker();
        }

        public override void Refresh() {
            this.Entity = new ContactInfo {
                Emails = new List<Email>(),
                PhoneNumbers = new List<PhoneNumber>(),
                SpeakWith = "",
                Ps = "",
                WebSite = "",
            };
            this._contactInfoFacade.SetEntity(this.Entity);
            this._contactInfoFacade.ConfigureValidations();
        }

        public override OperationType OperationType {
            get { return OperationType.AlterContactInfo; }
        }
        #region UI Validations
        private void AddEmail(object arg) {
            this._eventAggregator.GetEvent<OpenViewEvent>().Publish(OperationType.AlterEmail);
        }

        private bool CanAddEmail(object arg) {
            //TODO: Business logic
            return true;
        }

        private void AddPhoneNumber(object arg) {
            this._eventAggregator.GetEvent<OpenViewEvent>().Publish(OperationType.AlterPhoneNumber);
        }

        private bool CanAddPhoneNumber(object arg) {
            //TODO: Business logic
            return true;
        }

        private void DeleteEmail(object arg) {
            //TODO: Verify if can delete
            if(this.Email != null)
                using(this.Repository.Uow) {
                    this.Repository.Uow.BeginTransaction();
                    this.Repository.Delete(this.Email);
                    this.Repository.Uow.CommitTransaction();
                }
        }

        private bool CanDeleteEmail(object arg) {
            if(this.Email != null) return true;
            return false;
        }

        private void DeletePhoneNumber(object arg) {
            //TODO: Verify if can delete
            if(this.PhoneNumber != null)
                using(this.Repository.Uow) {
                    this.Repository.Uow.BeginTransaction();
                    this.Repository.Delete(this.PhoneNumber);
                    this.Repository.Uow.CommitTransaction();
                }
        }

        private bool CanDeletePhoneNumber(object arg) {
            if(this.PhoneNumber != null) return true;
            return false;
        }
        #endregion
        #region Repo Operations
        private void InitBackgroundWorker() {
            this._worker.DoWork += this.WorkerListsGetFromRepo;
            this._worker.RunWorkerCompleted += this.WorkerListsSetFromRepo;
            this._worker.ProgressChanged += this.WorkerListsProgress;
            this._worker.WorkerReportsProgress = true;
            this._worker.RunWorkerAsync();
        }

        private void WorkerListsGetFromRepo(object sender, DoWorkEventArgs args) {
            while(!this._worker.CancellationPending) {
                this._worker.ReportProgress(0);
                Task.Delay(2000);
                this._worker.ReportProgress(5);
                var result = new object[2];
                this._worker.ReportProgress(10);
                this.Emails = new ListCollectionView(this.Repository.GetList<Email>().ToList());
                this._worker.ReportProgress(50);
                this.PhoneNumbers = new ListCollectionView(this.Repository.GetList<PhoneNumber>().ToList());
                this._worker.ReportProgress(90);
                args.Result = result;
                this._worker.ReportProgress(100);
            }
        }

        private void WorkerListsSetFromRepo(object sender, RunWorkerCompletedEventArgs args) {
            var result = args.Result as object[];
            if(result != null) {
                this.Emails = result[0] as ListCollectionView;
                this.PhoneNumbers = result[1] as ListCollectionView;
            }
        }

        private void WorkerListsProgress(object sender, ProgressChangedEventArgs args) {
            var k = new Progress {Message = Strings.Progress_List_Updating, Percentage = args.ProgressPercentage};
            this._eventAggregator.GetEvent<ReportProgressEvent>().Publish(k);
        }
        #endregion
        protected override void QuickSearch(object arg) {
            this._eventAggregator.GetEvent<QuickSearchEvent>().Publish(OperationType.ListContactInfo);
        }

        protected override void ClearEntity(object arg) {
            this.Refresh();
        }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            return this._contactInfoFacade.CanAdd(out results);
        }

        protected override bool CanCancel(object arg) {
            return true;
        }

    }
}
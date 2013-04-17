#region Usings

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
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

//

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>, IAlterContactInfoViewModel {
        public AlterContactInfoViewModel(IRepository repository, IContactInfoFacade contactInfoFacade, IEventAggregator eventAggregator,
            ILoggerFacade logger)
            : base(contactInfoFacade, repository, eventAggregator, logger) {
            EmailOperation = new ViewID {State = ViewState.Add, Type = ViewType.Email};
            PhoneNumberOperation = new ViewID {State = ViewState.Add, Type = ViewType.PhoneNumber};
            AddEmailCommand = new DelegateCommand(AddEmail, CanAddEmail);
            DeleteEmailCommand = new DelegateCommand(DeleteEmail, CanDeleteEmail);
            AddPhoneNumberCommand = new DelegateCommand(AddPhoneNumber, CanAddPhoneNumber);
            DeletePhoneNumberCommand = new DelegateCommand(DeletePhoneNumber, CanDeletePhoneNumber);
        }

        public ICommand AddEmailCommand { get; set; }
        public ICommand DeleteEmailCommand { get; set; }
        public ICommand AddPhoneNumberCommand { get; set; }
        public ICommand DeletePhoneNumberCommand { get; set; }

        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public ICollectionView Emails { get; set; }
        public ICollectionView PhoneNumbers { get; set; }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
            InitBackgroundWorker();
        }
        #region Member Validations

        public ViewID EmailOperation { get; set; }
        public ViewID PhoneNumberOperation { get; set; }

        private void AddEmail(object arg) { EventAggregator.GetEvent<OpenViewEvent>().Publish(EmailOperation); }

        private bool CanAddEmail(object arg) {
            //TODO: Business logic
            return true;
        }

        private void AddPhoneNumber(object arg) { EventAggregator.GetEvent<OpenViewEvent>().Publish(PhoneNumberOperation); }

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
            Worker.DoWork += WorkerListsGetFromRepo;
            Worker.RunWorkerCompleted += WorkerListsSetFromRepo;
            Worker.ProgressChanged += WorkerListsProgress;
            Worker.WorkerSupportsCancellation = true;
            Worker.WorkerReportsProgress = true;
            Worker.RunWorkerAsync();
        }

        private void WorkerListsGetFromRepo(object sender, DoWorkEventArgs args) {
            Worker.ReportProgress(10);
            Repository.Uow.OnError += (o, s) => {
                                          NotificationEvent.Publish(
                                              Notification.Message(s.Description).Detail(s.ErrorMessage).Progress(-1).State(NotificationState.Error));
                                          Worker.CancelAsync();
                                      };
            do {
                Worker.ReportProgress(-1);
                Thread.Sleep(2000);
                Worker.ReportProgress(5);
                var result = new object[2];
                using(Repository.Uow.BeginTransaction())
                    if(!Worker.CancellationPending) {
                        Emails = new ListCollectionView(Repository.GetAll<Email>().ToList());
                        Worker.ReportProgress(50);
                        PhoneNumbers = new ListCollectionView(Repository.GetAll<PhoneNumber>().ToList());
                        Worker.ReportProgress(90);
                        args.Result = result;
                        Worker.ReportProgress(100);
                    }
            } while(!Worker.CancellationPending);
        }

        private void WorkerListsSetFromRepo(object sender, RunWorkerCompletedEventArgs args) {
            var result = args.Result as object[];
            if(result != null) {
                Emails = result[0] as ListCollectionView;
                PhoneNumbers = result[1] as ListCollectionView;
            }
        }

        private void WorkerListsProgress(object sender, ProgressChangedEventArgs args) {
            Notification.Message(args.ProgressPercentage == -1 ? Strings.Notification_List_Updated : Strings.Notification_List_Updating)
                        .State(NotificationState.Info)
                        .Progress(args.ProgressPercentage);
            NotificationEvent.Publish(Notification);
        }

        #endregion
        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.ContactInfo, State = ViewState.Add};
    }
}
#region Usings

using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows.Data;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

//

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterContactInfoViewModel))]
    public sealed class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>, IAlterContactInfoViewModel {
        public AlterContactInfoViewModel() {
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
            base.InitializeServices();
            InitBackgroundWorker();
        }
        #region Member Validations

        private void AddEmail(object arg) {
            //EventAggregator.GetEvent<OpenViewEvent>().Publish(EmailOperation);
        }

        private bool CanAddEmail(object arg) {
            //TODO: Business logic
            return true;
        }

        private void AddPhoneNumber(object arg) {
            //EventAggregator.GetEvent<OpenViewEvent>().Publish(PhoneNumberOperation);
        }

        private bool CanAddPhoneNumber(object arg) {
            //TODO: Business logic
            return true;
        }

        private void DeleteEmail(object arg) {
            //TODO: Verify if can delete
            if(Email != null)
                using(Repository.Value.Uow) {
                    Repository.Value.Uow.BeginTransaction();
                    Repository.Value.Delete(Email);
                    Repository.Value.Uow.CommitTransaction();
                }
        }

        private bool CanDeleteEmail(object arg) {
            if(Email != null) return true;
            return false;
        }

        private void DeletePhoneNumber(object arg) {
            //TODO: Verify if can delete
            if(PhoneNumber != null)
                using(Repository.Value.Uow) {
                    Repository.Value.Uow.BeginTransaction();
                    Repository.Value.Delete(PhoneNumber);
                    Repository.Value.Uow.CommitTransaction();
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
            Repository.Value.Uow.OnError += (o, s) => {
                                                NotificationEvent.Publish(
                                                    Notification.Value.Message(s.Description)
                                                                .Detail(s.ErrorMessage)
                                                                .Progress(-1)
                                                                .State(NotificationState.Error));
                                                Worker.CancelAsync();
                                            };
            do {
                Worker.ReportProgress(-1);
                Thread.Sleep(2000);
                Worker.ReportProgress(5);
                var result = new object[2];
                using(Repository.Value.Uow.BeginTransaction())
                    if(!Worker.CancellationPending) {
                        Emails = new ListCollectionView(Repository.Value.GetAll<Email>().ToList());
                        Worker.ReportProgress(50);
                        PhoneNumbers = new ListCollectionView(Repository.Value.GetAll<PhoneNumber>().ToList());
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
            Notification.Value.Message(args.ProgressPercentage == -1 ? Strings.Notification_List_Updated : Strings.Notification_List_Updating)
                        .State(NotificationState.Info)
                        .Progress(args.ProgressPercentage);
            NotificationEvent.Publish(Notification.Value);
        }

        #endregion
    }
}
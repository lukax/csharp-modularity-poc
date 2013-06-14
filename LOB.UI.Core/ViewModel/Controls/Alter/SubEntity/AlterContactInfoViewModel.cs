#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.Command;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.Event.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Prism.Events;

//

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterContactInfoViewModel)), Export(typeof(AlterBaseEntityViewModel<ContactInfo>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>, IAlterContactInfoViewModel {
        public AlterContactInfoViewModel() {
            AddEmailCommand = new DelegateCommand(AddEmail, CanAddEmail);
            DeleteEmailCommand = new DelegateCommand(DeleteEmail, CanDeleteEmail);
            AddPhoneNumberCommand = new DelegateCommand(AddPhoneNumber, CanAddPhoneNumber);
            DeletePhoneNumberCommand = new DelegateCommand(DeletePhoneNumber, CanDeletePhoneNumber);
            FetchCommand = new DelegateCommand(FetchExecute, CanFetch);
        }

        public ICommand AddEmailCommand { get; set; }
        public ICommand DeleteEmailCommand { get; set; }
        public ICommand AddPhoneNumberCommand { get; set; }
        public ICommand DeletePhoneNumberCommand { get; set; }
        public ICommand FetchCommand { get; set; }

        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        //public IList<Email> Emails { get; set; }
        //public IList<PhoneNumber> PhoneNumbers { get; set; }

        public override void InitializeServices() {
            base.InitializeServices();
            InitBackgroundWorker();
        }

        private SubscriptionToken _subEntityToken;
        private void AddEmail(object arg) {
            Lock();
            EventAggregator.Value.GetEvent<OpenViewEvent>().Publish(new OpenViewPayload(typeof(IAlterEmailViewModel), OnEmailIdFunc));
        }
        private void OnEmailIdFunc(Guid thisViewId) {
            _subEntityToken = EventAggregator.Value.GetEvent<CloseViewEvent>().Subscribe(payload => {
                                                                                             if(thisViewId != payload.ViewId) return;
                                                                                             if(payload.OperatedEntity != null) Entity.Emails.Add(payload.OperatedEntity as Email);
                                                                                             _subEntityToken.Dispose();
                                                                                             Unlock();
                                                                                         }, true);
        }

        private bool CanAddEmail(object arg) { return IsUnlocked; }

        private void AddPhoneNumber(object arg) {
            Lock();
            EventAggregator.Value.GetEvent<OpenViewEvent>().Publish(new OpenViewPayload(typeof(IAlterPhoneNumberViewModel), OnPhoneIdFunc));
        }
        private void OnPhoneIdFunc(Guid thisViewId) {
            _subEntityToken = EventAggregator.Value.GetEvent<CloseViewEvent>().Subscribe(payload => {
                                                                                             if(thisViewId != payload.ViewId) return;
                                                                                             if(payload.OperatedEntity != null)
                                                                                                 Entity.PhoneNumbers.Add(
                                                                                                     payload.OperatedEntity as PhoneNumber);
                                                                                             _subEntityToken.Dispose();
                                                                                             Unlock();
                                                                                         }, true);
        }

        private bool CanAddPhoneNumber(object arg) { return IsUnlocked; }

        private void DeleteEmail(object arg) {
            Entity.Emails.Remove(Email);
            Email = null;
            OnPropertyChanged("Emails");
            OnPropertyChanged("Entity.Emails");
            OnPropertyChanged("Email");
        }

        private bool CanDeleteEmail(object arg) {
            if(Email != null & IsUnlocked) return true;
            return false;
        }

        private void DeletePhoneNumber(object arg) { Entity.PhoneNumbers.Remove(PhoneNumber); }

        private bool CanDeletePhoneNumber(object arg) {
            if(PhoneNumber != null & IsUnlocked) return true;
            return false;
        }

        private void FetchExecute(object o) { Worker.RunWorkerAsync(); }
        private bool CanFetch(object obj) { return IsUnlocked & !Worker.IsBusy; }
        #region Repo Operations

        private void InitBackgroundWorker() {
            //Worker.DoWork += WorkerListsGetFromRepo;
            //Worker.RunWorkerCompleted += WorkerListsSetFromRepo;
            //Worker.ProgressChanged += WorkerListsProgress;
            //Worker.WorkerSupportsCancellation = true;
            //Worker.WorkerReportsProgress = true;
            //Worker.RunWorkerAsync();
        }

        private void WorkerListsGetFromRepo(object sender, DoWorkEventArgs args) {
            //Worker.ReportProgress(10);
            ////do {
            //Worker.ReportProgress(-1);
            ////Thread.Sleep(2000);
            //Worker.ReportProgress(5);
            //var result = new object[2];
            //Repository.Value.Uow.Initialize();
            //if (!Worker.CancellationPending)
            //{
            //    Emails = new ListCollectionView(Repository.Value.GetAll<Email>().ToList());
            //    Worker.ReportProgress(50);
            //    PhoneNumbers = new ListCollectionView(Repository.Value.GetAll<PhoneNumber>().ToList());
            //    Worker.ReportProgress(90);
            //    args.Result = result;
            //    Worker.ReportProgress(100);
            //}
            ////} while(!Worker.CancellationPending);
            //Worker.ReportProgress(-1);
        }

        private void WorkerListsSetFromRepo(object sender, RunWorkerCompletedEventArgs args) {
            //var result = args.Result as object[];
            //if(result != null) {
            //    Emails = result[0] as ListCollectionView;
            //    PhoneNumbers = result[1] as ListCollectionView;
            //}
        }

        private void WorkerListsProgress(object sender, ProgressChangedEventArgs args) {
            Notification.Value.Message(args.ProgressPercentage == -1 ? Strings.Notification_List_Updated : Strings.Notification_List_Updating)
                        .State(NotificationType.Info)
                        .Progress(args.ProgressPercentage);
            NotificationEvent.Publish(Notification.Value);
        }

        #endregion
    }
}
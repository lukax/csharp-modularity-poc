#region Usings

using System;
using System.ComponentModel;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    public abstract class AlterBaseEntityViewModel<T> : BaseViewModel, IAlterBaseEntityViewModel where T : BaseEntity {

        private UIOperationState _previousState;
        private SubscriptionToken _currentSubscription;
        private UIOperation _operation;
        public T Entity { get; set; }
        public int Index { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand DiscardChangesCommand { get; set; }
        public ICommand ClearEntityCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand QuickSearchCommand { get; set; }
        protected IRepository Repository { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }
        protected ILoggerFacade Logger { get; private set; }
        protected BackgroundWorker Worker { get; private set; }
        protected NotificationEvent NotificationEvent { get { return EventAggregator.GetEvent<NotificationEvent>(); } }
        protected Notification Notification { get; private set; }

        [InjectionConstructor]
        protected AlterBaseEntityViewModel(T entity, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger) {
            EventAggregator = eventAggregator;
            Logger = logger;
            Worker = new BackgroundWorker();
            Notification = new Notification();
            Repository = repository;
            Entity = entity;
            SaveChangesCommand = new DelegateCommand(SaveChanges, CanSaveChanges);
            DiscardChangesCommand = new DelegateCommand(Cancel, CanCancel);
            QuickSearchCommand = new DelegateCommand(QuickSearch, CanQuickSearch);
            ClearEntityCommand = new DelegateCommand(ClearEntity, CanClearEntity);
        }

        protected virtual bool CanCancel(object arg) {
            if(Operation.State == UIOperationState.Add) return true;
            if(Operation.State == UIOperationState.Update) return true;
            return false;
        }
        protected abstract void Cancel(object arg);

        protected virtual bool CanSaveChanges(object arg) { return Entity != null; }
        protected virtual void SaveChanges(object arg) {
            Worker.DoWork += SaveChanges;
            Worker.RunWorkerAsync();
        }
        private void SaveChanges(object sender, DoWorkEventArgs e) {
            NotificationEvent.Publish(Notification.Message(Strings.Notification_Field_Adding).Progress(-2).Severity(AttentionState.Info));
            using(Repository.Uow.BeginTransaction())
                if(!Worker.CancellationPending) {
                    NotificationEvent.Publish(Notification.Progress(50));
                    Entity = Repository.SaveOrUpdate(Entity);
                    NotificationEvent.Publish(Notification.Progress(70));
                    Repository.Uow.CommitTransaction();
                    NotificationEvent.Publish(Notification.Progress(90));
                }
            Operation.State(UIOperationState.Update);
            NotificationEvent.Publish(Notification.Message(Strings.Notification_Field_Added).Progress(100).Severity(AttentionState.Ok));
        }

        protected virtual bool CanQuickSearch(object obj) { return Operation.State != UIOperationState.QuickSearch; }
        protected virtual void QuickSearch(object arg) {
            Operation.State(UIOperationState.QuickSearch);
            EventAggregator.GetEvent<OpenViewEvent>().Publish(Operation);
            _currentSubscription = EventAggregator.GetEvent<CloseViewEvent>().Subscribe(RestoreUIState);
        }

        private void RestoreUIState(UIOperation obj) {
            if(Operation.State == UIOperationState.QuickSearch) {
                Operation.State(_previousState);
                _currentSubscription.Dispose();
            }
        }

        protected virtual bool CanClearEntity(object obj) { return Operation.State == UIOperationState.Add; }
        protected abstract void ClearEntity(object arg);

        public override UIOperation Operation {
            get { return _operation; }
            set {
                _operation = value;
                UIOpChanged(null, new PropertyChangedEventArgs("State"));
                value.PropertyChanged += UIOpChanged;
            }
        }
        private void UIOpChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            if(propertyChangedEventArgs.PropertyName == "State") {
                if(Operation.State == UIOperationState.QuickSearch) return;
                _previousState = Operation.State;
            }
        }
        #region Implementation of IDisposable

        ~AlterBaseEntityViewModel() { Dispose(false); }
        public override void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing) {
            if(Worker.WorkerSupportsCancellation) Worker.CancelAsync();
            if(!disposing) return;
            Worker.Dispose();
        }

        #endregion
    }
}
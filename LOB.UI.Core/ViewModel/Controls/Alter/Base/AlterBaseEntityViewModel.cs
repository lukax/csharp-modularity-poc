#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.Operation;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    [InheritedExport]
    public abstract class AlterBaseEntityViewModel<T> : BaseViewModel, IAlterBaseEntityViewModel<T> where T : BaseEntity {
        private ViewState _previousState;
        private SubscriptionToken _currentSubscription;
        private ViewID _viewID;
        private T _entity;
        public T Entity {
            get { return _entity; }
            set {
                _entity = value;
                EntityChanged();
            }
        }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand DiscardChangesCommand { get; set; }
        public ICommand ClearEntityCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand QuickSearchCommand { get; set; }
        protected IRepository Repository { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }
        protected ILoggerFacade Logger { get; private set; }
        protected BackgroundWorker Worker { get; private set; }
        protected NotificationEvent NotificationEvent {
            get { return EventAggregator.GetEvent<NotificationEvent>(); }
        }
        protected Notification Notification { get; private set; }
        protected IBaseEntityFacade<T> BaseEntityFacade { get; private set; }

        //[ImportingConstructor]
        //protected AlterBaseEntityViewModel(IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
        //    : this(null, repository, eventAggregator, logger) { }

        protected AlterBaseEntityViewModel(IBaseEntityFacade<T> baseEntityFacade, IRepository repository, IEventAggregator eventAggregator,
            ILoggerFacade logger) {
            BaseEntityFacade = baseEntityFacade;
            EventAggregator = eventAggregator;
            Logger = logger;
            Worker = new BackgroundWorker();
            Notification = new Notification();
            Repository = repository;
            SaveChangesCommand = new DelegateCommand(SaveChanges, CanSaveChanges);
            DiscardChangesCommand = new DelegateCommand(Cancel, CanCancel);
            QuickSearchCommand = new DelegateCommand(QuickSearch, CanQuickSearch);
            ClearEntityCommand = new DelegateCommand(ClearEntity, CanClearEntity);
        }

        public override void InitializeServices() {
            if(Entity == null) ClearEntity(null);
            EventAggregator.GetEvent<IncludeEntityEvent>().Subscribe(Include);
        }

        protected virtual void Include(BaseEntity baseEntity) {
            var entity = baseEntity as T;
            if(entity == null) return;
            Entity = entity;
            ViewID.State(ViewState.Update);
        }

        protected virtual bool CanCancel(object arg) {
            if(ViewID.State == ViewState.Add) return true;
            if(ViewID.State == ViewState.Update) return true;
            return false;
        }
        protected virtual void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        protected virtual bool CanSaveChanges(object arg) {
            if(BaseEntityFacade != null) {
                IEnumerable<ValidationResult> results;
                if(ViewID.State == ViewState.Add) return BaseEntityFacade.CanAdd(out results);
                if(ViewID.State == ViewState.Update) return BaseEntityFacade.CanUpdate(out results);
                return false;
            }
            return !ReferenceEquals(Entity, null);
        }
        protected virtual void SaveChanges(object arg) {
            Worker.DoWork += SaveChanges;
            Worker.WorkerSupportsCancellation = true;
            Worker.RunWorkerAsync();
        }
        private void SaveChanges(object sender, DoWorkEventArgs e) {
            ViewID.SubState(ViewSubState.Locked);
            NotificationEvent.Publish(Notification.Message(Strings.Notification_Field_Adding).Detail("").Progress(-2).State(NotificationState.Info));
            Repository.Uow.OnError += (o, s) => {
                                          NotificationEvent.Publish(
                                              Notification.Message(s.Description).Detail(s.ErrorMessage).Progress(-1).State(NotificationState.Error));
                                          Worker.CancelAsync();
                                      };
            using(Repository.Uow.BeginTransaction())
                if(!Worker.CancellationPending) {
                    NotificationEvent.Publish(Notification.Progress(50));
                    Entity = Repository.SaveOrUpdate(Entity);
                    NotificationEvent.Publish(Notification.Progress(70));
                    Repository.Uow.CommitTransaction();
                    NotificationEvent.Publish(Notification.Message(Strings.Notification_Field_Added).Progress(100).State(NotificationState.Ok));
                    ViewID.State(ViewState.Update);
                }
            ViewID.SubState(ViewSubState.Unlocked);
        }

        protected virtual bool CanQuickSearch(object obj) {
            if(ViewID == null) return false;
            return ViewID.State != ViewState.QuickSearch;
        }
        protected virtual void QuickSearch(object arg) {
            ViewID.State(ViewState.QuickSearch);
            EventAggregator.GetEvent<OpenViewEvent>().Publish(ViewID);
            _currentSubscription = EventAggregator.GetEvent<CloseViewEvent>().Subscribe(RestoreUIState);
        }

        private void RestoreUIState(ViewID obj) {
            if(ViewID.State == ViewState.QuickSearch) {
                ViewID.State(_previousState);
                _currentSubscription.Dispose();
            }
        }

        protected virtual bool CanClearEntity(object obj) { return ViewID.State == ViewState.Add; }
        protected virtual void ClearEntity(object arg) { Entity = BaseEntityFacade.GenerateEntity(); }

        protected virtual void EntityChanged() { BaseEntityFacade.Entity = Entity; }

        public override void Refresh() { ClearEntity(null); }

        public override ViewID ViewID {
            get { return _viewID; }
            set {
                _viewID = value;
                UIOpChanged(null, new PropertyChangedEventArgs("State"));
                value.PropertyChanged += UIOpChanged;
            }
        }
        private void UIOpChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            if(propertyChangedEventArgs.PropertyName == "State") {
                if(ViewID.State == ViewState.QuickSearch) return;
                _previousState = ViewID.State;
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
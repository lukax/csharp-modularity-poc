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
using LOB.UI.Core.Event;
using LOB.UI.Core.Event.Operation;
using LOB.UI.Core.Event.View;
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
        private ViewModelInfo _viewModelInfo;
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
        [Import] protected Lazy<IRepository> RepositoryLazy { get; private set; }
        [Import] protected Lazy<IEventAggregator> EventAggregatorLazy { get; private set; }
        [Import] protected Lazy<ILoggerFacade> LoggerLazy { get; private set; }
        protected BackgroundWorker Worker { get; private set; }
        protected NotificationEvent NotificationEvent {
            get { return EventAggregatorLazy.Value.GetEvent<NotificationEvent>(); }
        }
        [Import] protected Lazy<Notification> NotificationLazy { get; private set; }
        [Import] protected IBaseEntityFacade<T> BaseEntityFacade { get; set; }

        //[ImportingConstructor]
        //protected AlterBaseEntityViewModel(IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
        //    : this(null, repository, eventAggregator, logger) { }

        protected AlterBaseEntityViewModel(IBaseEntityFacade<T> baseEntityFacade) {
            if(baseEntityFacade != null) BaseEntityFacade = baseEntityFacade;
            Worker = new BackgroundWorker();
            SaveChangesCommand = new DelegateCommand(SaveChanges, CanSaveChanges);
            DiscardChangesCommand = new DelegateCommand(Cancel, CanCancel);
            QuickSearchCommand = new DelegateCommand(QuickSearch, CanQuickSearch);
            ClearEntityCommand = new DelegateCommand(ClearEntity, CanClearEntity);
        }

        protected AlterBaseEntityViewModel()
            : this(null) { }

        public override void InitializeServices() {
            if(Entity == null) ClearEntity(null);
            EventAggregatorLazy.Value.GetEvent<IncludeEntityEvent>().Subscribe(IncludeEventExecute);
        }

        protected virtual void IncludeEventExecute(IncludeEntityPayload includeEntityPayload) {
            if(includeEntityPayload.ViewId != Id) return;
            var entity = includeEntityPayload.Entity as T;
            if(entity == null) return;
            Entity = entity;
            Info.State(ViewState.Update);
        }

        protected virtual bool CanCancel(object arg) {
            if(Info.ViewState == ViewState.Add) return true;
            if(Info.ViewState == ViewState.Update) return true;
            return false;
        }
        protected virtual void Cancel(object arg) { EventAggregatorLazy.Value.GetEvent<CloseViewEvent>().Publish(Id); }

        protected virtual bool CanSaveChanges(object arg) {
            if(BaseEntityFacade != null) {
                IEnumerable<ValidationResult> results;
                if(Info.ViewState == ViewState.Add) return BaseEntityFacade.CanAdd(out results);
                if(Info.ViewState == ViewState.Update) return BaseEntityFacade.CanUpdate(out results);
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
            Info.SubState(ViewSubState.Locked);
            NotificationEvent.Publish(
                NotificationLazy.Value.Message(Strings.Notification_Field_Adding).Detail("").Progress(-2).State(NotificationState.Info));
            RepositoryLazy.Value.Uow.OnError += (o, s) => {
                                                    NotificationEvent.Publish(
                                                        NotificationLazy.Value.Message(s.Description)
                                                                        .Detail(s.ErrorMessage)
                                                                        .Progress(-1)
                                                                        .State(NotificationState.Error));
                                                    Worker.CancelAsync();
                                                };
            using(RepositoryLazy.Value.Uow.BeginTransaction())
                if(!Worker.CancellationPending) {
                    NotificationEvent.Publish(NotificationLazy.Value.Progress(50));
                    Entity = RepositoryLazy.Value.SaveOrUpdate(Entity);
                    NotificationEvent.Publish(NotificationLazy.Value.Progress(70));
                    RepositoryLazy.Value.Uow.CommitTransaction();
                    NotificationEvent.Publish(
                        NotificationLazy.Value.Message(Strings.Notification_Field_Added).Progress(100).State(NotificationState.Ok));
                    Info.State(ViewState.Update);
                }
            Info.SubState(ViewSubState.Unlocked);
        }

        protected virtual bool CanQuickSearch(object obj) {
            if(Info == null) return false;
            return Info.ViewState != ViewState.QuickSearch;
        }
        protected virtual void QuickSearch(object arg) {
            Info.State(ViewState.QuickSearch);
            var openPayload = new OpenViewPayload(ViewInfoExtension.New(ViewType.Address, new[] {ViewState.QuickSearch}));
            EventAggregatorLazy.Value.GetEvent<OpenViewEvent>().Publish(openPayload);
            _currentSubscription =
                EventAggregatorLazy.Value.GetEvent<CloseViewEvent>().Subscribe(delegate(Guid payloadId) { if(Id == payloadId) RestoreUIState(Info); });
        }

        private void RestoreUIState(ViewModelInfo obj) {
            if(Info.ViewState == ViewState.QuickSearch) {
                Info.State(_previousState);
                _currentSubscription.Dispose();
            }
        }

        protected virtual bool CanClearEntity(object obj) { return Info.ViewState == ViewState.Add; }
        protected virtual void ClearEntity(object arg) { Entity = BaseEntityFacade.GenerateEntity(); }

        protected virtual void EntityChanged() { BaseEntityFacade.Entity = Entity; }

        public override void Refresh() { ClearEntity(null); }

        public override ViewModelInfo Info {
            get {
                return _viewModelInfo ??
                       (_viewModelInfo = new ViewModelInfo {IsChild = true, ViewState = ViewState.Add, ViewSubState = ViewSubState.Locked});
            }
            set {
                _viewModelInfo = value;
                UIOpChanged(null, new PropertyChangedEventArgs("ViewState"));
                value.PropertyChanged += UIOpChanged;
            }
        }
        private void UIOpChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            if(propertyChangedEventArgs.PropertyName == "ViewState") {
                if(Info.ViewState == ViewState.QuickSearch) return;
                _previousState = Info.ViewState;
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
#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Event;
using LOB.UI.Core.Event.Infrastructure;
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
    public abstract class AlterBaseEntityViewModel<TEntity> : BaseViewModel, IAlterBaseEntityViewModel<TEntity> where TEntity : BaseEntity {
        private ViewState _previousState;
        private SubscriptionToken _currentSubscription;
        private ViewModelInfo _viewModelInfo;
        private TEntity _entity;
        public TEntity Entity {
            get { return _entity; }
            protected set {
                _entity = value;
                EntityChanged();
            }
        }
        public ICommand SaveChangesCommand { get; private set; }
        public ICommand DiscardChangesCommand { get; private set; }
        public ICommand ClearEntityCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand QuickSearchCommand { get; private set; }
        [Import] protected Lazy<IBaseEntityFacade<TEntity>> EntityFacade { get; private set; }
        [Import] protected Lazy<IRepository> Repository { get; private set; }
        [Import] protected Lazy<IEventAggregator> EventAggregator { get; private set; }
        [Import] protected Lazy<ILoggerFacade> Logger { get; private set; }
        [Import] protected Lazy<Notification> Notification { get; private set; }
        protected NotificationEvent NotificationEvent {
            get { return EventAggregator.Value.GetEvent<NotificationEvent>(); }
        }

        protected AlterBaseEntityViewModel(IBaseEntityFacade<TEntity> customBaseEntityFacade = null, IRepository customRepository = null) {
            if(customBaseEntityFacade != null) EntityFacade = new Lazy<IBaseEntityFacade<TEntity>>(() => customBaseEntityFacade);
            if(customRepository != null) Repository = new Lazy<IRepository>(() => customRepository);
            SaveChangesCommand = new RelayDelegateCommand(Id, SaveChangesExecute, CanSaveChanges, sharedCanExecute: true);
            DiscardChangesCommand = new DelegateCommand(DiscardChangesExecute, CanDiscardChanges);
            QuickSearchCommand = new DelegateCommand(QuickSearchExecute, CanQuickSearch);
            ClearEntityCommand = new DelegateCommand(ClearEntityExecute, CanClearEntity);
        }

        public override void InitializeServices() {
            if(Entity == null) ClearEntityExecute(null);
            EventAggregator.Value.GetEvent<EntityIncludeEvent<TEntity>>().Subscribe(IncludeEventExecute);
            Info.SubState(ViewSubState.Unlocked);
        }

        protected virtual void IncludeEventExecute(EntityIncludePayload<TEntity> entityIncludePayload) {
            if(entityIncludePayload.ViewId != Id) return;
            var entity = entityIncludePayload.Entity;
            if(entity == null) return;
            Entity = entity;
            Info.State(ViewState.Update);
        }

        protected virtual bool CanDiscardChanges(object arg) {
            if(Info.State == ViewState.Add & Info.SubState == ViewSubState.Unlocked) return true;
            if(Info.State == ViewState.Update & Info.SubState == ViewSubState.Unlocked) return true;
            return false;
        }
        protected virtual void DiscardChangesExecute(object arg) { EventAggregator.Value.GetEvent<CloseViewEvent>().Publish(Id); }

        protected virtual bool CanSaveChanges(object arg) {
            if(EntityFacade != null) {
                if(Info.State == ViewState.Add & Info.SubState == ViewSubState.Unlocked) return EntityFacade.Value.CanAdd().Item1;
                if(Info.State == ViewState.Update & Info.SubState == ViewSubState.Unlocked) return EntityFacade.Value.CanUpdate().Item1;
                return false;
            }
            return !ReferenceEquals(Entity, null);
        }
        private void SaveChangesExecute(object arg) {
            Worker.DoWork += SaveChangesExecute;
            Worker.WorkerSupportsCancellation = true;
            Worker.RunWorkerAsync();
        }
        protected virtual void SaveChangesExecute(object sender, DoWorkEventArgs e) {
            Info.SubState(ViewSubState.Locked);
            NotificationEvent.Publish(
                Notification.Value.Message(Strings.Notification_Field_Adding).Detail("").Progress(-2).State(NotificationState.Info));
            Repository.Value.Uow.OnError += (o, s) => {
                                                NotificationEvent.Publish(
                                                    Notification.Value.Message(s.Description)
                                                                .Detail(s.ErrorMessage)
                                                                .Progress(-1)
                                                                .State(NotificationState.Error));
                                                Worker.CancelAsync();
                                            };
            using(Repository.Value.Uow.BeginTransaction())
                if(!Worker.CancellationPending) {
                    NotificationEvent.Publish(Notification.Value.Progress(50));
                    Entity = Repository.Value.SaveOrUpdate(Entity);
                    NotificationEvent.Publish(Notification.Value.Progress(70));
                    Repository.Value.Uow.CommitTransaction();
                    NotificationEvent.Publish(Notification.Value.Message(Strings.Notification_Field_Added).Progress(100).State(NotificationState.Ok));
                    Info.State(ViewState.Update);
                }
            Info.SubState(ViewSubState.Unlocked);
        }

        protected virtual bool CanQuickSearch(object obj) {
            if(Info == null || Info.SubState == ViewSubState.Locked) return false;
            return Info.State != ViewState.QuickSearch;
        }
        protected virtual void QuickSearchExecute(object arg) {
            Info.State(ViewState.QuickSearch);
            var openPayload = new OpenViewPayload(ViewInfoExtension.New(ViewType.Address, new[] {ViewState.QuickSearch}));
            EventAggregator.Value.GetEvent<OpenViewEvent>().Publish(openPayload);
            _currentSubscription =
                EventAggregator.Value.GetEvent<CloseViewEvent>().Subscribe(delegate(Guid payloadId) { if(Id == payloadId) RestoreUIState(); });
        }

        private void RestoreUIState() {
            if(Info.State == ViewState.QuickSearch) {
                Info.State(_previousState);
                _currentSubscription.Dispose();
            }
        }

        protected virtual bool CanClearEntity(object obj) { return Info.State == ViewState.Add && Info.SubState == ViewSubState.Unlocked; }
        protected virtual void ClearEntityExecute(object arg) { Entity = EntityFacade.Value.GenerateEntity(); }

        protected virtual void EntityChanged() {
            EntityFacade.Value.Entity = Entity;
            EventAggregator.Value.GetEvent<EntityChangedEvent<TEntity>>().Publish(new EntityChangedPayload<TEntity>(Id, Entity));
        }

        public override void Refresh() { ClearEntityExecute(null); }

        public override ViewModelInfo Info {
            get { return _viewModelInfo ?? (_viewModelInfo = new ViewModelInfo {IsChild = true, State = ViewState.Add, SubState = ViewSubState.Locked}); }
            set {
                _viewModelInfo = value;
                UIOpChanged(null, new PropertyChangedEventArgs("ViewState"));
                value.PropertyChanged += UIOpChanged;
            }
        }
        private void UIOpChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            if(propertyChangedEventArgs.PropertyName == "ViewState") {
                if(Info.State == ViewState.QuickSearch) return;
                _previousState = Info.State;
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
            Repository.Value.Uow.Dispose();
        }

        #endregion
    }
}
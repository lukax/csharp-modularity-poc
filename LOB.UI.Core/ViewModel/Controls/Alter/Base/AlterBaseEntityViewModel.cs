#region Usings

using System.Diagnostics;
using System.Windows.Input;
using LOB.Dao.Interface;
using LOB.Domain.Base;
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
    public abstract class AlterBaseEntityViewModel<T> : BaseViewModel, IAlterBaseEntityViewModel
        where T : BaseEntity {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _loggerFacade;
        private UIOperation _previousOperation;
        private SubscriptionToken _currentSubscription;
        private UIOperation _operation;
        public T Entity { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand DiscardChangesCommand { get; set; }
        public ICommand ClearEntityCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand QuickSearchCommand { get; set; }
        public int Index { get; set; }
        protected IRepository Repository { get; set; }

        [InjectionConstructor]
        protected AlterBaseEntityViewModel(T entity, IRepository repository,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade) {
            _eventAggregator = eventAggregator;
            _loggerFacade = loggerFacade;
            Repository = repository;
            Entity = entity;
            SaveChangesCommand = new DelegateCommand(SaveChanges, CanSaveChanges);
            DiscardChangesCommand = new DelegateCommand(Cancel, CanCancel);
            QuickSearchCommand = new DelegateCommand(QuickSearch);
            ClearEntityCommand = new DelegateCommand(ClearEntity);
        }

        protected virtual bool CanSaveChanges(object arg) { return Entity != null; }

        protected virtual bool CanCancel(object arg) {
            if(Operation.State == UIOperationState.Add) return true;
            if(Operation.State == UIOperationState.Update) return true;
            return false;
        }

        protected virtual void SaveChanges(object arg) {
            using(Repository.Uow.BeginTransaction()) {
                Debug.Write("Saving changes...");
                Entity = Repository.SaveOrUpdate(Entity);
            }
            Cancel(arg);
        }

        protected abstract void Cancel(object arg);

        protected virtual void QuickSearch(object arg) {
            Operation = new UIOperation {
                Type = Operation.Type,
                State = UIOperationState.QuickSearch
            };
            _eventAggregator.GetEvent<OpenViewEvent>().Publish(Operation);
            _currentSubscription =
                _eventAggregator.GetEvent<CloseViewEvent>().Subscribe(ChangeUIState);
        }

        private void ChangeUIState(UIOperation obj) {
            if(Operation.State == UIOperationState.QuickSearch) {
                Operation = _previousOperation;
                _currentSubscription.Dispose();
            }
        }

        protected abstract void ClearEntity(object arg);

        public override UIOperation Operation {
            get { return _operation; }
            set {
                _previousOperation = value;
                _operation = value;
            }
        }
    }
}
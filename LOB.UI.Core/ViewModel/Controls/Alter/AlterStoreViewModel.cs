#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Logic;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using Category = LOB.Domain.SubEntity.Category;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterStoreViewModel : AlterBaseEntityViewModel<Store>,
                                                IAlterProductViewModel {

        private readonly IStoreFacade _storeFacade;
        private readonly IEventAggregator _eventAggregator;
        public ICommand AlterCategoryCommand { get; set; }
        public ICommand ListCategoryCommand { get; set; }
        public IList<Category> Categories { get; set; }
        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Store,
            State = UIOperationState.Add
        };
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        [InjectionConstructor]
        public AlterStoreViewModel(Store entity, IRepository repository, IStoreFacade storeFacade,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _storeFacade = storeFacade;
            _eventAggregator = eventAggregator;
            AlterCategoryCommand = new DelegateCommand(ExecuteAlterCategory);
            ListCategoryCommand = new DelegateCommand(ExecuteListCategory);
        }

        public override void InitializeServices() {
            Operation = _operation;
            ClearEntity(null);

            _worker.DoWork += UpdateCategoryList;
            _worker.WorkerSupportsCancellation = true;
            _worker.WorkerReportsProgress = true;
            _worker.RunWorkerAsync();
        }

        public override void Refresh() { ClearEntity(null); }

        private async void UpdateCategoryList(object sender, DoWorkEventArgs doWorkEventArgs) {
            do {
                await Task.Delay(2000); // TODO: Configuration based update time
                Categories = Repository.GetList<Category>().ToList();
            } while(!_worker.CancellationPending);
        }

        private void ExecuteListCategory(object o) {
            //UIOperationType oP = o.ToString().ToUIOperationType();
            //_navigator.ResolveView(oP).Show(true);
        }

        private void ExecuteAlterCategory(object o) {
            //UIOperationType oP = o.ToString().ToUIOperationType();
            //if(Entity.Category != null)
            //    _navigator.ResolveView(oP)
            //              .SetViewModel(
            //                            _unityContainer.Resolve<AlterCategoryViewModel>(new ParameterOverride(
            //                                                                                "category", Entity.Category)))
            //              .Show(true);
            //_navigator.ResolveView(oP).Show(true);
        }

        protected override void SaveChanges(object arg) {
            using(Repository.Uow.BeginTransaction()) {
                Entity = Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override bool CanSaveChanges(object arg) {
            //TODO: If viewState == Add : ..., If viewState == Update : ....
            IEnumerable<ValidationResult> results;
            return _storeFacade.CanAdd(out results);
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void ClearEntity(object args) {
            Entity = _storeFacade.GenerateEntity();
            _storeFacade.SetEntity(Entity);
            _storeFacade.ConfigureValidations();
        }

    }
}
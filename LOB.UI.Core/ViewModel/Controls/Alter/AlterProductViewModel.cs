#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Dao.Interface;
using LOB.Domain;
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
    public sealed class AlterProductViewModel : AlterBaseEntityViewModel<Product>, IAlterProductViewModel {

        private readonly IUnityContainer _unityContainer;
        private readonly IFluentNavigator _navigator;
        private readonly IEventAggregator _eventAggregator;
        public ICommand AlterCategoryCommand { get; set; }
        public ICommand ListCategoryCommand { get; set; }
        public IList<Category> Categories { get; set; }
        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Service,
            State = UIOperationState.Add
        };

        [InjectionConstructor]
        public AlterProductViewModel(Product entity, IUnityContainer unityContainer, IFluentNavigator fluentNavigator,
            IRepository repository, IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _unityContainer = unityContainer;
            _navigator = fluentNavigator;
            _eventAggregator = eventAggregator;
            AlterCategoryCommand = new DelegateCommand(ExecuteAlterCategory);
            ListCategoryCommand = new DelegateCommand(ExecuteListCategory);
            UpdateCategoryList();
        }

        public override void InitializeServices() {
            Operation = _operation;
            ClearEntity(null);
        }

        public override void Refresh() { ClearEntity(null); }

        private async void UpdateCategoryList(int delay = 2000) {
            while(true) {
                await Task.Delay(delay);
                Categories = Repository.GetList<Category>().ToList();
            }
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
            //TODO: Business logic
            return true;
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void ClearEntity(object args) { Entity = new Product {}; }

    }
}
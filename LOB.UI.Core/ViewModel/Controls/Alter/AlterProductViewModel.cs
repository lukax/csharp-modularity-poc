#region Usings
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterProductViewModel : AlterBaseEntityViewModel<Product>, IAlterProductViewModel {

        private readonly IUnityContainer _container;
        private readonly IFluentNavigator _navigator;

        [InjectionConstructor] public AlterProductViewModel(Product entity, IRepository repository,
            IUnityContainer container, IFluentNavigator navigator)
            : base(entity, repository) {
            this._container = container;
            this._navigator = navigator;
            this.AlterCategoryCommand = new DelegateCommand(this.ExecuteAlterCategory);
            this.ListCategoryCommand = new DelegateCommand(this.ExecuteListCategory);
            this.UpdateCategoryList();
        }

        public ICommand AlterCategoryCommand { get; set; }
        public ICommand ListCategoryCommand { get; set; }

        public IList<Category> Categories { get; set; }

        public override void InitializeServices() {}

        public override void Refresh() {
            this.Entity = new Product();
        }

        public override OperationType OperationType {
            get { return OperationType.AlterProduct; }
        }

        private async void UpdateCategoryList(int delay = 2000) {
            while(true) {
                await Task.Delay(2000);
                this.Categories = this.Repository.GetList<Category>().ToList();
            }
        }

        private void ExecuteListCategory(object o) {
            OperationType oP = o.ToString().ToOperationType();
            this._navigator.ResolveView(oP).Show(true);
        }

        private void ExecuteAlterCategory(object o) {
            OperationType oP = o.ToString().ToOperationType();
            if(this.Entity.Category != null)
                this._navigator.ResolveView(oP)
                    .SetViewModel(
                                  this._container.Resolve<AlterCategoryViewModel>(new ParameterOverride("category",
                                                                                                        this.Entity
                                                                                                            .Category)))
                    .Show(true);
            this._navigator.ResolveView(oP).Show(true);
        }

        protected override void SaveChanges(object arg) {
            using(this.Repository.Uow) {
                this.Repository.Uow.BeginTransaction();
                this.Entity = this.Repository.SaveOrUpdate(this.Entity);
                this.Repository.Uow.CommitTransaction();
            }

            //Messenger.Default.Send("SaveChangesCommand");
        }

        protected override bool CanSaveChanges(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void QuickSearch(object arg) {
            //commandService.Execute("QuickSearch", OperationName.ListProduct);
        }

        protected override void ClearEntity(object args) {
            this.Entity = new Product();
        }

    }
}
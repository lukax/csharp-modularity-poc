#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using LOB.Business.Interface.Logic;
using LOB.Domain;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterProductViewModel))]
    public sealed class AlterProductViewModel : AlterBaseEntityViewModel<Product>, IAlterProductViewModel {
        public ICommand AlterCategoryCommand { get; set; }
        public ICommand ListCategoryCommand { get; set; }
        public IList<Category> Categories { get; set; }
        private readonly ViewModelInfo _defaultViewModelInfo = new ViewModelInfo {ViewSubState = ViewSubState.Locked, ViewState = ViewState.Add};

        [ImportingConstructor]
        public AlterProductViewModel(IProductFacade productFacade)
            : base(productFacade) {
            AlterCategoryCommand = new DelegateCommand(ExecuteAlterCategory);
            ListCategoryCommand = new DelegateCommand(ExecuteListCategory);
        }

        public override void InitializeServices() {
            if(Equals(Info, default(ViewModelInfo))) Info = _defaultViewModelInfo;
            base.InitializeServices();
            Worker.DoWork += UpdateCategoryList;
            Worker.RunWorkerAsync();
        }

        private void UpdateCategoryList(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            RepositoryLazy.Value.Uow.OnError += (o, s) => {
                                                    NotificationEvent.Publish(
                                                        NotificationLazy.Value.Message(s.Description)
                                                                    .Detail(s.ErrorMessage)
                                                                    .Progress(-1)
                                                                    .State(NotificationState.Error));
                                                    //Worker.CancelAsync();
                                                    Info.SubState(ViewSubState.Locked);
                                                };

            do {
                if(RepositoryLazy.Value.Uow.TestConnection()) {
                    Categories = RepositoryLazy.Value.GetAll<Category>().ToList();
                    Info.SubState(ViewSubState.Unlocked);
                }
                Thread.Sleep(2000); // TODO: Configuration based update time
            } while(!worker.CancellationPending);
        }

        private void ExecuteListCategory(object o) {
            //var op = new ViewModelInfo().State(ViewState.QuickSearch).SubState(ViewSubState.Locked);
            //EventAggregatorLazy.GetEvent<OpenViewEvent>().Publish(op);
        }

        private void ExecuteAlterCategory(object o) {
            //var op =
            //    new ViewModelInfo().Info(string.IsNullOrWhiteSpace(Entity.Category.Name) ? ViewState.Add : ViewState.Update);
            //op.ViewModel = this;
            //LazyEventAggregator.GetEvent<OpenViewEvent>().Publish(op);
        }

        public override void Dispose() {
            Worker.Dispose();
            base.Dispose();
        }
    }
}
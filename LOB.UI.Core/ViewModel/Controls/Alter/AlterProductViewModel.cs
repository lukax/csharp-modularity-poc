#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows.Input;
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

        [ImportingConstructor]
        public AlterProductViewModel() {
            AlterCategoryCommand = new DelegateCommand(ExecuteAlterCategory);
            ListCategoryCommand = new DelegateCommand(ExecuteListCategory);
        }

        public override void InitializeServices() {
            base.InitializeServices();
            Worker.DoWork += UpdateCategoryList;
            Worker.RunWorkerAsync();
        }

        private void UpdateCategoryList(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            Repository.Value.Uow.OnError += (o, s) => {
                                                NotificationEvent.Publish(
                                                    Notification.Value.Message(s.Description)
                                                                .Detail(s.ErrorMessage)
                                                                .Progress(-1)
                                                                .State(NotificationState.Error));
                                                //Worker.CancelAsync();
                                                Info.SubState(ViewSubState.Locked);
                                            };

            do {
                if(Repository.Value.Uow.TestConnection()) {
                    Categories = Repository.Value.GetAll<Category>().ToList();
                    Info.SubState(ViewSubState.Unlocked);
                }
                Thread.Sleep(2000); // TODO: Configuration based update time
            } while(!worker.CancellationPending);
        }

        private void ExecuteListCategory(object o) {
            //var op = new ViewModelInfo().State(ViewState.QuickSearchExecute).SubState(ViewSubState.Locked);
            //EventAggregator.GetEvent<OpenViewEvent>().Publish(op);
        }

        private void ExecuteAlterCategory(object o) {
            //var op =
            //    new ViewModelInfo().Info(string.IsNullOrWhiteSpace(Entity.Category.Name) ? ViewState.Add : ViewState.UpdateExecute);
            //op.ViewModel = this;
            //LazyEventAggregator.GetEvent<OpenViewEvent>().Publish(op);
        }
    }
}
#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.Command;
using LOB.UI.Contract.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterProductViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class AlterProductViewModel : AlterBaseEntityViewModel<Product>, IAlterProductViewModel {
        public ICommand AlterCategoryCommand { get; set; }
        public ICommand ListCategoryCommand { get; set; }
        public IList<Category> Categories { get; set; }

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

            do {
                Categories = Repository.Value.GetAll<Category>().ToList();
                Unlock();
                Thread.Sleep(2000); // TODO: Configuration based update time
            } while(!worker.CancellationPending);
        }

        private void ExecuteListCategory(object o) {
            //var op = new ViewModelInfo().ViewState(ViewState.QuickSearchExecute).SubState(ViewSubState.Locked);
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
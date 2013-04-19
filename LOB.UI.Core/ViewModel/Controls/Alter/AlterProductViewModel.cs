#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
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
using Category = LOB.Domain.SubEntity.Category;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterProductViewModel))]
    public sealed class AlterProductViewModel : AlterBaseEntityViewModel<Product>, IAlterProductViewModel {
        public ICommand AlterCategoryCommand { get; set; }
        public ICommand ListCategoryCommand { get; set; }
        public IList<Category> Categories { get; set; }
        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Service, State = ViewState.Add};

        [ImportingConstructor]
        public AlterProductViewModel(IProductFacade productFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(productFacade, repository, eventAggregator, logger) {
            AlterCategoryCommand = new DelegateCommand(ExecuteAlterCategory);
            ListCategoryCommand = new DelegateCommand(ExecuteListCategory);
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
            Worker.DoWork += UpdateCategoryList;
            Worker.RunWorkerAsync();
        }

        private void UpdateCategoryList(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            Repository.Uow.OnError += (o, s) => {
                                          NotificationEvent.Publish(
                                              Notification.Message(s.Description).Detail(s.ErrorMessage).Progress(-1).State(NotificationState.Error));
                                          //Worker.CancelAsync();
                                          ViewID.SubState(ViewSubState.Locked);
                                      };

            do {
                if(Repository.Uow.TestConnection()) {
                    Categories = Repository.GetAll<Category>().ToList();
                    ViewID.SubState(ViewSubState.Unlocked);
                }
                Thread.Sleep(2000); // TODO: Configuration based update time
            } while(!worker.CancellationPending);
        }

        private void ExecuteListCategory(object o) {
            var op = new ViewID().State(ViewState.QuickSearch).Type(ViewType.Category);
            EventAggregator.GetEvent<OpenViewEvent>().Publish(op);
        }

        private void ExecuteAlterCategory(object o) {
            var op = new ViewID().Type(ViewType.Category).State(string.IsNullOrWhiteSpace(Entity.Category.Name) ? ViewState.Add : ViewState.Update);
            op.ViewModel = this;
            EventAggregator.GetEvent<OpenViewEvent>().Publish(op);
        }

        public override void Dispose() {
            Worker.Dispose();
            base.Dispose();
        }
    }
}
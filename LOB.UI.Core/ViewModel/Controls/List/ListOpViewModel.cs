#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    public class ListOpViewModel : BaseViewModel, IListOpViewModel {

        private readonly IEventAggregator _eventAggregator;
        private readonly Lazy<IDictionary<string, UIOperation>> _operationDictLazy;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private string _search;
        public override UIOperation Operation { get; set; }
        public string Entity { get; set; }
        public ObservableCollection<PanoramaGroup> Entitys { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        public ListOpViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            _operationDictLazy = new Lazy<IDictionary<string, UIOperation>>(CreateList);
            //Search = "";
            Entity = "";
        }

        [AllowNull]
        public string Search {
            get { return _search ?? ""; }
            set {
                _search = value.ToLower();
                if(!_worker.IsBusy) _worker.RunWorkerAsync();
            }
        }

        public override void InitializeServices() {
            Operation = _operation;
            _worker.DoWork += UpdateList;
            _worker.WorkerSupportsCancellation = true;
            _worker.RunWorkerAsync();
        }

        public override void Refresh() { }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Op, State = UIOperationState.List};

        private void UpdateList(object sender, DoWorkEventArgs doWorkEventArgs) {
            //var worker = sender as BackgroundWorker;
            //if(worker == null) return;
            //worker.WorkerSupportsCancellation = true;

            //Thread.Sleep(1000);
            if(string.IsNullOrEmpty(Search)) {
                var alterGroup = new PanoramaGroup(Strings.Header_Alter);
                alterGroup.SetSource(_operationDictLazy.Value.Keys.Where(x => _operationDictLazy.Value[x].ToString().Contains("Add")).ToList());
                var listGroup = new PanoramaGroup(Strings.Header_List);
                listGroup.SetSource(_operationDictLazy.Value.Keys.Where(x => _operationDictLazy.Value[x].ToString().Contains("List")).ToList());
                var sellGroup = new PanoramaGroup(Strings.Header_Sell);
                sellGroup.SetSource(_operationDictLazy.Value.Keys.Where(x => _operationDictLazy.Value[x].ToString().Contains("Sell")).ToList());
                Entitys = new ObservableCollection<PanoramaGroup> {alterGroup, listGroup, sellGroup};
            }
            else {
                var alterGroup = new PanoramaGroup(Strings.Header_Alter);
                alterGroup.SetSource(
                    _operationDictLazy.Value.Keys.Where(x => _operationDictLazy.Value[x].ToString().Contains("Add"))
                                      .Where(x => x.ToLower().Contains(Search))
                                      .ToList());
                var listGroup = new PanoramaGroup(Strings.Header_List);
                listGroup.SetSource(
                    _operationDictLazy.Value.Keys.Where(x => _operationDictLazy.Value[x].ToString().Contains("List"))
                                      .Where(x => x.ToLower().Contains(Search))
                                      .ToList());
                var sellGroup = new PanoramaGroup(Strings.Header_Sell);
                sellGroup.SetSource(
                    _operationDictLazy.Value.Keys.Where(x => _operationDictLazy.Value[x].ToString().Contains("Sell"))
                                      .Where(x => x.ToLower().Contains(Search))
                                      .ToList());
                Entitys = new ObservableCollection<PanoramaGroup> {alterGroup, listGroup, sellGroup};
            }
        }

        private void SaveChanges(object arg) {
            var parsedUIOperation = _operationDictLazy.Value[arg.ToString()];
            var not = new Notification {
                Message =
                    string.Format("{0} {1}", Strings.Common_Initializing,
                                  _operationDictLazy.Value.FirstOrDefault(x => x.Value.Equals(parsedUIOperation)).Key),
                Progress = -2,
                Severity = Severity.Info
            };
            _eventAggregator.GetEvent<NotificationEvent>().Publish(not);
            _eventAggregator.GetEvent<OpenViewEvent>().Publish(parsedUIOperation);
            var stringy = string.Format("{0} {1}", Strings.Common_Initialized,
                                        _operationDictLazy.Value.FirstOrDefault(x => x.Value.Equals(parsedUIOperation)).Key);
            _eventAggregator.GetEvent<NotificationEvent>().Publish(not.Message(stringy).Progress(-1).Severity(Severity.Ok));
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(Operation);
        }

        private IDictionary<string, UIOperation> CreateList() {
            var catalog = UIOperationCatalog.UIOperations;
            //Remove Internal usage Types from user selection:
            catalog.Remove(catalog.FirstOrDefault(x => x.Type == UIOperationType.Unknown));
            catalog.Remove(catalog.FirstOrDefault(x => x.Type == UIOperationType.BaseEntity));
            catalog.Remove(catalog.FirstOrDefault(x => x.Type == UIOperationType.Service));
            catalog.Remove(catalog.FirstOrDefault(x => x.Type == UIOperationType.Person));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.Internal));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.Update));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.Delete));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.QuickSearch));
            var operationTypes = new Dictionary<string, UIOperation>(catalog.Count);
            var stringsType = typeof(Strings);
            var stringsTypeProps = stringsType.GetProperties();

            //Parse to localized string
            foreach(var uiOperation in catalog) {
                UIOperation operation = uiOperation;
                foreach(string name in
                    from propertyInfo in stringsTypeProps
                    let name = propertyInfo.Name
                    where propertyInfo.Name.Contains("Command_" + operation)
                    select name) operationTypes.Add(stringsType.GetProperty(name).GetValue(stringsType).ToString(), uiOperation);
            }
            return operationTypes;
        }
        #region Implementation of IDisposable

        ~ListOpViewModel() { Dispose(false); }
        public override void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing) {
            if(_worker.WorkerSupportsCancellation) _worker.CancelAsync();
            if(disposing) _worker.Dispose();
        }

        #endregion
    }
}
#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    [Export(typeof(IListOpViewModel))]
    public class ListOpViewModel : BaseViewModel, IListOpViewModel {
        private readonly IEventAggregator _eventAggregator;
        private readonly Lazy<IDictionary<string, ViewID>> _defaultViewIDDictLazy;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private string _search;
        public override ViewID ViewID { get; set; }
        public string Entity { get; set; }
        //[AllowNull]
        public ObservableCollection<PanoramaGroup> Entitys { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public string Search {
            get { return _search ?? ""; }
            set {
                _search = value.ToLower();
                if(!_worker.IsBusy) _worker.RunWorkerAsync();
            }
        }

        [ImportingConstructor]
        public ListOpViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            _defaultViewIDDictLazy = new Lazy<IDictionary<string, ViewID>>(CreateList);
            Entity = "";
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            _worker.DoWork += UpdateList;
            _worker.RunWorkerAsync();
        }
        public override void Refresh() { Search = ""; }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Op, State = ViewState.List};

        private void UpdateList(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;

            //Thread.Sleep(1000);
            if(string.IsNullOrEmpty(Search)) {
                var alterGroup = new PanoramaGroup(Strings.UI_Header_Alter);
                alterGroup.SetSource(_defaultViewIDDictLazy.Value.Keys.Where(x => _defaultViewIDDictLazy.Value[x].ToString().Contains("Add")).ToList());
                var listGroup = new PanoramaGroup(Strings.UI_Header_List);
                listGroup.SetSource(_defaultViewIDDictLazy.Value.Keys.Where(x => _defaultViewIDDictLazy.Value[x].ToString().Contains("List")).ToList());
                var sellGroup = new PanoramaGroup(Strings.UI_Header_Sell);
                sellGroup.SetSource(_defaultViewIDDictLazy.Value.Keys.Where(x => _defaultViewIDDictLazy.Value[x].ToString().Contains("Sell")).ToList());
                Entitys = new ObservableCollection<PanoramaGroup> {alterGroup, listGroup, sellGroup};
            }
            else {
                var alterGroup = new PanoramaGroup(Strings.UI_Header_Alter);
                alterGroup.SetSource(
                    _defaultViewIDDictLazy.Value.Keys.Where(x => _defaultViewIDDictLazy.Value[x].ToString().Contains("Add"))
                                          .Where(x => x.ToLower().Contains(Search))
                                          .ToList());
                var listGroup = new PanoramaGroup(Strings.UI_Header_List);
                listGroup.SetSource(
                    _defaultViewIDDictLazy.Value.Keys.Where(x => _defaultViewIDDictLazy.Value[x].ToString().Contains("List"))
                                          .Where(x => x.ToLower().Contains(Search))
                                          .ToList());
                var sellGroup = new PanoramaGroup(Strings.UI_Header_Sell);
                sellGroup.SetSource(
                    _defaultViewIDDictLazy.Value.Keys.Where(x => _defaultViewIDDictLazy.Value[x].ToString().Contains("Sell"))
                                          .Where(x => x.ToLower().Contains(Search))
                                          .ToList());
                Entitys = new ObservableCollection<PanoramaGroup> {alterGroup, listGroup, sellGroup};
            }
        }

        private void SaveChanges(object arg) {
            var parsedUIOperation = _defaultViewIDDictLazy.Value[arg.ToString()];
            var not = new Notification {
                Message =
                    string.Format("{0} {1}", Strings.Common_Initializing,
                                  _defaultViewIDDictLazy.Value.FirstOrDefault(x => x.Value.Equals(parsedUIOperation)).Key),
                Progress = -2,
                State = NotificationState.Info
            };
            _eventAggregator.GetEvent<NotificationEvent>().Publish(not);
            _eventAggregator.GetEvent<OpenViewEvent>().Publish(parsedUIOperation);
            var stringy = string.Format("{0} {1}", Strings.Common_Initialized,
                                        _defaultViewIDDictLazy.Value.FirstOrDefault(x => x.Value.Equals(parsedUIOperation)).Key);
            _eventAggregator.GetEvent<NotificationEvent>().Publish(not.Message(stringy).Progress(-1).State(NotificationState.Ok));
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID);
        }

        private IDictionary<string, ViewID> CreateList() {
            //var catalog = UIOperationCatalog.UIOperations;
            ////Remove Internal usage Types from user selection:
            //catalog.Remove(catalog.FirstOrDefault(x => x.Type == ViewType.Unknown));
            //catalog.Remove(catalog.FirstOrDefault(x => x.Type == ViewType.BaseEntity));
            //catalog.Remove(catalog.FirstOrDefault(x => x.Type == ViewType.Service));
            //catalog.Remove(catalog.FirstOrDefault(x => x.Type == ViewType.Person));
            //catalog.Remove(catalog.FirstOrDefault(x => x.State == ViewState.Internal));
            //catalog.Remove(catalog.FirstOrDefault(x => x.State == ViewState.Update));
            //catalog.Remove(catalog.FirstOrDefault(x => x.State == ViewState.Delete));
            //catalog.Remove(catalog.FirstOrDefault(x => x.State == ViewState.QuickSearch));
            //var operationTypes = new Dictionary<string, ViewID>(catalog.Count);
            //var stringsType = typeof(Strings);
            //var stringsTypeProps = stringsType.GetProperties();

            ////Parse to localized string
            //foreach(var uiOperation in catalog) {
            //    ViewID operation = uiOperation;
            //    foreach(string name in
            //        from propertyInfo in stringsTypeProps
            //        let name = propertyInfo.Name
            //        where propertyInfo.Name.Contains("Command_" + operation)
            //        select name) operationTypes.Add(stringsType.GetProperty(name).GetValue(stringsType).ToString(), uiOperation);
            //}
            //return operationTypes;
            return null;
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
#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.UI.Contract;
using LOB.UI.Contract.Command;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.List;
using LOB.UI.Core.Event;
using LOB.UI.Core.Event.View;
using LOB.UI.Core.ViewModel.Base;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    [Export(typeof(IListOpViewModel))]
    public sealed class ListOpViewModel : BaseViewModel, IListOpViewModel {
        private Lazy<IDictionary<string, IViewInfo>> _defaultViewInfoDictLazy;
        private string _search;
        public string Entity { get; set; }
        public ObservableCollection<PanoramaGroup> Entitys { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public string Search {
            get { return _search ?? ""; }
            set {
                _search = value.ToLower();
                if(!Worker.IsBusy) Worker.RunWorkerAsync();
            }
        }
        [ImportMany] public Lazy<IBaseView<IBaseViewModel>, IViewInfo>[] LazyViewInfos { get; set; }
        [Import] public Lazy<IEventAggregator> LazyEventAggregator { get; set; }
        [Import] public AggregateCatalog Type { get; set; }

        public ListOpViewModel() {
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            Entity = "";
            IsChild = false;
            ChangeState(ViewState.Other);
            Unlock();
        }

        public override void InitializeServices() {
            _defaultViewInfoDictLazy = new Lazy<IDictionary<string, IViewInfo>>(CreateList);
            Worker.DoWork += UpdateList;
            Worker.RunWorkerAsync();
        }
        public override void Refresh() { Search = ""; }

        private void UpdateList(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;

            //Thread.Sleep(1000);
            if(string.IsNullOrEmpty(Search)) {
                var alterGroup = new PanoramaGroup(Strings.UI_Header_Alter);
                alterGroup.SetSource(_defaultViewInfoDictLazy.Value.Where(x => x.Value.ViewStates.Contains(ViewState.Add)).Select(x => x.Key));
                var listGroup = new PanoramaGroup(Strings.UI_Header_List);
                listGroup.SetSource(_defaultViewInfoDictLazy.Value.Where(x => x.Value.ViewStates.Contains(ViewState.List)).Select(x => x.Key));
                var sellGroup = new PanoramaGroup(Strings.UI_Header_Sell);
                sellGroup.SetSource(_defaultViewInfoDictLazy.Value.Where(x => x.Value.ViewStates.Contains(ViewState.Sell)).Select(x => x.Key));
                Entitys = new ObservableCollection<PanoramaGroup> {alterGroup, listGroup, sellGroup};
            }
            else {
                var alterGroup = new PanoramaGroup(Strings.UI_Header_Alter);
                alterGroup.SetSource(
                    _defaultViewInfoDictLazy.Value.Keys.Where(x => _defaultViewInfoDictLazy.Value[x].ToString().Contains("Add"))
                                            .Where(x => x.ToLower().Contains(Search))
                                            .ToList());
                var listGroup = new PanoramaGroup(Strings.UI_Header_List);
                listGroup.SetSource(
                    _defaultViewInfoDictLazy.Value.Keys.Where(x => _defaultViewInfoDictLazy.Value[x].ToString().Contains("List"))
                                            .Where(x => x.ToLower().Contains(Search))
                                            .ToList());
                var sellGroup = new PanoramaGroup(Strings.UI_Header_Sell);
                sellGroup.SetSource(
                    _defaultViewInfoDictLazy.Value.Keys.Where(x => _defaultViewInfoDictLazy.Value[x].ToString().Contains("Sell"))
                                            .Where(x => x.ToLower().Contains(Search))
                                            .ToList());
                Entitys = new ObservableCollection<PanoramaGroup> {alterGroup, listGroup, sellGroup};
            }
        }

        private void SaveChanges(object arg) {
            var viewInfo = _defaultViewInfoDictLazy.Value[arg.ToString()];
            var notification = new Notification {
                Message =
                    string.Format("{0} {1}", Strings.Common_Initializing,
                                  _defaultViewInfoDictLazy.Value.FirstOrDefault(x => x.Value.Equals(viewInfo)).Key),
                Progress = -2,
                Type = NotificationType.Info
            };
            LazyEventAggregator.Value.GetEvent<NotificationEvent>().Publish(notification);
            LazyEventAggregator.Value.GetEvent<OpenViewEvent>().Publish(new OpenViewPayload(viewInfo));
            var stringy = string.Format("{0} {1}", Strings.Common_Initialized,
                                        _defaultViewInfoDictLazy.Value.FirstOrDefault(x => x.Value.Equals(viewInfo)).Key);
            LazyEventAggregator.Value.GetEvent<NotificationEvent>().Publish(notification.Message(stringy).Progress(-1).State(NotificationType.Ok));
            LazyEventAggregator.Value.GetEvent<CloseViewEvent>().Publish(Id);
        }

        private IDictionary<string, IViewInfo> CreateList() {
            var catalog =
                LazyViewInfos.Where(x => !x.Metadata.ViewStates.Contains(ViewState.Other))
                             .ToDictionary(item => ViewInfoExtension.ToString(item.Metadata), item => item.Metadata);
            //var catalog = UIOperationCatalog.UIOperations;
            ////Remove Other usage Types from user selection:
            //catalog.Remove(catalog.FirstOrDefault(x => x.Type == ViewType.Other));
            //catalog.Remove(catalog.FirstOrDefault(x => x.Type == ViewType.BaseEntity));
            //catalog.Remove(catalog.FirstOrDefault(x => x.Type == ViewType.Service));
            //catalog.Remove(catalog.FirstOrDefault(x => x.Type == ViewType.Person));
            //catalog.Remove(catalog.FirstOrDefault(x => x.ViewState == ViewState.Other));
            //catalog.Remove(catalog.FirstOrDefault(x => x.ViewState == ViewState.UpdateExecute));
            //catalog.Remove(catalog.FirstOrDefault(x => x.ViewState == ViewState.UpdateExecute));
            //catalog.Remove(catalog.FirstOrDefault(x => x.ViewState == ViewState.DeleteExecute));
            //catalog.Remove(catalog.FirstOrDefault(x => x.ViewState == ViewState.QuickSearchExecute));
            //var operationTypes = new Dictionary<string, ViewModelInfo>(catalog.Count);
            //var stringsType = typeof(Strings);
            //var stringsTypeProps = stringsType.GetProperties();

            ////Parse to localized string
            //foreach(var uiOperation in catalog) {
            //    ViewModelInfo operation = uiOperation;
            //    foreach(string name in
            //        from propertyInfo in stringsTypeProps
            //        let name = propertyInfo.Name
            //        where propertyInfo.Name.Contains("Command_" + operation)
            //        select name) operationTypes.Add(stringsType.GetProperty(name).GetValue(stringsType).ToString(), uiOperation);
            //}
            //return operationTypes;
            return catalog;
        }
        #region Implementation of IDisposable

        ~ListOpViewModel() { Dispose(false); }
        public override void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing) {
            if(Worker.WorkerSupportsCancellation) Worker.CancelAsync();
            if(disposing) Worker.Dispose();
        }

        #endregion
    }
}
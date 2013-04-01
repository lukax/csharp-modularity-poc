#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    public class ListOpViewModel : BaseViewModel, IListOpViewModel {

        private readonly IEventAggregator _eventAggregator;
        private readonly Lazy<IDictionary<string, UIOperation>> _operationDictLazy;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private string _search;

        public ListOpViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            _operationDictLazy = new Lazy<IDictionary<string, UIOperation>>(CreateList);
            Search = "";
            Entity = "";
        }

        public string Entity { get; set; }

        public ObservableCollection<PanoramaGroup> Entitys { get; set; }

        public ICommand SaveChangesCommand { get; set; }

        public string Search {
            get { return _search.ToLower(); }
            set {
                _search = value;
                UpdateList();
            }
        }

        public override void InitializeServices() {
            _worker.DoWork += (sender, args) => UpdateList();
            _worker.RunWorkerAsync();
        }

        public override void Refresh() {}

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Op,
            State = UIOperationState.List
        };
        public override UIOperation UIOperation {
            get { return _operation; }
        }

        private void UpdateList() {
            Task.Delay(1000);
            if(string.IsNullOrEmpty(Search)) {
                var alterGroup = new PanoramaGroup(Strings.Header_Alter);
                alterGroup.SetSource(
                                     _operationDictLazy.Value.Keys.Where(
                                                                         x =>
                                                                         _operationDictLazy.Value[x].ToString()
                                                                                                    .Contains("Alter"))
                                                       .ToList());
                var listGroup = new PanoramaGroup(Strings.Header_List);
                listGroup.SetSource(
                                    _operationDictLazy.Value.Keys.Where(
                                                                        x =>
                                                                        _operationDictLazy.Value[x].ToString()
                                                                                                   .Contains("List"))
                                                      .ToList());
                var sellGroup = new PanoramaGroup(Strings.Header_Sell);
                sellGroup.SetSource(
                                    _operationDictLazy.Value.Keys.Where(
                                                                        x =>
                                                                        _operationDictLazy.Value[x].ToString()
                                                                                                   .Contains("Sell"))
                                                      .ToList());
                Entitys = new ObservableCollection<PanoramaGroup> {alterGroup, listGroup, sellGroup};
            }
            else {
                var alterGroup = new PanoramaGroup(Strings.Header_Alter);
                alterGroup.SetSource(
                                     _operationDictLazy.Value.Keys.Where(
                                                                         x =>
                                                                         _operationDictLazy.Value[x].ToString()
                                                                                                    .Contains("Alter"))
                                                       .Where(x => x.ToLower().Contains(Search))
                                                       .ToList());
                var listGroup = new PanoramaGroup(Strings.Header_List);
                listGroup.SetSource(
                                    _operationDictLazy.Value.Keys.Where(
                                                                        x =>
                                                                        _operationDictLazy.Value[x].ToString()
                                                                                                   .Contains("List"))
                                                      .Where(x => x.ToLower().Contains(Search))
                                                      .ToList());
                var sellGroup = new PanoramaGroup(Strings.Header_Sell);
                sellGroup.SetSource(
                                    _operationDictLazy.Value.Keys.Where(
                                                                        x =>
                                                                        _operationDictLazy.Value[x].ToString()
                                                                                                   .Contains("Sell"))
                                                      .Where(x => x.ToLower().Contains(Search))
                                                      .ToList());
                Entitys = new ObservableCollection<PanoramaGroup> {alterGroup, listGroup, sellGroup};
            }
        }

        private void SaveChanges(object arg) {
            var parsedUIOperation = _operationDictLazy.Value[arg.ToString()];
            _eventAggregator.GetEvent<OpenViewEvent>().Publish(parsedUIOperation);
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(UIOperation);
        }

        private IDictionary<string, UIOperation> CreateList() {
            var catalog = UIOperationCatalog.UIOperations;
            //Remove Internal usage Types from user selection:
            catalog.Remove(catalog.FirstOrDefault(x => x.Type == UIOperationType.Unknown));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.Tool));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.Loading));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.Add));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.Update));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.Discard));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.QuickSearch));
            catalog.Remove(catalog.FirstOrDefault(x => x.State == UIOperationState.Exit));

            var operationTypes = new Dictionary<string, UIOperation>(catalog.Count);
            var stringsType = typeof(Strings);
            var stringsTypeProps = stringsType.GetProperties();

            //Parse to localized string
            foreach(var uiOperation in catalog)
                foreach(string name in from propertyInfo in stringsTypeProps
                                       let name = propertyInfo.Name
                                       where propertyInfo.Name.Contains("Command_" + uiOperation.ToString())
                                       select name) operationTypes.Add(stringsType.GetProperty(name).GetValue(stringsType).ToString(), uiOperation);
            return operationTypes;
        }

    }
}
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
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class ListOpViewModel : BaseViewModel, IListOpViewModel {

        private readonly IEventAggregator _eventAggregator;
        private readonly Lazy<IDictionary<string, OperationType>> _operationDictLazy;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private string _search;

        public ListOpViewModel(IEventAggregator eventAggregator) {
            this._eventAggregator = eventAggregator;
            this.SaveChangesCommand = new DelegateCommand(this.SaveChanges);
            this._operationDictLazy = new Lazy<IDictionary<string, OperationType>>(this.CreateList);
            this.Search = "";
            this.Entity = "";
        }

        public string Entity { get; set; }

        public ObservableCollection<PanoramaGroup> Entitys { get; set; }

        public ICommand SaveChangesCommand { get; set; }

        public string Search {
            get { return this._search.ToLower(); }
            set {
                this._search = value;
                this.UpdateList();
            }
        }

        public override void InitializeServices() {
            this._worker.DoWork += (sender, args) => this.UpdateList();
            this._worker.RunWorkerAsync();
        }

        public override void Refresh() {}

        public override OperationType OperationType {
            get { return OperationType.ListOp; }
        }

        private void UpdateList() {
            Task.Delay(1000);
            if(string.IsNullOrEmpty(this.Search)) {
                var alterGroup = new PanoramaGroup(Strings.Header_Alter);
                alterGroup.SetSource(
                                     this._operationDictLazy.Value.Keys.Where(
                                                                              x =>
                                                                              this._operationDictLazy.Value[x].ToString()
                                                                                                              .Contains(
                                                                                                                        "Alter"))
                                         .ToList());
                var listGroup = new PanoramaGroup(Strings.Header_List);
                listGroup.SetSource(
                                    this._operationDictLazy.Value.Keys.Where(
                                                                             x =>
                                                                             this._operationDictLazy.Value[x].ToString()
                                                                                                             .Contains(
                                                                                                                       "List"))
                                        .ToList());
                var sellGroup = new PanoramaGroup(Strings.Header_Sell);
                sellGroup.SetSource(
                                    this._operationDictLazy.Value.Keys.Where(
                                                                             x =>
                                                                             this._operationDictLazy.Value[x].ToString()
                                                                                                             .Contains(
                                                                                                                       "Sell"))
                                        .ToList());
                this.Entitys = new ObservableCollection<PanoramaGroup> {alterGroup, listGroup, sellGroup};
            }
            else {
                var alterGroup = new PanoramaGroup(Strings.Header_Alter);
                alterGroup.SetSource(
                                     this._operationDictLazy.Value.Keys.Where(
                                                                              x =>
                                                                              this._operationDictLazy.Value[x].ToString()
                                                                                                              .Contains(
                                                                                                                        "Alter"))
                                         .Where(x => x.ToLower().Contains(this.Search))
                                         .ToList());
                var listGroup = new PanoramaGroup(Strings.Header_List);
                listGroup.SetSource(
                                    this._operationDictLazy.Value.Keys.Where(
                                                                             x =>
                                                                             this._operationDictLazy.Value[x].ToString()
                                                                                                             .Contains(
                                                                                                                       "List"))
                                        .Where(x => x.ToLower().Contains(this.Search))
                                        .ToList());
                var sellGroup = new PanoramaGroup(Strings.Header_Sell);
                sellGroup.SetSource(
                                    this._operationDictLazy.Value.Keys.Where(
                                                                             x =>
                                                                             this._operationDictLazy.Value[x].ToString()
                                                                                                             .Contains(
                                                                                                                       "Sell"))
                                        .Where(x => x.ToLower().Contains(this.Search))
                                        .ToList());
                this.Entitys = new ObservableCollection<PanoramaGroup> {alterGroup, listGroup, sellGroup};
            }
        }

        private void SaveChanges(object arg) {
            var parsedEntity = this._operationDictLazy.Value[arg.ToString()];
            this._eventAggregator.GetEvent<OpenViewEvent>().Publish(parsedEntity);
            this._eventAggregator.GetEvent<CloseViewEvent>().Publish(OperationType.ListOp);
        }

        private IDictionary<string, OperationType> CreateList() {
            var enumList = Enum.GetValues(typeof(OperationType)).Cast<OperationType>().ToList();
            //Remove Unapplicables to user selection:
            enumList.Remove(OperationType.Unknown);
            enumList.Remove(OperationType.MessageTools);
            enumList.Remove(OperationType.ColumnTools);
            enumList.Remove(OperationType.HeaderTools);
            enumList.Remove(OperationType.AlterBaseEntity);
            enumList.Remove(OperationType.ListBaseEntity);
            enumList.Remove(OperationType.Main);
            enumList.Remove(OperationType.ListService);
            enumList.Remove(OperationType.AlterService);
            var operationTypes = new Dictionary<string, OperationType>(enumList.Count);
            var stringsType = typeof(Strings);
            var stringsTypeProps = stringsType.GetProperties();
            //Parse to localized string
            foreach(var operationType in enumList)
                foreach(string name in
                    from propertyInfo in stringsTypeProps
                    let item = propertyInfo.Name
                    where propertyInfo.Name.Contains(operationType.ToString())
                    select item) operationTypes.Add(stringsType.GetProperty(name).GetValue(stringsType).ToString(), operationType);
            return operationTypes;
        }

    }
}
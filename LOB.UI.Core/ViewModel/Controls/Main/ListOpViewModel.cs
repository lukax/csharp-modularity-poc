#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Domain;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class ListOpViewModel : BaseViewModel, IListOpViewModel {
        private readonly IEventAggregator _eventAggregator;
        private readonly Lazy<IDictionary<string, OperationType>> _operationDictLazy;
        private BackgroundWorker _worker = new BackgroundWorker();


        public ListOpViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            _operationDictLazy = new Lazy<IDictionary<string, OperationType>>(CreateList);
            Search = "";
        }

        [AllowNull]
        public string Entity { get; set; }

        [AllowNull]
        public ObservableCollection<PanoramaGroup> Entitys { get; set; }

        public ICommand SaveChangesCommand { get; set; }

        public string Search { get; set; }

        public override void InitializeServices() {
            _worker.DoWork += (sender, args) => UpdateList();
            _worker.RunWorkerAsync();
        }

        public override void Refresh() {
        }

        public override OperationType OperationType {
            get { return OperationType.ListOp; }
        }

        private void UpdateList() {
            Task.Delay(1000);
                if (string.IsNullOrEmpty(Search)) {
                    var alterGroup = new PanoramaGroup(Strings.Header_Alter);
                    alterGroup.SetSource(_operationDictLazy.Value.Keys.Where(x=> _operationDictLazy.Value[x].ToString().Contains("Alter")).ToList());
                    var listGroup = new PanoramaGroup(Strings.Header_List);
                    listGroup.SetSource(_operationDictLazy.Value.Keys.Where(x => _operationDictLazy.Value[x].ToString().Contains("List")).ToList());
                    var sellGroup = new PanoramaGroup(Strings.Header_Sell);
                    sellGroup.SetSource(_operationDictLazy.Value.Keys.Where(x => _operationDictLazy.Value[x].ToString().Contains("Sell")).ToList());
                    Entitys = new ObservableCollection<PanoramaGroup>{alterGroup, listGroup, sellGroup};

                }
        }
        
        private void SaveChanges(object arg) {

            var parsedEntity = _operationDictLazy.Value[arg.ToString()];
            _eventAggregator.GetEvent<OpenViewEvent>().Publish(parsedEntity);
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(OperationType.ListOp);
        }

        private IDictionary<string, OperationType> CreateList() {
            var enumList = Enum.GetValues(typeof (OperationType)).Cast<OperationType>().ToList();
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
            var stringsType = typeof (Strings);
            var stringsTypeProps = stringsType.GetProperties();
            //Parse to localized string
            foreach (var operationType in enumList) {
                foreach (string name in
                    from propertyInfo in stringsTypeProps
                    let item = propertyInfo.Name
                    where propertyInfo.Name.Contains(operationType.ToString())
                    select item) {
                    operationTypes.Add(stringsType.GetProperty(name).GetValue(stringsType).ToString(), operationType);
                }
            }
            return operationTypes;
        }
    }
}
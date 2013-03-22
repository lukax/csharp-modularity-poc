#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Main
{
    public class ListOpViewModel : BaseViewModel, IListOpViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private string _search;
        private ICollection<string> _entitys;
        public OperationType Entity { get; set; }
        public ICollectionView Entitys { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public string Search { get { return _search ?? string.Empty; } set { _search = value; } }

        public ListOpViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            _operationDictLazy = new Lazy<IDictionary<string, OperationType>>(CreateList);
            UpdateList();
        }

        private async Task UpdateList()
        {

            while (true)
            {
                await Task.Delay(500);
                if (string.IsNullOrEmpty(Search))
                {
                    var list = _operationDictLazy.Value.Keys.ToList();
                    if (Entitys == null || !Entitys.SourceCollection.Cast<string>().SequenceEqual(list))
                        Entitys = new CollectionView(list);
                }
                else
                {
                    var list = _operationDictLazy.Value.Keys.Where(x => x.ToLower().Contains(Search.ToLower())).ToList();
                    if (Entitys == null || !Entitys.SourceCollection.Cast<string>().SequenceEqual(list)) 
                        Entitys = new CollectionView(list);
                }
                _eventAggregator.GetEvent<RefreshEvent>().Publish(OperationType.ListOp);
                Entitys.CurrentChanged += (sender, args) => SaveChanges();
            }

        }

        private void SaveChanges()
        {
            Entity = _operationDictLazy.Value[Entitys.CurrentItem.ToString()];
            _eventAggregator.GetEvent<OpenViewEvent>().Publish(Entity);
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(OperationType.ListOp);
        }

        private Lazy<IDictionary<string, OperationType>> _operationDictLazy;

        private IDictionary<string, OperationType> CreateList()
        {
            var enumList = Enum.GetValues(typeof(OperationType)).Cast<OperationType>().ToList();
            //Remove Unapplicables to user selection:
            enumList.Remove(OperationType.Unknown);
            enumList.Remove(OperationType.MessageTools);
            enumList.Remove(OperationType.ColumnTools);
            enumList.Remove(OperationType.HeaderTools);
            enumList.Remove(OperationType.NewBaseEntity);
            enumList.Remove(OperationType.ListBaseEntity);
            enumList.Remove(OperationType.Main);
            enumList.Remove(OperationType.ListService);
            enumList.Remove(OperationType.NewService);
            var operationTypes = new Dictionary<string, OperationType>(enumList.Count);
            var stringsType = typeof(Resources.Localization.Strings);
            var stringsTypeProps = stringsType.GetProperties();
            //Parse to localized string
            foreach (var operationType in enumList)
            {
                foreach (string name in
                    from propertyInfo in stringsTypeProps
                    let item = propertyInfo.Name
                    where propertyInfo.Name.Contains(operationType.ToString())
                    select item)
                {
                    operationTypes.Add(stringsType.GetProperty(name).GetValue(stringsType).ToString(), operationType);
                }
            }
            return operationTypes;
        }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }

        public override OperationType OperationType
        {
            get { return OperationType.ListOp; }
        }
    }
}
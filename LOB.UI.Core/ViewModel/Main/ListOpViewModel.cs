#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using LOB.Core.Util;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Event;
using LOB.UI.Core.Event.View;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Main
{
    public class ListOpViewModel : BaseViewModel, IListOpViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public ListOpViewModel(Category entity, IRepository repository, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            SaveChangesCommand = new DelegateCommand(SaveChangesExecute);
            _operationDictLazy = new Lazy<IDictionary<string, OperationType>>(PrepareList);
            UpdateList();
        }

        public OperationType Entity { get; set; }

        private ICollectionView _entitys;
        public ICollectionView Entitys
        {
            get
            {
                var local = _entitys ?? new CollectionView(_operationDictLazy.Value.Keys);
                Entitys = local;
                return local;
            }
            set
            {
                _entitys = value;
                ListenToList(_entitys);
            }
        }

        public ICommand SaveChangesCommand { get; set; }

        private string _search;
        public string Search { get { return _search ?? string.Empty; } set { _search = value; } }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }

        private async void UpdateList()
        {
            while (true)
            {
                await Task.Delay(500);
                //Entitys.AsQueryable().
                Entitys = string.IsNullOrEmpty(Search) ? 
                    new CollectionView(_operationDictLazy.Value.Keys) : 
                    new CollectionView(_operationDictLazy.Value.Keys.Where(x => x.ToLower().Contains(Search.ToLower())));
            }
        }

        private void ListenToList(ICollectionView collection)
        {
            _eventAggregator.GetEvent<RefreshEvent>().Publish(OperationType.ListOp);
            collection.CurrentChanged += (sender, args) => SaveChangesExecute();
        }

        private Lazy<IDictionary<string, OperationType>> _operationDictLazy;
        private IDictionary<string, OperationType> PrepareList()
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

        private void SaveChangesExecute()
        {
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(OperationType.ListOp);
            Entity = _operationDictLazy.Value[Entitys.CurrentItem.ToString()];
            _eventAggregator.GetEvent<OpenViewEvent>().Publish(Entity);
        }

        public override OperationType OperationType
        {
            get { return OperationType.ListOp; }
        }
    }
}
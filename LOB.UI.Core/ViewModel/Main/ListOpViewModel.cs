#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using LOB.Core.Util;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Event.View;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Main
{
    public class ListOpViewModel : BaseViewModel, IListOpViewModel
    {
        private readonly ICommandService _commandService;
        private readonly IEventAggregator _eventAggregator;
        
        public ListOpViewModel(Category entity, IRepository repository, ICommandService commandService,
                               IEventAggregator eventAggregator)
        {
            _commandService = commandService;
            _eventAggregator = eventAggregator;
            SaveChangesCommand = new DelegateCommand(SaveChangesExecute);
            ListenToSelection();
        }

        public OperationType Entity { get; set; }

        public ICollectionView Entitys { get; set; }

        public ICommand SaveChangesCommand { get; set; }

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

        private async void ListenToSelection()
        {
            //TODO: Localization and remove of unaplicable items
            Entitys = new CollectionView(PrepareList());
            await Task.Delay(1000); //Avoid missclick & First item
            Entitys.CurrentChanged += (sender, args) => SaveChangesExecute(null);
        }

        private IEnumerable<StringWrapper> PrepareList()
        {
            var enumList = Enum.GetValues(typeof (OperationType)).Cast<OperationType>().ToList();
            //Remove Unapplicables to user selection:
            enumList.Remove(OperationType.Unknown);
            enumList.Remove(OperationType.MessageTools);
            enumList.Remove(OperationType.ColumnTools);
            enumList.Remove(OperationType.HeaderTools);
            enumList.Remove(OperationType.NewBaseEntity);
            enumList.Remove(OperationType.ListBaseEntity);
            enumList.Remove(OperationType.Main);

            var processedList = new List<StringWrapper>();
            var culture = ConfigurationManager.AppSettings["Culture"];
            foreach (var operationType in enumList)
            {
                processedList.Add(new StringWrapper(operationType.ToString()));
            }
            return processedList;
        }

        private void SaveChangesExecute(object arg)
        {
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(OperationType.ListOp);
            _eventAggregator.GetEvent<OpenViewEvent>().Publish(Entity);
        }
    }
}
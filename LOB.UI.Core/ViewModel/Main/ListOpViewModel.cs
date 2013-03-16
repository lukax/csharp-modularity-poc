#region Usings

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using LOB.Core.Util;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Event;
using LOB.UI.Core.Infrastructure;
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

        private string _entity;

        public ListOpViewModel(Category entity, IRepository repository, ICommandService commandService, IEventAggregator eventAggregator)
        {
            _commandService = commandService;
            _eventAggregator = eventAggregator;
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            Entitys = new CollectionView(OperationName.All.Wrap());
            ListenToSelection();
        }

        public string Entity
        {
            get { return _entity; }
            set
            {
                if (_entity == value) return;
                _entity = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView Entitys { get; set; }

        public ICommand SaveChangesCommand { get; set; }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }

        private async void ListenToSelection()
        {
            await Task.Delay(1000); //Avoid missclick & First item
            Entitys.CurrentChanged += (sender, args) => SaveChanges(null);
        }

        private void SaveChanges(object arg)
        {
            _eventAggregator.GetEvent<QuitEvent>().Publish(OperationParam.QuickSearch);
            _eventAggregator.GetEvent<OpenTabEvent>().Publish(Entity);
            //_commandService.Execute(OperationParam.OpenTab, Entity);
            //_commandService.Execute(OperationParam.Cancel, null);
        }
    }
}
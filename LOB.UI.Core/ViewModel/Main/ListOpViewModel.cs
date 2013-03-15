#region Usings

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using LOB.Core.Util;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.ViewModel.Main
{
    public class ListOpViewModel : BaseViewModel, IListOpViewModel
    {
        private readonly ICommandService _commandService;

        private string _entity;

        public ListOpViewModel(Category entity, IRepository repository, ICommandService commandService)
        {
            _commandService = commandService;
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            Entitys = new CollectionView(Operation.All.Wrap());
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
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }

        private async void ListenToSelection()
        {
            await Task.Delay(1000); //Avoid missclick & First item
            Entitys.CurrentChanged += (sender, args) => SaveChanges(null);
        }

        private void SaveChanges(object arg)
        {
            _commandService.Execute(OperationNames.OpenTab, Entity);
            _commandService.Execute(OperationNames.Cancel, null);
        }
    }
}
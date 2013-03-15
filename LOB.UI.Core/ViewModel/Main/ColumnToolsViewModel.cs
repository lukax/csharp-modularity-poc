#region Usings

using System;
using System.Windows.Input;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Main
{
    public sealed class ColumnToolsViewModel : IColumnToolsViewModel
    {
        private readonly ICommandService _commandService;
        private readonly IUnityContainer _container;

        public ColumnToolsViewModel(IUnityContainer container, ICommandService commandService)
        {
            _container = container;
            _commandService = commandService;
            OpsCommand = new DelegateCommand(Ops);
            ShopCommand = new DelegateCommand(Shop);
        }

        public ICommand OpsCommand { get; set; }
        public ICommand ShopCommand { get; set; }
        public string Header { get; set; }

        public void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        private void Shop(object obj)
        {
            _commandService.Execute("OpenView", "ListOp");
        }

        private void Ops(object arg)
        {
            _commandService.Execute("OpenView", "ListOp");
        }
    }
}
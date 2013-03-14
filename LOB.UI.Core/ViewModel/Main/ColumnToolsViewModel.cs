using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace LOB.UI.Core.ViewModel.Main
{
    public sealed class ColumnToolsViewModel : IColumnToolsViewModel
    {
        private readonly IUnityContainer _container;
        private readonly ICommandService _commandService;
        public ICommand OpsCommand { get; set; }
        public ICommand ShopCommand { get; set; }
        public string Header { get; set; }

        public ColumnToolsViewModel(IUnityContainer container, ICommandService commandService)
        {
            _container = container;
            _commandService = commandService;
            OpsCommand = new DelegateCommand(Ops);
            ShopCommand = new DelegateCommand(Shop);
        }

        private void Shop(object obj)
        {
            _commandService.ExecuteCommand("OpenView", "ListOp");
        }

        private void Ops(object arg)
        {
            _commandService.ExecuteCommand("OpenView", "ListOp");
        }

        public void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}

﻿#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Expression.Interactivity.Core;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterEmployeeBaseView : UserControl, IBaseView
    {
        private ICommandService _commandService;
        private string _header;
        private IFluentNavigator _navigator;

        public AlterEmployeeBaseView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterEmployeeBaseView(IAlterEmployeeViewModel viewModel, IFluentNavigator navigator,
                                     ICommandService commandService)
            : this()
        {
            _navigator = navigator;
            _commandService = commandService;
            ViewModel = viewModel;
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IBaseViewModel; }
            set
            {
                DataContext = value;
                UcAlterBaseEntityView.DataContext = value;
                UcAlterNaturalPersonView.DataContext = value;
                var localViewModel = value as IAlterEmployeeViewModel;
                if (localViewModel != null)
                {
                    UcAlterNaturalPersonView.UcAlterPersonView.UcAlterAddressView.DataContext =
                        localViewModel.AlterAddressViewModel;
                    UcAlterNaturalPersonView.UcAlterPersonView.UcAlterContactInfoView.DataContext =
                        localViewModel.AlterContactInfoViewModel;
                }

                _commandService.RegisterCommand("SaveChanges",
                                                new ActionCommand(o => _commandService["Cancel"].Execute(null)));
            }
        }


        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Funcionário" : _header; }
            set { _header = value; }
        }

        public int? Index
        {
            get { return ((AlterEmployeeViewModel) DataContext).CancelIndex; }
            set { ((AlterEmployeeViewModel) DataContext).CancelIndex = value; }
        }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }
    }
}
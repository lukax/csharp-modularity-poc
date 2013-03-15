#region Usings

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
    public partial class AlterEmployeeView : UserControl, IBaseView
    {
        private ICommandService _commandService;
        private string _header;
        private IFluentNavigator _navigator;

        public AlterEmployeeView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IAlterEmployeeViewModel; }
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

                _commandService.Register("SaveChanges",
                                                new ActionCommand(o => _commandService.Execute("Cancel",null)));
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
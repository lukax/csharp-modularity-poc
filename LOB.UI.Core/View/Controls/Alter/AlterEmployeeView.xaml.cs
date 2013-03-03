#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterEmployeeView : UserControl, ITabProp, IView
    {
        private string _header;
        private IFluentNavigator _navigator;

        public AlterEmployeeView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterEmployeeView(AlterEmployeeViewModel viewModel, IFluentNavigator navigator)
            : this()
        {
            _navigator = navigator;
            ViewModel = viewModel;
        }

        public AlterEmployeeViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
                this.UcAlterBaseEntityView.DataContext = value;
                this.UcAlterNaturalPersonView.DataContext = value;
                this.UcAlterNaturalPersonView.UcAlterPersonView.UcAlterAddressView.DataContext =
                    value.AlterAddressViewModel;
                this.UcAlterNaturalPersonView.UcAlterPersonView.UcAlterContactInfoView.DataContext =
                    value.AlterContactInfoViewModel;
                Messenger.Default.Register<object>(DataContext, "SaveChangesCommand",
                                                   o => Messenger.Default.Send("Cancel"));
                Messenger.Default.Register<object>(DataContext, "QuickSearchCommand",
                                                   o => _navigator.Resolve("QuickSearch", o).Show(true));
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
#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterProductView : UserControl, IView
    {
        private string _header;
        private IFluentNavigator _navigator;

        public AlterProductView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterProductView(AlterProductViewModel viewModel, IFluentNavigator navigator)
            : this()
        {
            _navigator = navigator;
            ViewModel = viewModel;
        }

        public AlterProductViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
                this.UcAlterBaseEntityView.DataContext = value;
                Messenger.Default.Register<object>(DataContext, "SaveChangesCommand",
                                                   o => Messenger.Default.Send("Cancel"));
            }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Produto" : _header; }
            set { _header = value; }
        }

        public int? Index
        {
            get { return ((AlterBaseEntityViewModel<Product>) DataContext).CancelIndex; }
            set { ((AlterBaseEntityViewModel<Product>) DataContext).CancelIndex = value; }
        }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }
    }
}
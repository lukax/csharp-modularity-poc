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
    public partial class AlterProductView : UserControl, ITabProp, IView
    {
        private IFluentNavigator _navigator;

        private string _header;

        public AlterProductView()
        {
            InitializeComponent();
        }

        public AlterProductViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
                this.UcAlterBaseEntityView.DataContext = value;
                Messenger.Default.Register<object>(DataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
                Messenger.Default.Register<object>(DataContext, "QuickSearchCommand", o => _navigator.Resolve("QuickSearch", o).Show(true) );
            }
        }

        [ImportingConstructor]
        public AlterProductView(AlterProductViewModel viewModel, IFluentNavigator navigator)
            : this()
        {
            _navigator = navigator;
            ViewModel = viewModel;
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Produto" : _header; }
            set { _header = value; }
        }

        public int? Index
        {
            get { return ((AlterBaseEntityViewModel<Product>)DataContext).CancelIndex; }
            set { ((AlterBaseEntityViewModel<Product>)DataContext).CancelIndex = value; }
        }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }
    }
}
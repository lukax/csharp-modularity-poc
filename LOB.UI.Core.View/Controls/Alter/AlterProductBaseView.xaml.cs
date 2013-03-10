#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterProductBaseView : UserControl, IBaseView
    {
        private string _header;
        private IFluentNavigator _navigator;

        public AlterProductBaseView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterProductBaseView(IAlterProductViewModel viewModel, IFluentNavigator navigator)
            : this()
        {
            _navigator = navigator;
            ViewModel = viewModel;
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IBaseViewModel; }
            set
            {
                DataContext = value;
                UcAlterBaseEntityView.DataContext = value;
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
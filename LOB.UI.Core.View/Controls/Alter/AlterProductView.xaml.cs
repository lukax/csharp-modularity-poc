#region Usings

using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    public partial class AlterProductView : UserControl, IBaseView
    {
        private string _header;
        private IFluentNavigator _navigator;

        public AlterProductView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IAlterProductViewModel; }
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

        public Interface.Infrastructure.OperationType OperationType
        {
            get { return OperationType.AlterProduct; }
        }
    }
}
#region Usings

using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain.Base;
using LOB.UI.Core.View.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterCustomerView : UserControl, ITabProp, IView
    {
        private string _header;
        private IUnityContainer _container;

        public AlterCustomerView()
        {
            InitializeComponent();
        }

        public AlterCustomerViewModel ViewModel { set { this.DataContext = value; } }
        [ImportingConstructor]
        public AlterCustomerView(AlterCustomerViewModel viewModel, IUnityContainer container, INavigator navigator)
            : this()
        {
            ViewModel = viewModel;
            _container = container;

            InitializeServices();
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Cliente" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
            Messenger.Default.Register<object>(DataContext, "PersonTypeChanged", o => { TabAlterPersonDetails.Content = o; });
            Messenger.Default.Register<object>(DataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
        }

        public void Refresh()
        {
        }
    }
}
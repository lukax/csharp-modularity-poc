#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterCustomerView : UserControl, ITabProp, IView
    {
        private IUnityContainer _container;
        private string _header;

        public AlterCustomerView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterCustomerView(AlterCustomerViewModel viewModel, IUnityContainer container, IFluentNavigator navigator)
            : this()
        {
            ViewModel = viewModel;
            _container = container;
        }

        public AlterCustomerViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
                this.UcAlterBaseEntity.DataContext = value;
                Messenger.Default.Register<object>(DataContext, "PersonTypeChanged",
                                                   o => { UcAlterPersonDetails.Content = o; });
                Messenger.Default.Register<object>(DataContext, "SaveChangesCommand",
                                                   o => Messenger.Default.Send("Cancel"));
            }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Cliente" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }
    }
}
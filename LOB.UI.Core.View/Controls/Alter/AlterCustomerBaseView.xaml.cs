#region Usings

using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    public partial class AlterCustomerBaseView : UserControl, IBaseView
    {
        private IUnityContainer _container;
        private string _header;

        [InjectionConstructor]
        public AlterCustomerBaseView(IAlterCustomerViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as AlterCustomerViewModel; }
            set
            {
                DataContext = value;
                UcAlterBaseEntity.DataContext = value;
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
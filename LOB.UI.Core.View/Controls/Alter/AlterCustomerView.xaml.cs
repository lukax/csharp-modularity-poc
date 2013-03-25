#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    public partial class AlterCustomerView : UserControl, IBaseView
    {
        private string _header;

        [InjectionConstructor]
        public AlterCustomerView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as AlterCustomerViewModel; }
            set
            {
                DataContext = value;
                UcAlterBaseEntity.DataContext = value;
                //Messenger.Default.Register<object>(DataContext, "PersonTypeChanged",o => { UcAlterPersonDetails.Content = o; });
                //Messenger.Default.Register<object>(DataContext, "SaveChangesCommand",o => Messenger.Default.Send("Cancel"));
            }
        }

        public string Header
        {
            get { return Strings.Header_Alter_Customer; }
        }

        public int Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }

        public OperationType OperationType
        {
            get { return OperationType.AlterCustomer; }
        }
    }
}
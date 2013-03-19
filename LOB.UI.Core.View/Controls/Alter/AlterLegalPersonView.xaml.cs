#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    public partial class AlterLegalPersonView : UserControl, IBaseView
    {
        private string _header;

        public AlterLegalPersonView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IAlterLegalPersonViewModel; }
            set
            {
                DataContext = value;
                var localViewModel = value as IAlterLegalPersonViewModel;
                if (localViewModel != null)
                {
                    UcAlterPersonView.DataContext = value;
                    UcAlterPersonView.UcAlterAddressView.DataContext = localViewModel.AlterAddressViewModel;
                    UcAlterPersonView.UcAlterContactInfoView.DataContext = localViewModel.AlterContactInfoViewModel;
                }
            }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Pessoa" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }

        public Interface.Infrastructure.OperationType OperationType
        {
            get { return OperationType.AlterLegalPerson; }
        }
    }
}
#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterLegalPersonView : UserControl, IView
    {
        private string _header;

        public AlterLegalPersonView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterLegalPersonView(AlterLegalPersonViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public AlterLegalPersonViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
                //this.UcAlterBaseEntityView.DataContext = value;
                this.UcAlterPersonView.DataContext = value;
                this.UcAlterPersonView.UcAlterAddressView.DataContext = value.AlterAddressViewModel;
                this.UcAlterPersonView.UcAlterContactInfoView.DataContext = value.AlterContactInfoViewModel;
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
    }
}
#region Usings
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterNaturalPersonView : UserControl, IBaseView {

        private string _header;

        public AlterNaturalPersonView() {
            this.InitializeComponent();
        }

        public IBaseViewModel ViewModel {
            get { return this.DataContext as IAlterNaturalPersonViewModel; }
            set {
                this.DataContext = value;
                var localViewModel = value as IAlterNaturalPersonViewModel;
                if(localViewModel != null) {
                    this.UcAlterPersonView.DataContext = value;
                    this.UcAlterPersonView.UcAlterAddressView.DataContext = localViewModel.AlterAddressViewModel;
                    this.UcAlterPersonView.UcAlterContactInfoView.DataContext = localViewModel.AlterContactInfoViewModel;
                }
            }
        }

        public string Header {
            get { return Strings.Header_Alter_NaturalPerson; }
        }

        public int Index { get; set; }

        public void InitializeServices() {}

        public void Refresh() {}

        public OperationType OperationType {
            get { return OperationType.AlterNaturalPerson; }
        }

    }
}
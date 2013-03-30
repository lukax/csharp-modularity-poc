#region Usings
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    public partial class AlterPayCheckView : UserControl, IBaseView {

        public AlterPayCheckView() {
            this.InitializeComponent();
        }

        public IBaseViewModel ViewModel {
            get { return this.DataContext as IAlterPayCheckViewModel; }
            set {
                this.DataContext = value;
                this.UcAlterBaseEntityView.DataContext = value;
            }
        }

        public string Header {
            get { return Strings.Header_Alter_PayCheck; }
        }

        public int Index { get; set; }

        public void InitializeServices() {}

        public void Refresh() {}

        public OperationType OperationType {
            get { return OperationType.AlterPayCheck; }
        }

    }
}
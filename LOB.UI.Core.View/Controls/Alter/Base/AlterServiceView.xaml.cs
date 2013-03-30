#region Usings
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base {
    public partial class AlterServiceView : UserControl, IBaseView {

        private string _header;

        public AlterServiceView() {
            this.InitializeComponent();
        }

        public IBaseViewModel ViewModel {
            get { return this.DataContext as IAlterServiceViewModel; }
            set { this.DataContext = value; }
        }

        public string Header {
            get { return Strings.Header_Alter_Service; }
        }

        public int Index { get; set; }

        public void InitializeServices() {}

        public void Refresh() {}

        public OperationType OperationType {
            get { return OperationType.AlterService; }
        }

    }
}
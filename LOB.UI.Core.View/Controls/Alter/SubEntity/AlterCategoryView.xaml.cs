#region Usings
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    public partial class AlterCategoryView : UserControl, IBaseView {

        private string _header;

        public AlterCategoryView() {
            this.InitializeComponent();
        }

        public IBaseViewModel ViewModel {
            get { return this.DataContext as IAlterCategoryViewModel; }
            set {
                this.DataContext = value;
                this.UcAlterServiceView.DataContext = value;
            }
        }

        public string Header {
            get { return Strings.Header_Alter_Category; }
        }

        public int Index { get; set; }

        public void InitializeServices() {}

        public void Refresh() {}

        public OperationType OperationType {
            get { return OperationType.AlterCategory; }
        }

    }
}
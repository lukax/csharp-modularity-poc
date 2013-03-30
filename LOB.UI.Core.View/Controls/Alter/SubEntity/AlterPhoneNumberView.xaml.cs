#region Usings
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    public partial class AlterPhoneNumberView : UserControl, IBaseView {

        public AlterPhoneNumberView() {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterPhoneNumberViewModel; }
            set { DataContext = value; }
        }

        public string Header {
            get { return Strings.Header_Alter_PhoneNumber; }
        }

        public int Index { get; set; }

        public void InitializeServices() {}

        public void Refresh() {}

        public OperationType OperationType {
            get { return OperationType.AlterPhoneNumber; }
        }

    }
}
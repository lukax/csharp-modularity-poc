#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    public partial class AlterAddressView : UserControl, IBaseView {

        private string _header;

        public AlterAddressView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterAddressViewModel; }
            set {
                DataContext = value;
                ViewConfCancelTools.DataContext = value;
                ViewAlterBaseEntity.DataContext = value;
            }
        }

        public string Header {
            get { return Strings.Header_Alter_Address; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation UIOperation {
            get { return ViewModel.UIOperation; }
        }

    }
}
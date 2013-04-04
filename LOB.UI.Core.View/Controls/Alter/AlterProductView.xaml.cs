#region Usings

using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterProductView : IBaseView {
        public AlterProductView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterProductViewModel; }
            set {
                DataContext = value;
                ViewEditTools.DataContext = value;
                ViewAlterBaseEntity.DataContext = value;
                ViewConfCancelTools.DataContext = value;
            }
        }

        public string Header {
            get { return Strings.Header_Alter_Product; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }
    }
}
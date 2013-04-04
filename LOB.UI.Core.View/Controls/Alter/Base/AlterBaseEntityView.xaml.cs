#region Usings

using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base {
    public partial class AlterBaseEntityView : IBaseView {
        public AlterBaseEntityView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public string Header {
            get { return Strings.Header_Alter_BaseEntity; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }
    }
}
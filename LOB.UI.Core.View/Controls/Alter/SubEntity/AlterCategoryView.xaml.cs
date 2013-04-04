#region Usings

using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    public partial class AlterCategoryView : IBaseView {
        public AlterCategoryView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterCategoryViewModel; }
            set {
                DataContext = value;
                ViewAlterService.DataContext = value;
                ViewConfCancelTools.DataContext = value;
            }
        }

        public string Header {
            get { return Strings.Header_Alter_Category; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }
    }
}
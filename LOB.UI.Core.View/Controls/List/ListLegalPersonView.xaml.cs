#region Usings

using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.View.Controls.List {
    public partial class ListLegalPersonView : IBaseView
    {

        public ListLegalPersonView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IListLegalPersonViewModel; }
            set {
                DataContext = value;
                ViewListBaseEntity.DataContext = value;
                ViewListContextTool.DataContext = value;
            }
        }

        public string Header {
            get { return Strings.Header_List_Category; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }

    }
}
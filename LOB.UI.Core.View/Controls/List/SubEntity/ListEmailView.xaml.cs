#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity {
    public partial class ListEmailView : UserControl, IBaseView {

        private string _header;

        public ListEmailView() {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel {
            get { return DataContext as IListEmailViewModel; }
            set { DataContext = value; }
        }

        public string Header {
            get { return Strings.Header_List_Email; }
        }

        public int Index { get; set; }

        public void InitializeServices() {}

        public void Refresh() {}

        public UIOperation UIOperation {
            get { return ViewModel.UIOperation; }
        }

    }
}
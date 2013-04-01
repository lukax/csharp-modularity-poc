#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Main {
    /// <summary>
    ///     Interaction logic for BusyView.xaml
    /// </summary>
    public partial class BusyView : UserControl, IBaseView {

        public BusyView() {
            InitializeComponent();
        }

        public UIOperation UIOperation {
            get { return ViewModel.UIOperation; }
        }
        public IBaseViewModel ViewModel { get; set; }

        public string Header {
            get { return Strings.Header_Main_Busy; }
        }

        public int Index { get; set; }

        public void InitializeServices() {}

        public void Refresh() {}

    }
}
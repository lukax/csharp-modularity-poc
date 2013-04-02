#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterLegalPersonView : UserControl, IBaseView {

        private string _header;

        public AlterLegalPersonView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterLegalPersonViewModel; }
            set {
                DataContext = value;
                var localViewModel = value as IAlterLegalPersonViewModel;
                if(localViewModel != null) {
                    ViewAlterPerson.DataContext = value;
                    ViewEditTools.DataContext = value;
                    ViewAlterPerson.ViewAlterAddress.DataContext = localViewModel.AlterAddressViewModel;
                    ViewAlterPerson.ViewAlterAddress.DataContext = localViewModel.AlterContactInfoViewModel;
                }
            }
        }

        public string Header {
            get { return Strings.Header_Alter_LegalPerson; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation UIOperation {
            get { return ViewModel.UIOperation; }
        }

    }
}
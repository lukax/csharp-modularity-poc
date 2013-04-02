#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterNaturalPersonView : UserControl, IBaseView {

        private string _header;

        public AlterNaturalPersonView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterNaturalPersonViewModel; }
            set {
                DataContext = value;
                var localViewModel = value as IAlterNaturalPersonViewModel;
                if(localViewModel != null) {
                    ViewAlterPerson.DataContext = value;
                    ViewAlterPerson.ViewAlterAddress.DataContext = localViewModel.AlterAddressViewModel;
                    ViewAlterPerson.ViewAlterContactInfo.DataContext = localViewModel.AlterContactInfoViewModel;
                }
            }
        }

        public string Header {
            get { return Strings.Header_Alter_NaturalPerson; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation UIOperation {
            get { return ViewModel.UIOperation; }
        }

    }
}
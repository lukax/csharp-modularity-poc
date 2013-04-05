#region Usings

using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterStoreView : IBaseView {

        public AlterStoreView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterStoreViewModel; }
            set {
                DataContext = value;
                var localViewModel = value as IAlterStoreViewModel;
                if(localViewModel != null) {
                    //ViewAlterPerson.DataContext = value;
                    //ViewEditTools.DataContext = value;
                    //ViewAlterPerson.ViewAlterAddress.DataContext =
                    //    localViewModel.AlterAddressViewModel;
                    //ViewAlterPerson.ViewAlterAddress.DataContext =
                    //    localViewModel.AlterContactInfoViewModel;
                }
            }
        }

        public string Header {
            get { return Strings.Header_Alter_LegalPerson; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }

    }
}
#region Usings

using System;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base {
    public partial class AlterPersonView : IBaseView {
        public AlterPersonView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterPersonViewModel; }
            set {
                DataContext = value;
                var localViewModel = value as IAlterPersonViewModel;
                if(localViewModel != null) {
                    ViewAlterAddress.DataContext = localViewModel.AlterAddressViewModel;
                    ViewAlterContactInfo.DataContext = localViewModel.AlterContactInfoViewModel;
                }
            }
        }

        public string Header {
            get { return Strings.Header_Alter_Person; }
        }

        public int Index { get; set; }

        public void InitializeServices() { throw new NotImplementedException(); }

        public void Refresh() { throw new NotImplementedException(); }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }
    }
}
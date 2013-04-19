#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity {
    [Export]
    public partial class ListPhoneNumberView : IBaseView<IListPhoneNumberViewModel> {
        public ListPhoneNumberView() { InitializeComponent(); }
        
        [Import] public IListPhoneNumberViewModel ViewModel {
            get { return DataContext as IListPhoneNumberViewModel; }
            set { DataContext = value; }
        }

        public string Header {
            get { return Strings.UI_Header_List_PhoneNumber; }
        }

        public int Index { get; set; }

        public void Refresh() { }

        public ViewID ViewID {
            get { return ViewModel.ViewID; }
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity {
    [Export(typeof(IBaseView<IListPhoneNumberViewModel>))]
    [ViewInfo(ViewType.PhoneNumber, new[] { ViewState.List, ViewState.QuickSearch })]
    public partial class ListPhoneNumberView : IBaseView<IListPhoneNumberViewModel> {
        public ListPhoneNumberView() { InitializeComponent(); }

        [Import] public IListPhoneNumberViewModel ViewModel {
            get { return DataContext as IListPhoneNumberViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        public int Index { get; set; }

        public void Refresh() { }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity {
    [Export(typeof(IBaseView<IListPhoneNumberViewModel>)), Export(typeof(IBaseView<IBaseViewModel>))]
    [ViewInfo(ViewType.PhoneNumber, new[] {ViewState.List, ViewState.QuickSearch})]
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
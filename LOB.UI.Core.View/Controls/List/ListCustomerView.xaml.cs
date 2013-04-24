#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.View.Controls.List {
    [Export(typeof(IBaseView<IListCustomerViewModel>))]
    [ViewInfo(ViewType.Customer, new[] {ViewState.List, ViewState.QuickSearch})]
    public partial class ListCustomerView : IBaseView<IListCustomerViewModel> {
        public ListCustomerView() { InitializeComponent(); }

        [Import] public IListCustomerViewModel ViewModel {
            get { return DataContext as IListCustomerViewModel; }
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
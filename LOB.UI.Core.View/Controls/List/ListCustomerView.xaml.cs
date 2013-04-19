#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.View.Controls.List {
    [Export]
    public partial class ListCustomerView : IBaseView<IListCustomerViewModel> {
        public ListCustomerView() { InitializeComponent(); }

        [Import] public IListCustomerViewModel ViewModel {
            get { return DataContext as IListCustomerViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        public string Header {
            get { return Strings.UI_Header_List_Customer; }
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
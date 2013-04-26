#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.List;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.List {
    /// <summary>
    ///     Interaction logic for ListCommandView.xaml
    /// </summary>
    [Export(typeof(IBaseView<IListOpViewModel>)), Export(typeof(IBaseView<IBaseViewModel>))]
    [ViewInfo(ViewType.Op, ViewState.Other)]
    public partial class ListOpView : IBaseView<IListOpViewModel> {
        public ListOpView() { InitializeComponent(); }

        [Import] public IListOpViewModel ViewModel {
            get { return DataContext as IListOpViewModel; }
            set { DataContext = value; }
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
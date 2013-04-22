#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.View.Controls.List {
    /// <summary>
    ///     Interaction logic for ListCommandView.xaml
    /// </summary>
    [Export(typeof(IBaseView<IListOpViewModel>)), Export(typeof(IBaseView<IBaseViewModel>))]
    [ViewInfo(ViewType.Op, ViewState.List)]
    public partial class ListOpView : IBaseView<IListOpViewModel> {
        public ListOpView() { InitializeComponent(); }

        [Import] public IListOpViewModel ViewModel {
            get { return DataContext as IListOpViewModel; }
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
#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.List.Base {
    [Export(typeof(IBaseView<IBaseViewModel>))]
    [ViewInfo(ViewType.BaseEntity, ViewState.Other)]
    public partial class ListBaseEntityView : IBaseView<IBaseViewModel> {
        public ListBaseEntityView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IBaseViewModel; }
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
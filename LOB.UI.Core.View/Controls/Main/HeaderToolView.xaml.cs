#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;

#endregion

namespace LOB.UI.Core.View.Controls.Main {
    /// <summary>
    ///     Interaction logic for ColumnToolView.xaml
    /// </summary>
    [Export(typeof(IBaseView<IHeaderToolViewModel>))]
    [ViewInfo(ViewType.HeaderTool, ViewState.Other)]
    public partial class HeaderToolView : IBaseView<IHeaderToolViewModel> {
        public HeaderToolView() { InitializeComponent(); }

        [Import] public IHeaderToolViewModel ViewModel {
            get { return DataContext as IHeaderToolViewModel; }
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
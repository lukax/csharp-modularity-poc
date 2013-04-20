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
    ///     Interaction logic for ColumnToolIuiComponent.xaml
    /// </summary>
    [Export(typeof(IBaseView<INotificationToolViewModel>)), Export]
    [ViewInfo(ViewType.NotificationTool, ViewState.Other)]
    public partial class NotificationToolView : IBaseView<INotificationToolViewModel> {
        public NotificationToolView() { InitializeComponent(); }

        [Import] public INotificationToolViewModel ViewModel {
            get { return DataContext as INotificationToolViewModel; }
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
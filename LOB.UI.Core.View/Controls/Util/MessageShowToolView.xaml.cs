#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;

#endregion

namespace LOB.UI.Core.View.Controls.Util {
    /// <summary>
    ///     Interaction logic for MessageShowToolIuiComponent.xaml
    /// </summary>
    [Export(typeof(IBaseView<IMessageToolViewModel>)), Export]
    [ViewInfo(ViewType.MessageTool, ViewState.Other)]
    public partial class MessageShowToolView : IBaseView<IMessageToolViewModel> {
        public MessageShowToolView() { InitializeComponent(); }

        [Import] public IMessageToolViewModel ViewModel {
            get {
                IMessageToolViewModel result = null;
                Dispatcher.Invoke(() => result = DataContext as IMessageToolViewModel);
                return result;
            }
            set {
                IMessageToolViewModel result = value;
                Dispatcher.Invoke(() => DataContext = result);
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
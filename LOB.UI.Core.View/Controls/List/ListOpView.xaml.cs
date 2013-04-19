#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.View.Controls.List {
    /// <summary>
    ///     Interaction logic for ListCommandView.xaml
    /// </summary>
    [Export]
    public partial class ListOpView : IBaseView<IListOpViewModel> {
        [Import] public IEventAggregator EventAggregator { get; set; }

        public ListOpView() { InitializeComponent(); }

        [Import] public IListOpViewModel ViewModel {
            get { return DataContext as IListOpViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        public string Header {
            get { return Strings.UI_Header_List_Op; }
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
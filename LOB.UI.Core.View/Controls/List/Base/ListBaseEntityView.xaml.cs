#region Usings

using System;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.List.Base {
    public partial class ListBaseEntityView : IBaseView {
        public ListBaseEntityView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) { }

        public IBaseViewModel ViewModel {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public int Index { get; set; }

        public string Header {
            get { return Strings.UI_Header_List_BaseEntity; }
        }

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
#region Usings

using System;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.List {
    public partial class ListCustomerView : IBaseView {

        public ListCustomerView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) { }

        public IBaseViewModel ViewModel { get { return DataContext as IBaseViewModel; } set { DataContext = value; } }

        public string Header { get { return Strings.UI_Header_List_Customer; } }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public ViewID Operation { get { return ViewModel.Operation; } }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
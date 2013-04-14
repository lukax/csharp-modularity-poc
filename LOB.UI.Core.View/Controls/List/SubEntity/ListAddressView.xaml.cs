#region Usings

using System;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity {
    public partial class ListAddressView : IBaseView {

        public ListAddressView()
        {
            InitializeComponent(); DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ViewListBaseEntity.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
            ViewListContextTool.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
        }

        public IBaseViewModel ViewModel { get { return DataContext as IBaseViewModel; } set { DataContext = value; } }

        public string Header {
            get { return Strings.UI_Header_List_Category; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public ViewID Operation {
            get { return ViewModel.Operation; }
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
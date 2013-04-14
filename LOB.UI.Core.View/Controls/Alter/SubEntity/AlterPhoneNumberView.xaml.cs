#region Usings

using System;
using System.Windows;
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    public partial class AlterPhoneNumberView : UserControl, IBaseView {

        public AlterPhoneNumberView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
        }

        public IBaseViewModel ViewModel { get { return DataContext as IBaseViewModel; } set { DataContext = value; } }

        public string Header { get { return Strings.UI_Header_Alter_PhoneNumber; } }

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
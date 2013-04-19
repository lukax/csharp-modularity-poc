#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterProductView : IBaseView<IAlterProductViewModel> {
        public AlterProductView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            ViewEditTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
        }

        [Import] public IAlterProductViewModel ViewModel {
            get { return DataContext as IAlterProductViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        public string Header {
            get { return Strings.UI_Header_Alter_Product; }
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
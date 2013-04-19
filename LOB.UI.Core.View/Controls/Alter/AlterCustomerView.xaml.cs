#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Core.View.Controllers;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterCustomerView : IBaseView<IAlterCustomerViewModel> {
        [Import] public CustomerRegionController Controller { get; set; }

        [ImportingConstructor]
        public AlterCustomerView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            ViewEditTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
        }

        [Import] public IAlterCustomerViewModel ViewModel {
            get { return DataContext as IAlterCustomerViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        public string Header {
            get { return Strings.UI_Header_Alter_Customer; }
        }

        public int Index { get; set; }

        public void Refresh() { }

        public ViewID ViewID {
            get { return ViewModel.ViewID; }
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            if(Controller != null) Controller.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Core.View.Controllers;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    [Export(typeof(IBaseView<IAlterCustomerViewModel>))]
    [ViewInfo(ViewType.Customer, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
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

        public int Index { get; set; }

        public void Refresh() { }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            if(Controller != null) Controller.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
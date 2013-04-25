#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Alter;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    [Export(typeof(IBaseView<IAlterSaleViewModel>))]
    [ViewInfo(ViewType.Sale, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
    public partial class AlterSaleView : IBaseView<IAlterSaleViewModel> {
        public AlterSaleView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            ViewEditTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
        }

        [Import] public IAlterSaleViewModel ViewModel {
            get { return DataContext as IAlterSaleViewModel; }
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
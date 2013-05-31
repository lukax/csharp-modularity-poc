#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Alter;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base {
    [Export(typeof(IBaseView<IAlterProductViewModel>))]
    [ViewInfo(ViewType.Product, new[] {ViewState.Add, ViewState.Update, ViewState.Delete}), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ProductView : IBaseView<IAlterProductViewModel> {
        public ProductView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) { }

        [Import] public IAlterProductViewModel ViewModel {
            get { return DataContext as IAlterProductViewModel; }
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
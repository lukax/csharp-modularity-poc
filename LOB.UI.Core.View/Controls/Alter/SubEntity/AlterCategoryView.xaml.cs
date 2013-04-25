#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    [Export(typeof(IBaseView<IAlterCategoryViewModel>)), Export(typeof(IBaseView<IBaseViewModel>))]
    [ViewInfo(ViewType.Category, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
    public partial class AlterCategoryView : IBaseView<IAlterCategoryViewModel> {
        public AlterCategoryView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            // ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
        }

        [Import] public IAlterCategoryViewModel ViewModel {
            get { return DataContext as IAlterCategoryViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        public int Index { get; set; }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
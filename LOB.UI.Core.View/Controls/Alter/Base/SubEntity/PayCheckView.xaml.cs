#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base.SubEntity {
    [Export(typeof(IBaseView<IAlterPayCheckViewModel>)), Export(typeof(IBaseView<IBaseViewModel>)), PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewInfo(ViewType.PayCheck, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
    public partial class AlterPayCheckView : IBaseView<IAlterPayCheckViewModel> {
        public AlterPayCheckView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
        }

        [Import] public IAlterPayCheckViewModel ViewModel {
            get { return DataContext as IAlterPayCheckViewModel; }
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
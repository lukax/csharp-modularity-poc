#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    [Export(typeof(IBaseView<IAlterAddressViewModel>))]
    [ViewInfo(ViewType.Address, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
    public partial class AlterAddressView : IBaseView<IAlterAddressViewModel> {
        public AlterAddressView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
            ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
        }

        [Import] public IAlterAddressViewModel ViewModel {
            get { return DataContext as IAlterAddressViewModel; }
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
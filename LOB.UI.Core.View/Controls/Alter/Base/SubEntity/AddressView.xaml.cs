#region Usings

using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base.SubEntity {
    [Export(typeof(IBaseView<IAlterAddressViewModel>)), Export(typeof(IBaseView<IBaseViewModel>)), PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewInfo(ViewType.Address, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
    public partial class AlterAddressView : IBaseView<IAlterAddressViewModel> {
        public AlterAddressView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
        }

        [Import] public IAlterAddressViewModel ViewModel {
            get { return DataContext as IAlterAddressViewModel; }
            set { DataContext = value; }
        }

        public int Index { get; set; }

        public void Dispose() { ViewModel.Dispose(); }
    }
}
#region Usings

using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    [Export(typeof(IBaseView<IAlterContactInfoViewModel>)), Export(typeof(IBaseView<IBaseViewModel>)), PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewInfo(ViewType.ContactInfo, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
    public partial class AlterContactInfoView : IBaseView<IAlterContactInfoViewModel> {
        public AlterContactInfoView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
        }

        [Import] public IAlterContactInfoViewModel ViewModel {
            get { return DataContext as IAlterContactInfoViewModel; }
            set { DataContext = value; }
        }

        public int Index { get; set; }
        
        public void Dispose() { ViewModel.Dispose(); }
    }
}
#region Usings

using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Alter;
using LOB.UI.Core.View.Controllers;
using LOB.UI.Core.View.Controllers.Controls;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    [Export(typeof(IBaseView<IAlterNaturalPersonViewModel>)), Export(typeof(IBaseView<IBaseViewModel>)), PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewInfo(ViewType.NaturalPerson, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
    public partial class AlterNaturalPersonView : IBaseView<IAlterNaturalPersonViewModel> {
        private NaturalPersonRegionController _controller;
        public AlterNaturalPersonView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
        }

        public IAlterNaturalPersonViewModel ViewModel {
            get { return DataContext as IAlterNaturalPersonViewModel; }
            set { DataContext = value; }
        }

        [Import] public NaturalPersonRegionController Controller {
            get { return _controller; }
            set {
                _controller = value;
                ViewModel = _controller.ViewModel;
            }
        }

        public int Index { get; set; }

        public void Dispose() { Controller.Dispose(); }
    }
}
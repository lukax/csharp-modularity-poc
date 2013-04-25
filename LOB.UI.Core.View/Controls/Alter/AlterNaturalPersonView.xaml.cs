#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Alter;
using LOB.UI.Core.View.Controllers;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    [Export(typeof(IBaseView<IAlterNaturalPersonViewModel>)), Export(typeof(IBaseView<IBaseViewModel>))]
    [ViewInfo(ViewType.NaturalPerson, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
    public partial class AlterNaturalPersonView : IBaseView<IAlterNaturalPersonViewModel> {
        public AlterNaturalPersonView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            var view = dependencyPropertyChangedEventArgs.NewValue as IAlterNaturalPersonViewModel;
        }

        [Import] public IAlterNaturalPersonViewModel ViewModel {
            get { return DataContext as IAlterNaturalPersonViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        [Import] public NaturalPersonRegionController Controller {
            set { value.ThisOne = new Lazy<IBaseViewModel>(() => ViewModel); }
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
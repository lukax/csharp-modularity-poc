#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    [Export(typeof(IBaseView<IAlterNaturalPersonViewModel>))]
    [ViewInfo(ViewType.NaturalPerson, new[] { ViewState.Add, ViewState.Update, ViewState.Delete })]
    public partial class AlterNaturalPersonView : IBaseView<IAlterNaturalPersonViewModel> {
        public AlterNaturalPersonView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            var view = dependencyPropertyChangedEventArgs.NewValue as IAlterNaturalPersonViewModel;
            ViewAlterPerson.DataContext = view != null ? view.AlterPersonViewModel : dependencyPropertyChangedEventArgs.NewValue;
        }

        [Import] public IAlterNaturalPersonViewModel ViewModel {
            get { return DataContext as IAlterNaturalPersonViewModel; }
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
#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity {
    [Export(typeof(IBaseView<IListContactInfoViewModel>)), Export(typeof(IBaseView<IBaseViewModel>)), PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewInfo(ViewType.ContactInfo, new[] {ViewState.List, ViewState.QuickSearch})]
    public partial class ListContactInfoView : IBaseView<IListContactInfoViewModel> {
        public ListContactInfoView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewListBaseEntity.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
            ViewListContextTool.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
        }

        [Import] public IListContactInfoViewModel ViewModel {
            get { return DataContext as IListContactInfoViewModel; }
            set { DataContext = value; }
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
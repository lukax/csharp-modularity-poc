#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.View.Controls.List {
    [Export]
    public partial class ListNaturalPersonView : IBaseView<IListNaturalPersonViewModel> {
        public ListNaturalPersonView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewListBaseEntity.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
            ViewListContextTool.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
        }

        [Import] public IListNaturalPersonViewModel ViewModel {
            get { return DataContext as IListNaturalPersonViewModel; }
            set { DataContext = value; }
        }

        public string Header {
            get { return Strings.UI_Header_List_Category; }
        }

        public int Index { get; set; }

        public void Refresh() { }

        public ViewID ViewID {
            get { return ViewModel.ViewID; }
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
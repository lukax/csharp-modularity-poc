#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity {
    [Export]
    public partial class ListPayCheckView : IBaseView<IListPayCheckViewModel> {
        public ListPayCheckView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewListBaseEntity.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
            ViewListContextTool.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
        }

        [Import] public IListPayCheckViewModel ViewModel {
            get { return DataContext as IListPayCheckViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
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
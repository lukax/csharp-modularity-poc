#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    public partial class AlterEmailView : IBaseView<IAlterEmailViewModel> {
        public AlterEmailView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
            ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue as IBaseViewModel;
        }

        [Import] public IAlterEmailViewModel ViewModel {
            get { return DataContext as IAlterEmailViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        public string Header {
            get { return Strings.UI_Header_Alter_Email; }
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
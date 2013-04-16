#region Usings

using System;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base {
    public partial class AlterPersonView : IBaseView {
        public AlterPersonView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            var view = dependencyPropertyChangedEventArgs.NewValue as AlterPersonViewModel;
            if(view != null) {
                ViewAlterAddress.DataContext = view.AlterAddressViewModel;
                ViewAlterContactInfo.DataContext = view.AlterContactInfoViewModel;
            }
            else {
                ViewAlterAddress.DataContext = dependencyPropertyChangedEventArgs.NewValue;
                ViewAlterContactInfo.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            }
        }

        public IBaseViewModel ViewModel {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public string Header {
            get { return Strings.UI_Header_Alter_Person; }
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
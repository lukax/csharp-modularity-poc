#region Usings

using System;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterLegalPersonView : IBaseView {

        public AlterLegalPersonView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            var model = dependencyPropertyChangedEventArgs.NewValue as IAlterLegalPersonViewModel;
            if(model != null) {
                ViewCode.DataContext = dependencyPropertyChangedEventArgs.NewValue;
                ViewAlterPerson.DataContext = dependencyPropertyChangedEventArgs.NewValue;
                ViewConfCancelTools.DataContext = dependencyPropertyChangedEventArgs.NewValue;
                ViewAlterPerson.ViewAlterAddress.DataContext = model.AlterAddressViewModel;
                ViewAlterPerson.ViewAlterAddress.DataContext = model.AlterContactInfoViewModel;
            }
        }

        public IBaseViewModel ViewModel { get { return DataContext as IBaseViewModel; } set { DataContext = value; } }

        public string Header { get { return Strings.UI_Header_Alter_LegalPerson; } }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation { get { return ViewModel.Operation; } }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
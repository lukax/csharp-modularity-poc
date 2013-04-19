#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base {
    public partial class AlterPersonView : IBaseView<IAlterPersonViewModel> {
        public AlterPersonView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            var view = dependencyPropertyChangedEventArgs.NewValue as IAlterPersonViewModel;
            if(view != null) {
                ViewAlterAddress.DataContext = view.AlterAddressViewModel;
                ViewAlterContactInfo.DataContext = view.AlterContactInfoViewModel;
            }
            else {
                ViewAlterAddress.DataContext = dependencyPropertyChangedEventArgs.NewValue;
                ViewAlterContactInfo.DataContext = dependencyPropertyChangedEventArgs.NewValue;
            }
        }

        [Import] public IAlterPersonViewModel ViewModel {
            get { return DataContext as IAlterPersonViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
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
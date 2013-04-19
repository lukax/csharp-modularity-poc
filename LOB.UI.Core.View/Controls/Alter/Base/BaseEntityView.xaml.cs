#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base {
    [Export(typeof(IBaseView<IBaseViewModel>)), ExportMetadata(null, ViewType.BaseEntity)]
    public partial class BaseEntityView : IBaseView<IBaseViewModel> {
        public BaseEntityView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IBaseViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        public string Header {
            get { return Strings.UI_Header_Alter_BaseEntity; }
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
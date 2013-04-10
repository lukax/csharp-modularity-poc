#region Usings

using System;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Util {
    public partial class ListContextToolMenuView : IBaseView {

        public ListContextToolMenuView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public string Header {
            get { return Strings.UI_Header_Alter_BaseEntity; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
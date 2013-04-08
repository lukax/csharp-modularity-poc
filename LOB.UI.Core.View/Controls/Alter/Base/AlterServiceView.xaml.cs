#region Usings

using System;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base {
    public partial class AlterServiceView : IBaseView {

        public AlterServiceView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterServiceViewModel; }
            set {
                DataContext = value;
                ViewAlterBaseEntity.DataContext = value;
            }
        }

        public string Header {
            get { return Strings.Header_Alter_Service; }
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
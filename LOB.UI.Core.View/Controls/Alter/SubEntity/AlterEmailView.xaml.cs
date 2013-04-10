#region Usings

using System;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    public partial class AlterEmailView : IBaseView {

        public AlterEmailView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterEmailViewModel; }
            set {
                DataContext = value;
                ViewConfCancelTools.DataContext = value;
            }
        }

        public string Header {
            get { return Strings.UI_Header_Alter_Email; }
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
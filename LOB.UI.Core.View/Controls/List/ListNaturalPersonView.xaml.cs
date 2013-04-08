#region Usings

using System;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.View.Controls.List {
    public partial class ListNaturalPersonView : IBaseView {

        public ListNaturalPersonView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IListNaturalPersonViewModel; }
            set {
                DataContext = value;
                ViewListBaseEntity.DataContext = value;
                ViewListContextTool.DataContext = value;
            }
        }

        public string Header {
            get { return Strings.Header_List_Category; }
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
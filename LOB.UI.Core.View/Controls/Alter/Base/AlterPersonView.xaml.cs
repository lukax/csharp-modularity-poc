#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.View.Controllers;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base {
    [Export(typeof(IBaseView<IAlterPersonViewModel>)), Export(typeof(IBaseView<IBaseViewModel>))]
    [ViewInfo(ViewType.Person, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
    public partial class AlterPersonView : IBaseView<IAlterPersonViewModel> {
        public AlterPersonView() { InitializeComponent(); }

        [Import] public IAlterPersonViewModel ViewModel {
            get { return DataContext as IAlterPersonViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        [Import] public PersonRegionController Controller {
            set { value.ThisOne = new Lazy<IBaseViewModel>(() => ViewModel); }
        }

        public int Index { get; set; }

        public void Refresh() { }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
#region Usings

using System.ComponentModel.Composition;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Controllers;
using LOB.UI.Core.View.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity {
    [Export(typeof(IBaseView<IAlterPersonViewModel>)), Export(typeof(IBaseView<IBaseViewModel>)), PartCreationPolicy(CreationPolicy.NonShared)]
    [ViewInfo(ViewType.Person, new[] {ViewState.Add, ViewState.Update, ViewState.Delete})]
    public partial class AlterPersonView : IBaseView<IAlterPersonViewModel> {
        private PersonRegionController _controller;
        public AlterPersonView() { InitializeComponent(); }

        public IAlterPersonViewModel ViewModel {
            get { return DataContext as IAlterPersonViewModel; }
            set { DataContext = value; }
        }

        [Import] public PersonRegionController Controller {
            get { return _controller; }
            set {
                _controller = value;
                ViewModel = value.ViewModel;
            }
        }

        public int Index { get; set; }

        public void Dispose() { Controller.Dispose(); }
    }
}
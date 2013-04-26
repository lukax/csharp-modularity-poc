#region Usings

using System.ComponentModel.Composition;
using LOB.UI.Contract;
using LOB.UI.Contract.Controller;
using LOB.UI.Contract.ViewModel.Controls.Alter;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.Event.View;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

#endregion

namespace LOB.UI.Core.View.Controllers {
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class NaturalPersonRegionController : IBaseController, IPartImportsSatisfiedNotification {
        [Import] protected IEventAggregator EventAggregator { get; set; }
        [Import] protected IRegionManager RegionManager { get; set; }
        [Import] protected IBaseView<IAlterPersonViewModel> AlterPersonView { get; set; }

        [Import] public IAlterNaturalPersonViewModel ViewModel { get; set; }

        public void OnImportsSatisfied() {
            EventAggregator.GetEvent<SetupChildViewEvent>()
                           .Publish(new SetupChildPayload(AlterPersonView.ViewModel.Id, ViewModel.Id, ViewModel.Entity));

            RegionManager.RegisterViewWithRegion("PersonRegion", () => AlterPersonView);

            AlterPersonView.ViewModel.InitializeServices();
        }

        public void Dispose() {
            AlterPersonView.Dispose();
            ViewModel.Dispose();
        }
    }
}
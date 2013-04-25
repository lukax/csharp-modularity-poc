#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Contract;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.Event.View;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

#endregion

namespace LOB.UI.Core.View.Controllers {
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonRegionController {
        [Import] public Lazy<IEventAggregator> EventAggregator { get; set; }
        [Import] public Lazy<IBaseView<IAlterAddressViewModel>> AlterAddressView { get; set; }
        [Import] public Lazy<IBaseView<IAlterContactInfoViewModel>> AlterContactInfoView { get; set; }
        public Lazy<IBaseViewModel> ThisOne { get; set; }

        [Import] public IRegionManager Manager {
            set {
                value.RegisterViewWithRegion("AddressRegion", () => {
                                                                  var view = AlterAddressView.Value;
                                                                  EventAggregator.Value.GetEvent<SetupChildViewEvent>()
                                                                                 .Publish(new SetupChildPayload(view.ViewModel.Id, ThisOne.Value.Id));
                                                                  return view;
                                                              });
                value.RegisterViewWithRegion("ContactInfoRegion", () => {
                                                                      var view = AlterContactInfoView.Value;
                                                                      EventAggregator.Value.GetEvent<SetupChildViewEvent>()
                                                                                     .Publish(new SetupChildPayload(view.ViewModel.Id,
                                                                                                                    ThisOne.Value.Id));
                                                                      return view;
                                                                  });
            }
        }
    }
}
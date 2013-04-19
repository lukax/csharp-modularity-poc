#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterStoreViewModel))]
    public sealed class AlterStoreViewModel : AlterBaseEntityViewModel<Store>, IAlterStoreViewModel {
        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Store, State = ViewState.Add};

        [ImportingConstructor]
        public AlterStoreViewModel(IRepository repository, IStoreFacade storeFacade, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(storeFacade, repository, eventAggregator, logger) { }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
        }
    }
}
#region Usings

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
    public sealed class AlterSaleViewModel : AlterBaseEntityViewModel<Sale>, IAlterSaleViewModel {
        private readonly ViewID _viewID = new ViewID {Type = ViewType.Service, State = ViewState.Add};

        public AlterSaleViewModel(ISaleFacade saleFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(saleFacade, repository, eventAggregator, logger) { }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _viewID;
            base.InitializeServices();
        }
    }
}
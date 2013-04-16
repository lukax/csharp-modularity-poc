#region Usings

using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterEmailViewModel : AlterBaseEntityViewModel<Email>, IAlterEmailViewModel {
        public AlterEmailViewModel(IRepository repository, IEmailFacade emailFacade, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(emailFacade, repository, eventAggregator, logger) { }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
        }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Email, State = ViewState.Add};
    }
}
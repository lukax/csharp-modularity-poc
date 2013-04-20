#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterEmailViewModel))]
    public sealed class AlterEmailViewModel : AlterBaseEntityViewModel<Email>, IAlterEmailViewModel {
        [ImportingConstructor]
        public AlterEmailViewModel(IRepository repository, IEmailFacade emailFacade, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(emailFacade, repository, eventAggregator, logger) { }

        public override void InitializeServices() { base.InitializeServices(); }
    }
}
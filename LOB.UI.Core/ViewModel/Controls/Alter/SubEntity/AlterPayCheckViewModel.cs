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
    [Export(typeof(IAlterPayCheckViewModel))]
    public sealed class AlterPayCheckViewModel : AlterBaseEntityViewModel<PayCheck>, IAlterPayCheckViewModel {
        [ImportingConstructor]
        public AlterPayCheckViewModel(IPayCheckFacade payCheckFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(payCheckFacade, repository, eventAggregator, logger) { }

        public override void InitializeServices() { base.InitializeServices(); }
    }
}
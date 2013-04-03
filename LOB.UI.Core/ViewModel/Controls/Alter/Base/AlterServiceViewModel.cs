#region Usings

using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    public abstract class AlterServiceViewModel : AlterBaseEntityViewModel<Service>, IAlterServiceViewModel {

        protected AlterServiceViewModel(Service entity, IRepository repository, IEventAggregator eventAggregator,
            ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) { }

    }
}
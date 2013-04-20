#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterSaleViewModel))]
    public sealed class AlterSaleViewModel : AlterBaseEntityViewModel<Sale>, IAlterSaleViewModel {
        [ImportingConstructor]
        public AlterSaleViewModel(ISaleFacade saleFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(saleFacade, repository, eventAggregator, logger) { }
    }
}
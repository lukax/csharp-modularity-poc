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
    [Export(typeof(IAlterEmployeeViewModel))]
    public sealed class AlterEmployeeViewModel : AlterBaseEntityViewModel<Employee>, IAlterEmployeeViewModel {
        private AlterNaturalPersonViewModel _alterNaturalPersonViewModel;
        public IAlterNaturalPersonViewModel AlterNaturalPersonViewModel {
            get { return _alterNaturalPersonViewModel; }
            set { _alterNaturalPersonViewModel = value as AlterNaturalPersonViewModel; }
        }

        [ImportingConstructor]
        public AlterEmployeeViewModel(IEmployeeFacade employeeFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger,
            IAlterNaturalPersonViewModel alterNaturalPersonViewModel)
            : base(employeeFacade, repository, eventAggregator, logger) { AlterNaturalPersonViewModel = alterNaturalPersonViewModel; }

        public override void InitializeServices() {
            AlterNaturalPersonViewModel.InitializeServices();
            base.InitializeServices();
        }

        protected override void EntityChanged() {
            base.EntityChanged();
            _alterNaturalPersonViewModel.Entity = Entity;
        }

        public override void Dispose() {
            AlterNaturalPersonViewModel.Dispose();
            base.Dispose();
        }
    }
}
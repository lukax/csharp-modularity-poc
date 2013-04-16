#region Usings

using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterEmployeeViewModel : AlterBaseEntityViewModel<Employee>, IAlterEmployeeViewModel {
        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Employee, State = ViewState.Add};
        private AlterNaturalPersonViewModel _alterNaturalPersonViewModel;
        [Dependency]
        public IAlterNaturalPersonViewModel AlterNaturalPersonViewModel {
            get { return _alterNaturalPersonViewModel; }
            set { _alterNaturalPersonViewModel = value as AlterNaturalPersonViewModel; }
        }

        public AlterEmployeeViewModel(IEmployeeFacade employeeFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(employeeFacade, repository, eventAggregator, logger) { }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
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
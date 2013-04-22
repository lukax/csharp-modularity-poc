#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter;

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
        public AlterEmployeeViewModel(IEmployeeFacade employeeFacade)
            : base(employeeFacade) { }

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
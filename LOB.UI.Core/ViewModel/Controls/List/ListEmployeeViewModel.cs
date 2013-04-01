#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    public sealed class ListEmployeeViewModel : ListNaturalPersonViewModel, IListEmployeeViewModel {

        [InjectionConstructor] public ListEmployeeViewModel(Employee entity, IRepository repository,
            IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) {}

        public new Expression<Func<Employee, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.Title.ToUpper().Contains(Search.ToUpper()) ||
                         arg.FirstName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.LastName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.NickName.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.Notes.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.Rg.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.Cpf.ToString().ToUpper().Contains(Search.ToUpper()));
                }
                catch(FormatException) {
                    return arg => false;
                }
            }
        }

        protected override bool CanUpdate(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override bool CanDelete(object arg) {
            //TODO: Business logic
            return true;
        }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.List};
        public override UIOperation UIOperation {
            get { return _operation; }
        }

    }
}
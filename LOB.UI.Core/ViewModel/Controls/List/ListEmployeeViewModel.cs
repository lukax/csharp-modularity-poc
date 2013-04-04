#region Usings

using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    public sealed class ListEmployeeViewModel : ListNaturalPersonViewModel, IListEmployeeViewModel {

        [InjectionConstructor]
        public ListEmployeeViewModel(Employee entity, IRepository repository,
            IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

        private CultureInfo Culture {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        public new Expression<Func<Employee, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Title.ToUpper().Contains(Search.ToUpper()) ||
                         arg.FirstName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.LastName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.NickName.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Notes.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.RG.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.CPF.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void InitializeServices() { Operation = _operation; }

        protected override bool CanUpdate(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override bool CanDelete(object arg) {
            //TODO: Business logic
            return true;
        }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Employee,
            State = UIOperationState.List
        };

    }
}
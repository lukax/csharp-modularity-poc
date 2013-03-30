#region Usings
using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
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
                         arg.Code.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Title.ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.FirstName.ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.LastName.ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.NickName.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Notes.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Rg.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Cpf.ToString().ToUpper().Contains(this.Search.ToUpper()));
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

    }
}
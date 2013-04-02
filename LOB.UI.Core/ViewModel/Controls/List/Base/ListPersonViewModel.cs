#region Usings

using System;
using System.Globalization;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Interface.ViewModel.Controls.List.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base {
    public abstract class ListPersonViewModel : ListBaseEntityViewModel<Person>, IListPersonViewModel {

        protected ListPersonViewModel(Person entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

        public new Expression<Func<Person, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(CultureInfo.InvariantCulture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Notes.ToString(CultureInfo.InvariantCulture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
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
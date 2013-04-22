#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    [Export(typeof(IListCustomerViewModel))]
    public sealed class ListCustomerViewModel : ListBaseEntityViewModel<Customer>, IListCustomerViewModel {
        [ImportingConstructor]
        public ListCustomerViewModel(IRepository repository, IEventAggregator eventAggregator)
            : base() { }

        public new Expression<Func<Employee, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Title.ToUpper().Contains(Search.ToUpper()) || arg.HireDate.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) || arg.Title.ToUpper().Contains(Search.ToUpper()) ||
                         arg.FirstName.ToUpper().Contains(Search.ToUpper()) || arg.LastName.ToUpper().Contains(Search.ToUpper()) ||
                         arg.NickName.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Notes.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.RG.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.CPF.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}
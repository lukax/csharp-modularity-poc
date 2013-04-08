#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List {
    public sealed class ListCustomerViewModel : ListBaseEntityViewModel<Customer>, IListCustomerViewModel {

        public ListCustomerViewModel(Customer entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) {
            Entity = entity;
            if(Entity.Person == null) throw new ArgumentException("Entity has not defined a person");
        }

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

        public override void InitializeServices() { Operation = _operation; }

        public override void Refresh() { Search = ""; }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.List};

    }
}